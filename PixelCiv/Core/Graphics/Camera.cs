using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Graphics
{
    public class Camera : GameObject
    {
        public Camera()
        {
            Transform.Scale = new Vector2(1f);
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(Transform.GetGlobalPosition().X, Transform.GetGlobalPosition().Y, 0)
                 * Matrix.CreateScale(Transform.GetGlobalScale().X, Transform.GetGlobalScale().Y, 1)
                 * Matrix.CreateTranslation(GameData.ScreenResolution.Width / 2, GameData.ScreenResolution.Height / 2, 0);
        }

        public Vector2 ConvertToWorldPosition(Vector2 pos)
        {
            return pos / Transform.GetGlobalScale() - Transform.GetGlobalPosition();
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            float speed = 100;

            if (input.IsKeyDown(Keys.W))
            {
                Transform.Position += new Vector2(0, speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (input.IsKeyDown(Keys.S))
            {
                Transform.Position += new Vector2(0, -speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (input.IsKeyDown(Keys.A))
            {
                Transform.Position += new Vector2(speed, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (input.IsKeyDown(Keys.D))
            {
                Transform.Position += new Vector2(-speed, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (input.IsKeyDown(Keys.Z))
            {
                if (Transform.Scale.X >= 4)
                {
                    Transform.Scale = new Vector2(4);
                }
                else
                {
                    Transform.Scale += new Vector2(1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            if (input.IsKeyDown(Keys.X))
            {
                if (Transform.Scale.X <= 1)
                {
                    Transform.Scale = new Vector2(1);
                }
                else
                {
                    Transform.Scale -= new Vector2(1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            return base.Interact(input, gameTime);
        }
    }
}
