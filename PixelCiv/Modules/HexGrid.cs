using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.GameObjects
{
    public class HexGrid : GameObject
    {
        public ResourceManager ResourceManager { get; set; }

        private Dictionary<Point, HexTile> TileDictionary;

        public HexGrid() 
        { 
            TileDictionary = new Dictionary<Point, HexTile>();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    HexTile tile = new HexTile();
                    tile.Transform.Position = new Vector2(x * 22 + (y + 1) % 2 * 11, y * 4);

                    TileDictionary.Add(new Point(x, y), tile);
                    AddComponent(tile);
                }
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
