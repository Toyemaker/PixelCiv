using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using PixelCiv.GameObjects.Structures;
using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.GameObjects
{
    public class HexTile : GameObject
    {
        private bool _isHovered;

        public HexTile()
        {
            AddComponent("sprite", new Sprite2D(GameData.BaseTileTexture));

            List<Vector2> vertices = new List<Vector2>()
            {
                new Vector2(0, 6),
                new Vector2(0, 9),
                new Vector2(4, 14),
                new Vector2(11, 14),
                new Vector2(16, 9),
                new Vector2 (16, 6),
                new Vector2 (11, 2),
                new Vector2 (4, 2),
            };

            AddComponent("boundingBox", new Polygon(vertices));
        }

        public override void Update(GameTime gameTime)
        {
            if (_isHovered)
            {
                GetChild<Sprite2D>("sprite").Color = Color.Red;
                _isHovered = false;
            }
            else
            {
                GetChild<Sprite2D>("sprite").Color = Color.White;
            }

            base.Update(gameTime);
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            if (GetChild<Polygon>("boundingBox").ContainsPoint(input.GetMousePosition()))
            {
                if (ContainsChild("house") == default)
                {
                    if (input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        Warehouse house = new Warehouse();
                        if (Parent is HexGrid grid)
                        {
                            grid.GetChild<ResourceManager>("resourceManager").Add(house.GetChild<ResourceStorage>("storage"));
                        }
                        AddComponent("house", house);
                    }   
                }
                else if (input.IsMouseButtonPressed(MouseButton.Right))
                {
                    if (Parent is HexGrid grid)
                    {
                        grid.GetChild<ResourceManager>("resourceManager")?.Remove(GetChild<Warehouse>("house").GetChild<ResourceStorage>("storage"));
                    }
                    RemoveComponent("house");
                }

                _isHovered = true;

                return true;
            }

            return base.Interact(input, gameTime);
        }
    }
}
