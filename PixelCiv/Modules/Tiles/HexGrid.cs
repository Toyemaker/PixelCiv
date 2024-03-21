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

namespace PixelCiv.Modules.Tiles
{
    public class HexGrid : GameObject
    {
        private Dictionary<Point, HexTile> _tileDictionary;

        private Dictionary<string, Dictionary<string, int>> _generationWeightDict;

        private int _gridRadius;

        public HexGrid()
        {
            _tileDictionary = new Dictionary<Point, HexTile>();
            _generationWeightDict = new Dictionary<string, Dictionary<string, int>>()
            {
                {
                    "grass", new Dictionary<string, int>()
                    {
                        { "desert", 1 },
                        { "grass", 10 },
                        { "forest", 2 },
                        { "water", 0 },
                        { "beach", 4 },

                    }
                }
            };

            _gridRadius = 10;

            Generate();

            AddComponent("resourceManager", new ResourceManager());
        }

        public void Generate()
        {
            for (int y = -_gridRadius; y <= _gridRadius; y++)
            {
                for (int x = Math.Max(-(_gridRadius + y), -_gridRadius); x <= Math.Min(_gridRadius - y, _gridRadius); x++)
                {
                    int xPos = x + _gridRadius;
                    int yPos = y + _gridRadius / 2;
                    
                    HexTile tile = new HexTile();
                    tile.Transform.Position = new Vector2(xPos * 11, xPos * 4 + yPos * 8);

                    AddComponent("(" + x + ", " + y + ")", tile);
                    _tileDictionary.Add(new Point(x, y), tile);
                }
            }

            //int xPos = x + _gridRadius;
            //int yPos = y + _gridRadius;

            //HexTile tile = new HexTile();
            //tile.Transform.Position = new Vector2(xPos * 22 + yPos % 2 * 11, yPos * 4);

            //AddComponent("(" + x + ", " + y + ")", tile);
            //_tileDictionary.Add(new Point(x, y), tile);
        }
    }
}
