using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Data
{
    public static class GameData
    {
        public static Texture2D BaseTileTexture;
        public static Texture2D BaseHouseTexture;
        public static Texture2D BaseButtonTexture;

        public static SpriteFont BaseFont;

        public static void Load(ContentManager content)
        {
            BaseTileTexture = content.Load<Texture2D>("Textures/Tiles/tile_hex_blank");
            BaseHouseTexture = content.Load<Texture2D>("Textures/house_red");
            BaseButtonTexture = content.Load<Texture2D>("Textures/square_blank");

            BaseFont = content.Load<SpriteFont>("Fonts/CascadiaMono-Regular");
        }
    }
}
