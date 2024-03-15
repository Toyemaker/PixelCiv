using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCiv.Core.Components;
using static System.Net.Mime.MediaTypeNames;

namespace PixelCiv.Core.UI
{
    internal class Text2D : IRenderable
    {
        public IComponent Parent { get; set; }
        public Transform2D Transform { get; private set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float LayerDepth { get; set; }
        public bool IsVisible { get; set; }

        public Text2D(SpriteFont font, string text)
        {
            Font = font;
            Transform = new Transform2D(this);
            Text = text;
            Color = Color.Black;
            Origin = new Vector2();
            SpriteEffects = SpriteEffects.None;
            LayerDepth = 0;

            IsVisible = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Font, Text, Transform.GetGlobalPosition() * Transform.GetGlobalScale(), Color, Transform.GetGlobalRotation(), Origin, Transform.GetGlobalScale(), SpriteEffects, LayerDepth);
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            yield return Transform is T t ? t : default;
        }
    }
}
