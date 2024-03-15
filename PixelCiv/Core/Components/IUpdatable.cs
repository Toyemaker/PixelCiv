using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public interface IUpdatable : IComponent
    {
        void Update(GameTime gameTime) { }
        void FixedUpdate(float tickInterval) { }
        bool IsActive { get; }
    }
}
