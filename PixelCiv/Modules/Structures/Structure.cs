using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.GameObjects.Structures
{
    public class Structure : GameObject
    {
        protected Sprite2D _sprite;

        public Structure()
        {
            _sprite = new Sprite2D(GameData.BaseHouseTexture);
            AddComponent(_sprite);
        }
    }
}
