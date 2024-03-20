using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Modules.Logistics;
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
        public static Texture2D Pixel;

        public static SpriteFont BaseFont;

        public static void Load(ContentManager content, GraphicsDevice graphics)
        {
            BaseTileTexture = content.Load<Texture2D>("Textures/Tiles/tile_hex_blank");
            BaseHouseTexture = content.Load<Texture2D>("Textures/house_red");
            BaseButtonTexture = content.Load<Texture2D>("Textures/square_blank");

            Pixel = new Texture2D(graphics, 1, 1);
            Pixel.SetData(new Color[] { Color.White });

            BaseFont = content.Load<SpriteFont>("Fonts/CascadiaMono-Regular");
        }
    }
}
