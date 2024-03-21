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

namespace PixelCiv.Modules.Tiles
{
    public class HexTile : GameObject
    {
        public Biome Biome { get; set; }

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
            base.Update(gameTime);
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            if (GetChild<Polygon>("boundingBox").ContainsPoint(input.GetMousePosition()))
            {
                if (Parent != null && Parent is HexGrid grid && grid.GetChild<GameObject>("placeStructure") != default)
                {
                    if (input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        AddComponent("structure", grid.GetChild<Structure>("placeStructure"));
                        GetChild<Structure>("structure").Transform.Position = new Vector2();
                        if (GetChild<Structure>("structure").ContainsChild("storage"))
                        {
                            grid.GetChild<ResourceManager>("resourceManager").Add(GetChild<Structure>("structure").GetChild<ResourceStorage>("storage"));
                        }
                        grid.RemoveComponent("placeStructure");
                    }
                    else
                    {
                        grid.GetChild<Structure>("placeStructure").Transform.Position = Transform.Position;
                    }
                }

                _isHovered = true;

                return true;
            }

            return base.Interact(input, gameTime);
        }
    }
}
