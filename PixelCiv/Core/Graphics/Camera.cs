using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Graphics
{
    public class Camera : ITransformable
    {
        public Transform2D Transform { get; private set; }
        public GameObject Parent { get; set; }

        public Camera()
        {
            Transform = new Transform2D();
            Transform.Scale = new Vector2(4);
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(Transform.GetGlobalPosition().X, Transform.GetGlobalPosition().Y, 0)
                 * Matrix.CreateScale(Transform.GetGlobalScale().X, Transform.GetGlobalScale().Y, 1);
        }
        public Vector2 ConvertToWorldPosition(Vector2 pos)
        {
            return (pos - Transform.GetGlobalPosition()) / Transform.GetGlobalScale();
        }
    }
}
