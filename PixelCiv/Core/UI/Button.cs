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
        public Action<Input, GameTime> OnInteractEvent { get; private set; }

        public Button()
        {
            AddComponent("buttonSprite", new Sprite2D(GameData.BaseButtonTexture));

            List<Vector2> vertices = new List<Vector2>()
            {
                new Vector2 (0, 0),
                new Vector2 (0, 15),
                new Vector2 (15, 15),
                new Vector2 (15, 0),
            };

            AddComponent("boundingBox", new Polygon(vertices));
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            if (GetChild<Polygon>("boundingBox").ContainsPoint(input.GetMousePosition()))
            {
                if (input.IsMouseButtonDown(MouseButton.Left))
                {
                    GetChild<Sprite2D>("buttonSprite").Color = Color.Green;
                    OnInteractEvent?.Invoke(input, gameTime);
                }
                else
                {
                    GetChild<Sprite2D>("buttonSprite").Color = Color.Red;
                }

                return true;
            }
            else
            {
                GetChild<Sprite2D>("buttonSprite").Color = Color.White;
            }

            return base.Interact(input, gameTime);
        }
    }
}
