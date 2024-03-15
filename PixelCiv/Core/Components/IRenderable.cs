using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public interface IRenderable : IComponent
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        bool IsVisible { get; }
    }
}
