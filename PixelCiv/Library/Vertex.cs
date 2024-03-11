using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Library
{
    public class Vertex
    {
        private Vector2 _position;
        private Vector2 _origin;
        private Vector2 _scale;

        public Vertex(Vector2 origin) 
        {
            _origin = origin;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public Vertex SetPosition(Vector2 pos)
        {
            _position = pos;
            return this;
        }

        public Vector2 GetScale()
        {
            return _scale; 
        }
        public Vertex SetScale(Vector2 scale)
        {
            _scale = scale;
            return this;
        }

        public float CalculateX()
        {
            return (_position.X + _origin.X) * _scale.X;
        }

        public float CalculateY()
        {
            return (_position.Y + _origin.Y) * _scale.Y;
        }
    }
}
