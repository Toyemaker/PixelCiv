using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Modules.Displays;
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
        private Dictionary<Point, HexTile> _tileDictionary;

        public HexGrid() 
        { 
            _tileDictionary = new Dictionary<Point, HexTile>();

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    HexTile tile = new HexTile();
                    tile.Transform.Position = new Vector2(x * 22 + (y + 1) % 2 * 11, y * 4);

                    _tileDictionary.Add(new Point(x, y), tile);
                    AddComponent("(" + x + ", " + y + ")", tile);
                }
            }

            AddComponent("resourceManager", new ResourceManager());
        }
    }
}
