using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Library
{
    public class Polygon
    {
        private Vector2 _position;
        private Vector2 _scale;

        public List<Vertex> Vertices;

        public Polygon(List<Vertex> verts) 
        {
            Vertices = verts;
            SetScale(new Vector2(2));
        }

        public Polygon SetPosition(Vector2 pos)
        {
            _position = pos;
            foreach (Vertex vert in Vertices)
            {
                vert.SetPosition(pos);
            }
            return this;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public Vector2 GetScale()
        {
            return _scale;
        }
        public Polygon SetScale(Vector2 scale)
        {
            _scale = scale;

            foreach (Vertex vert in Vertices)
            {
                vert.SetScale(scale);
            }

            return this;
        }

        public bool ContainsPoint(Vector2 point)
        {
            bool result = false;

            int j = Vertices.Count - 1;
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].CalculateY() < point.Y && Vertices[j].CalculateY() >= point.Y
                    || Vertices[j].CalculateY() < point.Y && Vertices[i].CalculateY() >= point.Y)
                {
                    if (Vertices[i].CalculateX() + (point.Y - Vertices[i].CalculateY()) / (Vertices[j].CalculateX() + Vertices[i].CalculateY()) * (Vertices[j].CalculateX() - Vertices[j].CalculateX()) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }
    }
}
