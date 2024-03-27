using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Graphics;
using PixelCiv.Core.UI;
using PixelCiv.Modules.Displays;
using PixelCiv.Modules.Logistics;
using PixelCiv.Modules.Tiles;
using System;
using System.Collections.Generic;
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

        private int _counter;

        public TestScene() 
        {
            _screenRoot = new GameObject();
            _screenRoot.AddComponent("logisticsManager", new LogisticsManager());


            _worldRoot = new GameObject();
            HexGrid grid = new HexGrid();
            _worldRoot.AddComponent("grid", grid);
            _screenRoot.AddComponent("display", new ResourceDisplay(grid.GetChild<ResourceManager>("resourceManager")));
            _screenRoot.GetChild<ResourceDisplay>("display").Transform.Position = new Vector2(400, 0);

            _screenRoot.AddComponent("buildingMenu", new BuildingMenu(_worldRoot.GetChild<HexGrid>("grid")));
            _screenRoot.GetChild<GameObject>("buildingMenu").Transform.Position = new Vector2(0, 480 - 86);

            _camera = new Camera();

            _tickInterval = 0.05f;
        }

        public bool Interact(Input input, GameTime gameTime)
        {
            HexGrid grid = _worldRoot.GetChild<HexGrid>("grid");
            if (input.IsKeyPressed(Keys.Space))
            {
                Random random = new Random();

                if (_counter < 1)
                {
                    // altitude
                    for (int i = 0; i < 10; i++)
                    {
                        int y = random.Next(-grid._gridRadius, grid._gridRadius + 1);
                        int x = random.Next(Math.Max(-(grid._gridRadius + y), -grid._gridRadius), Math.Min(grid._gridRadius - y, grid._gridRadius) + 1);
                        //grid.Spread(new Point(x, y), 0f, 1f, 0f, 8);
                    }

                    grid.Spread(new Point(0, 0), 0f, 1f, 0f, 8);
                }
                else if (_counter < 2)
                {
                    grid.Spread(new Point(0, -10), 1f, 0f, 0f, 20);
                    grid.Spread(new Point(0, -10), 0f, 1f, 0f, 8);
                    grid.Spread(new Point(0, 10), 1f, 0f, 0f, 20);
                    grid.Spread(new Point(0, 10), 0f, 1f, 0f, 8);
                }
                else
                {
                    // humidity
                    for (int i = 0; i < 10; i++)
                    {
                        int y = random.Next(-grid._gridRadius, grid._gridRadius + 1);
                        int x = random.Next(Math.Max(-(grid._gridRadius + y), -grid._gridRadius), Math.Min(grid._gridRadius - y, grid._gridRadius) + 1);

                        grid.Spread(new Point(x, y), 0f, 0f, 1f, 8);
                    }
                }

                _counter++;
            }
            else if (input.IsKeyPressed(Keys.LeftControl))
            {
                foreach (HexTile tile in grid.GetChildren<HexTile>())
                {
                    tile.GetChild<Sprite2D>("sprite").Color = grid.GetBiomeColor(tile.Temperature, tile.Altitude, tile.Humidity);
                }
            }
            if (!_screenRoot.Interact(input, gameTime))
            {
                input.SetMousePosition(_camera.ConvertToWorldPosition(Mouse.GetState().Position.ToVector2()));
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
