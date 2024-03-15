using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.UI
{
    public class Button : GameObject
    {
        private Sprite2D _sprite;
        private Polygon _polygon;

        public Button()
        {
            _sprite = new Sprite2D(GameData.BaseButtonTexture);
            AddComponent(_sprite);

            List<Vector2> vertices = new List<Vector2>()
            {
                new Vector2 (0, 0),
                new Vector2 (0, 15),
                new Vector2 (15, 15),
                new Vector2 (15, 0),
            };

            _polygon = new Polygon(vertices);

            AddComponent(_polygon);
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            if (_polygon.ContainsPoint(input.GetMousePosition()))
            {
                if (input.IsMouseButtonDown(MouseButton.Left))
                {
                    _sprite.Color = Color.Green;
                }
                else
                {
                    _sprite.Color = Color.Red;
                }

                return true;
            }
            else
            {
                _sprite.Color = Color.White;
            }

            return base.Interact(input, gameTime);
        }
    }
}
