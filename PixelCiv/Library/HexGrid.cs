using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Library
{
    public class HexGrid
    {
        private Dictionary<Point, HexTile> TileDictionary;

        public HexGrid() 
        { 
            TileDictionary = new Dictionary<Point, HexTile>();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    TileDictionary.Add(new Point(x, y), new HexTile().SetPosition(new Vector2(x * 22 + (y + 1) % 2 * 11, y * 4)));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (KeyValuePair<Point, HexTile> tile in TileDictionary.OrderBy(a => a.Key.Y).ThenBy(a => a.Key.X))
            {
                tile.Value.Draw(spriteBatch, gameTime);
            }
        }

        public bool ContainsPoint(Vector2 point)
        {
            foreach (KeyValuePair<Point, HexTile> tile in TileDictionary.OrderBy(a => a.Key.Y).ThenBy(a => a.Key.X).Reverse())
            {
                if (tile.Value.ContainsPoint(point))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
