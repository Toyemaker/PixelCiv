using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core
{
    public class Vertex : ITransformable
    {
        public string Name { get; set; }
        public IComponent Parent { get; set; }
        public bool IsEnabled { get; set; }

        public Transform2D Transform { get; private set; }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            if (Transform is T t)
            {
                yield return t;
            }

            yield break;
        }

        public Vertex()
        {
            Transform = new Transform2D(this);
        }
    }
}
