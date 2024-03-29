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

        public int TileRadius { get; private set; }
        
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

            TileRadius = 50;

            Generate();

            AddComponent("resourceManager", new ResourceManager());
        }

        public override bool Interact(Input input, GameTime gameTime)
        {
            return false;
        }

        public void Generate()
        {
            for (int y = -TileRadius; y <= TileRadius; y++)
            {
                for (int x = Math.Max(-(TileRadius + y), -TileRadius); x <= Math.Min(TileRadius - y, TileRadius); x++)
                {
                    int xPos = x + TileRadius;
                    int yPos = y + TileRadius / 2;
                    
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

        public IEnumerable<Point> GetAdjacent(Point point)
        {
            foreach (int i in Enumerable.Range(0, 6).OrderBy(x => _random.Next()))
            {
                int x = Math.Sign(((i + 3) % 6 - 3) % 3);
                int y = Math.Sign(((i + 5) % 6 - 3) % 3);
                
                if (Contains(point + new Point(x, y)))
                {
                    yield return point + new Point(x, y);
                }
            }
        }

        public void Spread(Point point, int radius, int strength)
        {
            this[point].Temperature = strength;
            this[point].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(strength, 0.25f, 0f);

            int maxSteps = (int)Math.Round(radius / (strength + 1.0) * Math.Log(radius + 1.0) / 2.0);
            float chance = 1f;

            List<Point> points = SpreadAdjacent(point, strength, chance, maxSteps).ToList();
            
            for (int i = strength - 1; i >= 0; i--)
            {
                List<Point> nextPoints = points.ToList();
                points.Clear();
                foreach (var tile in nextPoints)
                {
                    foreach (var next in GetAdjacent(tile))
                    {
                        if (this[next].Temperature == 0)
                        {
                            this[next].Temperature = i;
                            this[next].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(i, 0.25f, 0f);
                        }
                        points.AddRange(SpreadAdjacent(next, i, chance, maxSteps).ToList());
                    }
                }
            }

            
        }

        public IEnumerable<Point> SpreadAdjacent(Point point, int strength, float chance, int maxSteps, int step = 1)
        {
            if (step >= maxSteps)
            {
                yield return point;
                yield break;
            }

            bool passed = true;

            foreach (Point tile in GetAdjacent(point))
            {
                if (this[tile].Temperature >= strength)
                {
                    continue;
                }

                if (_random.NextSingle() < chance)
                {
                    this[tile].Temperature = strength;
                    this[tile].GetChild<Sprite2D>("sprite").Color = GetBiomeColor(strength, 0.25f, 0f);

                    foreach (Point next in SpreadAdjacent(tile, strength, chance, maxSteps, step + 1))
                    {
                        yield return next;
                    }
                }
                else
                {
                    passed = false;
                }
            }

            if (!passed)
            {
                yield return point;
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