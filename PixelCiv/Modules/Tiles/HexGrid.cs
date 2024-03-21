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
        private Dictionary<Biome, List<Biome>> _biomeRestrictionDict;

        private int _gridRadius;

        public HexGrid()
        {
            _tileDictionary = new Dictionary<Point, HexTile>();
            _biomeRestrictionDict = new Dictionary<Biome, List<Biome>>()
            {
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

            while (_tileDictionary.Where(a => a.Value.Biome == Biome.None).Count() > 0)
            {
                KeyValuePair<Point, HexTile> tile = _tileDictionary.Where(a => a.Value.Biome == Biome.None).OrderBy(a => GetEntropy(GetValidBiomes(a.Key))).FirstOrDefault();

                Random random = new Random();

                IEnumerable<Biome> biomes = GetValidBiomes(tile.Key);
                int test = GetEntropy(biomes);

                int i = random.Next(biomes.Count());

                tile.Value.Biome = biomes.ElementAt(i);
            }
        }

        public IEnumerable<Biome> GetAdjacentBiomes(Point point)
        {
            if (_tileDictionary.ContainsKey(point))
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = Math.Max(-(1 + y), -1); x <= Math.Min(1 - y, 1); x++)
                    {
                        if (x == 0 && y == 0)
                        {
                            break;
                        }

                        if (_tileDictionary.ContainsKey(point - new Point(x, y)))
                        {
                            yield return _tileDictionary[point - new Point(x, y)].Biome;
                        }
                        else
                        {
                            yield return Biome.Ocean;
                        }
                    }
                }
            }

        }

        public IEnumerable<Biome> GetValidBiomes(Point point)
        {
            List<Biome> biomes = new List<Biome>()
            {
            };

            foreach (Biome biome in GetAdjacentBiomes(point))
            {
                for (int i = 0; i < biomes.Count; i++)
                {
                    if (!_biomeRestrictionDict[biome].Contains(biomes[i]))
                    {
                        biomes.Remove(biomes[i]);
                    }
                }
            }

            return biomes;
        }

        public int GetEntropy(IEnumerable<Biome> possibleBiomes)
        {
            return possibleBiomes.Count();
        }
    }

    public enum Biome
    {
    }
}
