using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core
{
    public class Polygon : ITransformable
    {
        public IComponent Parent { get; set; }
        public bool IsEnabled { get; set; }
        public Transform2D Transform { get; private set; }

        private List<Vertex> _vertices;

        public Polygon(List<Vector2> vertices)
        {
            Transform = new Transform2D(this);
            IsEnabled = true;

            _vertices = new List<Vertex>();

            foreach (Vector2 vec in vertices)
            {
                Vertex vert = new Vertex(this);
                vert.Transform.Position = vec;

                _vertices.Add(vert);
            }
        }

        public bool ContainsPoint(Vector2 point)
        {
            bool result = false;

            int j = _vertices.Count - 1;
            for (int i = 0; i < _vertices.Count; i++)
            {
                if (_vertices[i].Transform.GetGlobalPosition().Y < point.Y && _vertices[j].Transform.GetGlobalPosition().Y >= point.Y ||
                    _vertices[j].Transform.GetGlobalPosition().Y < point.Y && _vertices[i].Transform.GetGlobalPosition().Y >= point.Y)
                {
                    if (_vertices[i].Transform.GetGlobalPosition().X + (point.Y - _vertices[i].Transform.GetGlobalPosition().Y) /
                        (_vertices[j].Transform.GetGlobalPosition().Y - _vertices[i].Transform.GetGlobalPosition().Y) *
                        (_vertices[j].Transform.GetGlobalPosition().X - _vertices[i].Transform.GetGlobalPosition().X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            return _vertices.OfType<T>();
        }
    }
}
