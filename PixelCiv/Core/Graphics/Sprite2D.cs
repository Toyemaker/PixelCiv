using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Graphics
{
    public class Sprite2D : IRenderable
    {
        public GameObject Parent { get; set; }

        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float LayerDepth { get; set; }
        public bool IsVisible { get; set; }

        public Sprite2D(Texture2D texture)
        {
            Texture = texture;

            SourceRectangle = Texture.Bounds;
            Color = Color.White;
            Origin = new Vector2();
            SpriteEffects = SpriteEffects.None;
            LayerDepth = 0;

            IsVisible = true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Parent.Transform.GetGlobalPosition() * Parent.Transform.GetGlobalScale(), SourceRectangle, Color, Parent.Transform.GetGlobalRotation(), Origin, Parent.Transform.GetGlobalScale(), SpriteEffects, LayerDepth);
        }
    }
}
