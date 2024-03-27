using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Graphics;
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
        private static Random _random = new Random(DateTime.Now.Millisecond);

        public static List<List<Color>> BiomeColors = new List<List<Color>>()
        {
            new List<Color>()
            {
                new Color(212, 255, 77),
                new Color(212, 255, 77),
                new Color(144, 255, 77),
                new Color(144, 255, 77),
                new Color(77, 255, 77),
                new Color(28, 178, 66),
                new Color(28, 178, 66),
                new Color(28, 178, 66),
            },
            new List<Color>()
            {
                new Color(212, 255, 77),
                new Color(212, 255, 77),
                new Color(144, 255, 77),
                new Color(77, 255, 77),
                new Color(77, 255, 77),
                new Color(28, 178, 66),
                new Color(28, 178, 66),
            },
            new List<Color>()
            {
                new Color(170, 192, 102),
                new Color(170, 192, 102),
                new Color(82, 154, 82),
                new Color(10, 154, 118),
                new Color(10, 154, 118),
                new Color(10, 154, 118),
            },
            new List<Color>()
            {
                new Color(144, 144, 122),
                new Color(144, 144, 122),
                new Color(29, 96, 96),
                new Color(29, 96, 96),
                new Color(29, 96, 96),
            },
            new List<Color>()
            {
                new Color(15, 59, 59),
                new Color(15, 59, 59),
                new Color(15, 59, 59),
                new Color(15, 59, 59),
            },
            new List<Color>()
            {
                Color.White,
                Color.White,
                Color.White,
            }
        };

        public int _gridRadius { get; private set; }

        public HexGrid()
        {
            _tileDictionary = new Dictionary<Point, HexTile>();

            _gridRadius = 10;

            Generate();

            AddComponent("resourceManager", new ResourceManager());
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            return false;
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

                    if (x == y && x == 0)
                    {
                        tile.GetChild<Sprite2D>("sprite").Color = Color.White;
                    }

                    AddComponent("(" + x + ", " + y + ")", tile);
                    _tileDictionary.Add(new Point(x, y), tile);
                }
            }
        }

        //public void Spread(Point point, float tempFactor, float altitudeFactor, float humidityFactor, int spread, int step = 0)
        //{
        //    if (tempFactor != 0)
        //    {
        //        _tileDictionary[point].Temperature = tempFactor * spread / (spread + step);
        //    }
        //    if (altitudeFactor != 0)
        //    {
        //        _tileDictionary[point].Altitude = altitudeFactor * spread / (spread +step);
        //    }
        //    if (humidityFactor != 0)
        //    {
        //        _tileDictionary[point].Humidity = humidityFactor * spread / (spread + step);
        //    }

        //    if (spread <= 1)
        //    {
        //        _tileDictionary[point].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(_tileDictionary[point].Temperature, _tileDictionary[point].Altitude, _tileDictionary[point].Humidity);
        //        return;
        //    }

        //    for (int y = -1; y <= 1; y++)
        //    {
        //        for (int x = Math.Max(-(1 + y), -1); x <= Math.Min(1 - y, 1); x++)
        //        {
        //            if ((x == y && x == 0) || !_tileDictionary.ContainsKey(point + new Point(x, y)))
        //            {
        //                continue;
        //            }

        //            if ((tempFactor != 0 && _tileDictionary[point + new Point(x, y)].Temperature > tempFactor) ||
        //                (altitudeFactor != 0 && _tileDictionary[point + new Point(x, y)].Altitude > altitudeFactor) ||
        //                (humidityFactor != 0 && _tileDictionary[point + new Point(x, y)].Humidity > humidityFactor))
        //            {
        //                continue;
        //            }

        //            float ran = _random.Next(spread + step + 1);

        //            if (ran < spread)
        //            {
        //                Spread(point + new Point(x, y), tempFactor * (1 - (1f / spread)), altitudeFactor, humidityFactor * (1 - (1f / spread)), spread - 1, step + 1);
        //            }
        //        }
        //    }

        //    _tileDictionary[point].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(_tileDictionary[point].Temperature, _tileDictionary[point].Altitude, _tileDictionary[point].Humidity);
        //}


        public void Spread(Point point, float factor, int spread, int step = 0)
        {

        }

        public Color GetBiomeColor(float temp, float altitude, float humidity)
        {
            if (altitude < 0.25)
            {
                return new Color(altitude, altitude * 2, 255);
            }
            else
            {
                int tempIndex = Math.Clamp((int)Math.Floor((BiomeColors.Count) * temp), 0, BiomeColors.Count - 1);
                int humidityIndex = Math.Clamp((int)Math.Floor((BiomeColors[tempIndex].Count) * humidity), 0, BiomeColors[tempIndex].Count - 1);

                return BiomeColors[tempIndex][humidityIndex];
            }
            
        }
    }
}
