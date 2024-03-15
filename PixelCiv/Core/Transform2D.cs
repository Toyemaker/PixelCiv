using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core
{
    public class Transform2D : IComponent
    {
        public GameObject Parent { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        public Transform2D() : this(null) { }
        public Transform2D(GameObject parent) : this(parent, default)
        {

        }
        public Transform2D(GameObject parent, Vector2 pos) : this (parent, pos, Vector2.One, 0)
        {

        }
        public Transform2D(GameObject parent, Vector2 pos, Vector2 scale, float rot)
        {
            Parent = parent;
            Position = pos;
            Rotation = rot;
            Scale = scale;
        }
        public Vector2 GetGlobalPosition()
        {
            return Position + (Parent?.Parent != null ? Parent.Parent.Transform.GetGlobalPosition() : Vector2.Zero);
        }
        public Vector2 GetGlobalScale()
        {
            return Scale * (Parent?.Parent != null ? Parent.Parent.Transform.GetGlobalScale() : Vector2.One);
        }
        public float GetGlobalRotation()
        {
            return Rotation + (Parent?.Parent != null ? Parent.Parent.Transform.GetGlobalRotation() : 0f);
        }
    }
}
