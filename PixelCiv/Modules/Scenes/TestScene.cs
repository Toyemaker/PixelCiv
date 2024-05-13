using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using PixelCiv.Core.UI;
using PixelCiv.Modules.Displays;
using PixelCiv.Modules.Logistics;
using PixelCiv.Modules.Tiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Scenes
{
    public class TestScene
    {
        private GameObject _screenRoot;
        private GameObject _worldRoot;

        private Camera _camera;

        private float _timer;
        private float _tickInterval;

        public TestScene(ContentManager content, GraphicsDevice graphics) 
        {
            _screenRoot = new GameObject();
            _screenRoot.AddComponent("logisticsManager", new LogisticsManager());


            _worldRoot = new GameObject();
            HexGrid grid = new HexGrid();
            _worldRoot.AddComponent("grid", grid);
            _screenRoot.AddComponent("display", new ResourceDisplay(grid.GetChild<ResourceManager>("resourceManager")));
            _screenRoot.GetChild<ResourceDisplay>("display").Transform.Position = new Vector2(400, 0);

            _screenRoot.AddComponent("buildingMenu", new BuildingMenu(_worldRoot.GetChild<HexGrid>("grid")));
            _screenRoot.GetChild<GameObject>("buildingMenu").Transform.Position = new Vector2(0, GameData.ScreenResolution.Bottom - 86);

            _camera = new Camera();
            _screenRoot.AddComponent("camera", _camera);

            _tickInterval = 0.05f;
        }

        public bool Interact(Input input, GameTime gameTime)
        {
            HexGrid grid = _worldRoot.GetChild<HexGrid>("grid");
            if (input.IsKeyPressed(Keys.Space))
            {
                grid.Spread(new Point(-grid.TileRadius / 2, -grid.TileRadius / 2), grid.TileRadius, 3, 0);
                grid.Spread(new Point(grid.TileRadius / 2, -grid.TileRadius), grid.TileRadius, 3, 0);
                grid.Spread(new Point(0, -grid.TileRadius), grid.TileRadius, 5, 0);

                grid.Spread(new Point(grid.TileRadius / 2, grid.TileRadius / 2), grid.TileRadius, 3, 0);
                grid.Spread(new Point(-grid.TileRadius / 2, grid.TileRadius), grid.TileRadius, 3, 0);
                grid.Spread(new Point(0, grid.TileRadius), grid.TileRadius, 5, 0);

                Random random = new Random();

                for (int i = 0; i < 10; i++)
                {
                    int y = random.Next(-grid.TileRadius, grid.TileRadius + 1);
                    int x = random.Next(Math.Max(-(grid.TileRadius + y), -grid.TileRadius), Math.Min(grid.TileRadius - y, grid.TileRadius) + 1);

                    grid.Spread(new Point(x, y), grid.TileRadius / 3, 5, 1);
                }

                int max = grid.TileRadius / 3;
                for (int x = max; x >= -max; x--)
                {
                    if (grid[new Point(grid.TileRadius * x / max, -grid.TileRadius / 2 * x / max)].Altitude > 4)
                    {
                        x -= max;
                        continue;
                    }

                    grid.Spread(new Point(grid.TileRadius * x / max, -grid.TileRadius / 2 * x / max), grid.TileRadius / 4, 5, 2);
                }
                max = grid.TileRadius / 6;
                for (int x = -max; x <= max; x++)
                {
                    if (grid[new Point(grid.TileRadius * 2 / 3 * x / max, -grid.TileRadius / 3 * x / max - 20 - grid.TileRadius * 1 / 9)].Altitude > 4)
                    {
                        x += max;
                        continue;
                    }

                    grid.Spread(new Point(grid.TileRadius * 2 / 3 * x / max, -grid.TileRadius / 3 * x / max - 20 - grid.TileRadius * 1 / 9), grid.TileRadius * 2 / 9, 5, 2);
                }
                for (int x = -max; x <= max; x++)
                {
                    if (grid[new Point(grid.TileRadius * 2 / 3 * x / max, -grid.TileRadius / 3 * x / max + 20 + grid.TileRadius * 1 / 9)].Altitude > 4)
                    {
                        x += max;
                        continue;
                    }

                    grid.Spread(new Point(grid.TileRadius * 2 / 3 * x / max, -grid.TileRadius / 3 * x / max + 20 + grid.TileRadius * 1 / 9), grid.TileRadius * 2 / 9, 5, 2);
                }

                foreach (HexTile tile in grid.GetChildren<HexTile>())
                {
                    tile.Color = grid.GetBiomeColor(tile.Temperature, tile.Altitude, tile.Humidity);
                }
            }
            else if (input.IsKeyPressed(Keys.LeftControl))
            {
                foreach (HexTile tile in grid.GetChildren<HexTile>())
                {
                    tile.Temperature = 0;
                    tile.Altitude = 0;
                    tile.Humidity = 0;
                    tile.Color = grid.GetBiomeColor(tile.Temperature, 0, 0);
                }
            }

            if (!_screenRoot.Interact(input, gameTime))
            {
                input.SetMousePosition(_camera.ConvertToWorldPosition(input.GetMousePosition()));
                return _worldRoot.Interact(input, gameTime);
            }

            return true;
        }
        public void Update(GameTime gameTime)
        {
            _screenRoot.Update(gameTime);
            _worldRoot.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (_timer >= _tickInterval)
            {
                _screenRoot.FixedUpdate(_tickInterval);
                _worldRoot.FixedUpdate(_tickInterval);
                _timer -= _tickInterval;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.GetTransform());
            _worldRoot.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            _screenRoot.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
