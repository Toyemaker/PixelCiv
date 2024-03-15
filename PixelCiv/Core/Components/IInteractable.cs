using Microsoft.Xna.Framework;
using PixelCiv.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public interface IInteractable : IComponent
    {
        bool Interact(Input input, GameTime gameTime);
        bool IsInteractable { get; }
        bool IsBlocker { get; }
    }
}
