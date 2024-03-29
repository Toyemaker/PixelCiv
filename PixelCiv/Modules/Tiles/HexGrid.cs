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

        public int GridRadius { get; private set; }
        
        private HexTile this[int x, int y]
        {
            get
            {
                return _tileDictionary[new Point(x, y)];
            }
        }
        private HexTile this[Point point]
        {
            get
            {
                return _tileDictionary[point];
            }
        }


        public HexGrid()
        {
            _tileDictionary = new Dictionary<Point, HexTile>();

            GridRadius = 50;

            Generate();

            AddComponent("resourceManager", new ResourceManager());
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            return false;
        }

        public void Generate()
        {
            for (int y = -GridRadius; y <= GridRadius; y++)
            {
                for (int x = Math.Max(-(GridRadius + y), -GridRadius); x <= Math.Min(GridRadius - y, GridRadius); x++)
                {
                    int xPos = x + GridRadius;
                    int yPos = y + GridRadius / 2;
                    
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

        public void Spread(Point point, int radius)
        {
            this[point].Temperature = 5;
            this[point].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(5, 0.25f, 0f);

            SpreadDirection(point, 0, 5, 0.25f);
            SpreadDirection(point, 1, 5, 0.25f);
            SpreadDirection(point, 2, 5, 0.25f);
            SpreadDirection(point, 3, 5, 0.25f);
            SpreadDirection(point, 4, 5, 0.25f);
            SpreadDirection(point, 5, 5, 0.25f);

            //for (int r = 1; r <= 2 * radius; r++)
            //{
            //    for (int t = 0; t < r; t++)
            //    {
            //        for (int i = 0; i < 6; i++)
            //        {
            //            int x = Math.Sign(((i + 3) % 6 - 3) % 3) * r + Math.Sign(((i + 1) % 6 - 3) % 3) * t;
            //            int y = Math.Sign(((i + 5) % 6 - 3) % 3) * r + Math.Sign(((i + 3) % 6 - 3) % 3) * t;

            //            if (Contains(point + new Point(x, y)) && this[point + new Point(x, y)].Temperature > 0)
            //            {
            //                int temp = this[point + new Point(x, y)].Temperature;

            //                SpreadDirection(point + new Point(x, y), i, temp, radius / (2f * GridRadius));
            //            }
            //        }
            //    }
            //}
        }

        public void SpreadDirection(Point point, int direction, int strength, float decay)
        {
            int x = Math.Sign(((direction + 3) % 6 - 3) % 3);
            int y = Math.Sign(((direction + 5) % 6 - 3) % 3);

            if (Contains(point + new Point(x, y)) && this[point + new Point(x, y)].Temperature < strength)
            {
                if (_random.NextSingle() < decay)
                {
                    this[point + new Point(x, y)].Temperature = strength;
                }
                else
                {
                    this[point + new Point(x, y)].Temperature = strength - 1;
                }

                SpreadDirection(point + new Point(x, y), direction, this[point + new Point(x, y)].Temperature, decay * 0.9f);

                this[point + new Point(x, y)].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(this[point + new Point(x, y)].Temperature, 0.25f, 0f);
            }
        }

        public Color GetBiomeColor(int temperature, float altitude, float humidity)
        {
            if (altitude < 0.25)
            {
                return new Color(altitude, altitude * 2, 255);
            }
            else
            {
                int humidityIndex = (int)Math.Clamp((BiomeColors[temperature].Count - 1) * humidity, 0, BiomeColors[temperature].Count - 1);
                return BiomeColors[temperature][humidityIndex];
            }
            
        }

        private bool Contains(Point point)
        {
            return _tileDictionary.ContainsKey(point);
        }
    }
}