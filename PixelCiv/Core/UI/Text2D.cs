using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCiv.Core.Components;

namespace PixelCiv.Core.UI
{
    internal class Text2D : IRenderable
    {
        public GameObject Parent { get; set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float LayerDepth { get; set; }
        public bool IsVisible { get; set; }

        public Text2D(GameObject parent, SpriteFont font, string text)
        {
            Parent = parent;

            Font = font;

            Text = text;
            Color = Color.Black;
            Origin = new Vector2();
            SpriteEffects = SpriteEffects.None;
            LayerDepth = 0;

            IsVisible = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Font, Text, Parent.Transform.GetGlobalPosition() * Parent.Transform.GetGlobalScale(), Color, Parent.Transform.GetGlobalRotation(), Origin, Parent.Transform.GetGlobalScale(), SpriteEffects, LayerDepth);
        }
    }
}
