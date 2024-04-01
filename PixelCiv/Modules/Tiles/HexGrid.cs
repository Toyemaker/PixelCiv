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
using static System.Net.Mime.MediaTypeNames;

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
        
        public HexTile this[Point point]
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

        public void Spread(Point point, int radius, int strength, int category)
        {
            if (this[point].GetCategory(category) < strength)
            {
                this[point].SetCategory(category, strength);
            }

            int maxSteps = (int)Math.Round(radius / (strength + 1.0) * Math.Log(radius + 1.0) / 2.0);
            float chance = 0.5f;

            List<Point> points = SpreadAdjacent(point, strength, chance, category, maxSteps).ToList();
            
            for (int i = strength; i >= 0; i--)
            {
                List<Point> nextPoints = points.ToList();
                points.Clear();
                foreach (var tile in nextPoints)
                {
                    foreach (var next in GetAdjacent(tile))
                    {
                        if (this[next].GetCategory(category) < i)
                        {
                            this[next].SetCategory(category, i);
                        }

                        points.AddRange(SpreadAdjacent(next, i, chance, category, maxSteps).ToList());
                    }
                }
            }

            foreach(HexTile tile in _tileDictionary.Values)
            {
                tile.IsModified = false;                
            }
        }

        public IEnumerable<Point> SpreadAdjacent(Point point, int strength, float chance, int category, int maxSteps, int step = 1)
        {
            if (step >= maxSteps)
            {
                yield return point;
                yield break;
            }

            bool passed = true;

            foreach (Point tile in GetAdjacent(point))
            {
                if (this[tile].IsModified || this[tile].GetCategory(category) > strength)
                {
                    continue;
                }

                if (_random.NextSingle() < chance)
                {
                    this[tile].SetCategory(category, strength);
                    this[tile].IsModified = true;

                    foreach (Point next in SpreadAdjacent(tile, strength, chance, category, maxSteps, step + 1))
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

        public Color GetBiomeColor(int temperature, int altitude, int humidity)
        {
            if (temperature == 5)
            {
                return Color.White;
            }
            else if (altitude < 2)
            {
                return new Color(0, 0, (altitude + 1) * 255 / 2);
            }
            else if (altitude == 2)
            {
                return BiomeColors[Math.Clamp(temperature + altitude - 3, 0, 5)][Math.Clamp(humidity + 1, 0, BiomeColors[Math.Clamp(temperature + altitude - 3, 0, 5)].Count - 1)];
            }

            return BiomeColors[Math.Clamp(temperature + altitude - 3, 0, 5)][Math.Clamp(humidity, 0, BiomeColors[Math.Clamp(temperature + altitude - 3, 0, 5)].Count - 1)];
        }

        private bool Contains(Point point)
        {
            return _tileDictionary.ContainsKey(point);
        }
    }
}