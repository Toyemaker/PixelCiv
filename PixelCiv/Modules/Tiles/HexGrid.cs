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
        private Dictionary<Biome, List<Biome>> _biomeWeightList;

        private int _gridRadius;

        public HexGrid()
        {
            _tileDictionary = new Dictionary<Point, HexTile>();
            _biomeWeightList = new Dictionary<Biome, List<Biome>>()
            {
                { 
                    Biome.Coastal, new List<Biome>()
                    {
                        Biome.Coastal,
                        Biome.Desert,
                        Biome.Grassland,
                        Biome.Ocean,
                        Biome.Swamp,
                        Biome.Tundra,
                    }
                },
                {
                    Biome.Desert, new List<Biome>()
                    {
                        Biome.Coastal,
                        Biome.Desert,
                        Biome.Grassland,
                        Biome.Mountains,
                    }
                },
                {
                    Biome.Forest, new List<Biome>()
                    {
                        Biome.Forest,
                        Biome.Grassland,
                        Biome.Jungle,
                        Biome.Mountains,
                        Biome.Swamp,
                        Biome.Taiga,
                    }
                },
                {
                    Biome.Grassland, new List<Biome>()
                    {
                        Biome.Coastal,
                        Biome.Desert,
                        Biome.Grassland,
                        Biome.Mountains,
                        Biome.Tundra,
                    }
                },
                {
                    Biome.Jungle, new List<Biome>()
                    {
                        Biome.Forest,
                        Biome.Jungle,
                        Biome.Mountains,
                        Biome.Swamp,
                    }
                },
                {
                    Biome.Mountains, new List<Biome>()
                    {
                        Biome.Desert,
                        Biome.Forest,
                        Biome.Grassland,
                        Biome.Jungle,
                        Biome.Mountains,
                        Biome.Swamp,
                        Biome.Taiga,
                        Biome.Tundra,
                    }
                },
                {
                    Biome.Ocean, new List<Biome>()
                    {
                        Biome.Ocean,
                        Biome.Coastal,
                        Biome.Swamp,
                    }
                },
                {
                    Biome.Swamp, new List<Biome>()
                    {
                        Biome.Coastal,
                        Biome.Forest,
                        Biome.Jungle,
                        Biome.Mountains,
                        Biome.Ocean,
                        Biome.Swamp,
                        Biome.Tundra,
                    }
                },
                {
                    Biome.Taiga, new List<Biome>()
                    {
                        Biome.Forest,
                        Biome.Mountains,
                        Biome.Taiga,
                        Biome.Tundra,
                    }
                },
                {
                    Biome.Tundra, new List<Biome>()
                    {
                        Biome.Coastal,
                        Biome.Grassland,
                        Biome.Mountains,
                        Biome.Swamp,
                        Biome.Taiga,
                        Biome.Tundra,
                    }
                },
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
        }

        public IEnumerable<HexTile> GetAdjacentTiles(Point point)
        {
            if (_tileDictionary.ContainsKey(point))
            {
                if (_tileDictionary.ContainsKey(point - new Point(0, -1)))
                {
                    yield return _tileDictionary[point - new Point(0, -1)];
                }
                if (_tileDictionary.ContainsKey(point - new Point(1, -1)))
                {
                    yield return _tileDictionary[point - new Point(1, -1)];
                }
                if (_tileDictionary.ContainsKey(point - new Point(1, 0)))
                {
                    yield return _tileDictionary[point - new Point(1, 0)];
                }
                if (_tileDictionary.ContainsKey(point - new Point(0, 1)))
                {
                    yield return _tileDictionary[point - new Point(0, 1)];
                }
                if (_tileDictionary.ContainsKey(point - new Point(-1, 1)))
                {
                    yield return _tileDictionary[point - new Point(-1, 1)];
                }
                if (_tileDictionary.ContainsKey(point - new Point(-1, 0)))
                {
                    yield return _tileDictionary[point - new Point(-1, 0)];
                }
            }

        }
    }

    public enum Biome
    {
        Coastal,
        Desert,
        Forest,
        Grassland,
        Jungle,
        Mountains,
        Ocean,
        Swamp,
        Taiga,
        Tundra,
    }
}
