using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public interface IComponent
    {
        GameObject Parent { get; set; }
    }
}
