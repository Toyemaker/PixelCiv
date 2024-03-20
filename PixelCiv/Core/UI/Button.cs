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
        public event Action<Input, GameTime, Button> OnInteractEvent;

        public Button(Sprite2D sprite)
        {
            AddComponent("buttonSprite", sprite);

            List<Vector2> vertices = new List<Vector2>()
            {
                new Vector2(sprite.SourceRectangle.Left * sprite.Transform.GetGlobalScale().X, sprite.SourceRectangle.Top * sprite.Transform.GetGlobalScale().Y),
                new Vector2 (sprite.SourceRectangle.Left * sprite.Transform.GetGlobalScale().X, sprite.SourceRectangle.Bottom * sprite.Transform.GetGlobalScale().Y),
                new Vector2 (sprite.SourceRectangle.Right * sprite.Transform.GetGlobalScale().X, sprite.SourceRectangle.Bottom * sprite.Transform.GetGlobalScale().Y),
                new Vector2 (sprite.SourceRectangle.Right * sprite.Transform.GetGlobalScale().X, sprite.SourceRectangle.Top * sprite.Transform.GetGlobalScale().Y),
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
                    OnInteractEvent?.Invoke(input, gameTime, this);
                }
                else
                {
                    GetChild<Sprite2D>("buttonSprite").Color = Color.Red;
                }

                return true;
            }
            else
            {
                GetChild<Sprite2D>("buttonSprite").Color = Color.Gray;
            }

            return base.Interact(input, gameTime);
        }
    }
}
