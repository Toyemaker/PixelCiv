using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public interface IComponent
    {
        string Name { get; set; }
        IComponent Parent { get; set; }

        IEnumerable<T> GetChildren<T>() where T : IComponent;

        T GetChild<T>() where T : IComponent
        {
            return GetChildren<T>().FirstOrDefault();
        }

        bool IsEnabled { get; set; }
    }
}
