using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Graphics
{
    public class Camera : GameObject
    {
        public Camera()
        {
            Transform.Scale = new Vector2(1);
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
