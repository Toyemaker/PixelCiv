using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using PixelCiv.GameObjects.Structures;
using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.GameObjects
{
    public class HexTile : GameObject
    {
        private Sprite2D _sprite;

        private Polygon _boundingBox;

        private bool _isHovered;

        public HexTile()
        {
            _sprite = new Sprite2D(GameData.BaseTileTexture);
            AddComponent(_sprite);

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

            _boundingBox = new Polygon(vertices);

            AddComponent(_boundingBox);
        }

        public override void Update(GameTime gameTime)
        {
            if (_isHovered)
            {
                _sprite.Color = Color.Red;
                _isHovered = false;
            }
            else
            {
                _sprite.Color = Color.White;
            }

            base.Update(gameTime);
        }

        public bool ContainsPoint(Vector2 point)
        {
            if (_boundingBox.ContainsPoint(point))
            {
                return true;
            }

            return false;
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            if (ContainsPoint(input.GetMousePosition()))
            {
                if (input.IsMouseButtonPressed(MouseButton.Left))
                {
                    Warehouse house = new Warehouse();
                    if (Parent is HexGrid grid)
                    {
                        grid.ResourceManager?.Add(house.Storage);
                    }
                    AddComponent(house);
                }

                _isHovered = true;

                return true;
            }

            return base.Interact(input, gameTime);
        }
    }
}
