using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Library
{
    public static class GameData
    {
        public static Texture2D BaseTileTexture;

        public static void Load(ContentManager content)
        {
            BaseTileTexture = content.Load<Texture2D>("Textures/Tiles/tile_hex_blank");
        }
    }
}
