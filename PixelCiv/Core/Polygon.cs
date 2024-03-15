using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core
{
    public class Polygon : GameObject
    {
        public List<Vector2> Vertices;

        public Polygon(List<Vector2> verts)
        {
            Vertices = verts;
        }

        public bool ContainsPoint(Vector2 point)
        {
            bool result = false;

            int j = Vertices.Count - 1;
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (GetY(Vertices[i]) < point.Y && GetY(Vertices[j]) >= point.Y ||
                    GetY(Vertices[j]) < point.Y && GetY(Vertices[i]) >= point.Y)
                {
                    if (GetX(Vertices[i]) + (point.Y - GetY(Vertices[i])) /
                        (GetY(Vertices[j]) - GetY(Vertices[i])) *
                        (GetX(Vertices[j]) - GetX(Vertices[i])) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }

        private float GetX(Vector2 vert)
        {
            return vert.X + Transform.GetGlobalPosition().X;
        }

        private float GetY(Vector2 vert)
        {
            return vert.Y + Transform.GetGlobalPosition().Y;
        }
    }
}
