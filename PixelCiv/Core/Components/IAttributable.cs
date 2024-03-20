using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public interface IAttributable
    {
        bool HasAttribute(string name);
        void AddAttribute(string name);
        void RemoveAttribute(string name);
    }
}
