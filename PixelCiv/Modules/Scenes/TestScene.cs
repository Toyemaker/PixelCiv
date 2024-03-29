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
                for (int i = 0; i < 10; i++)
                {
                    int y = random.Next(-grid.TileRadius, grid.TileRadius + 1);
                    int x = random.Next(Math.Max(-(grid.TileRadius + y), -grid.TileRadius), Math.Min(grid.TileRadius - y, grid.TileRadius) + 1);
                    //grid.Spread(new Point(x, y), 5);
                }
                //grid.Spread(new Point(0, -45), 50);
                //grid.Spread(new Point(0, 45), 50);

                //grid.Spread(new Point(0, 0), 50);

                grid.Spread(new Point(-50, 0), 50, 3);
                grid.Spread(new Point(-25, -25), 50, 3);
                grid.Spread(new Point(0, -50), 80, 5);
                grid.Spread(new Point(25, -50), 50, 3);
                grid.Spread(new Point(50, -50), 50, 3);


                grid.Spread(new Point(50, 0), 50, 3);
                grid.Spread(new Point(25, 25), 50, 3);
                grid.Spread(new Point(0, 50), 80, 5);
                grid.Spread(new Point(-25, 50), 50, 3);
                grid.Spread(new Point(-50, 50), 50, 3);
            }
            else if (input.IsKeyPressed(Keys.LeftControl))
            {
                foreach (HexTile tile in grid.GetChildren<HexTile>())
                {
                    tile.Temperature = 0;
                    tile.GetChild<Sprite2D>("sprite").Color = grid.GetBiomeColor(tile.Temperature, 0.25f, 0);
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
