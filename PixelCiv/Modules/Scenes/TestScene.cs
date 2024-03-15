using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Graphics;
using PixelCiv.Core.UI;
using PixelCiv.GameObjects;
using PixelCiv.Modules.Displays;
using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Scenes
{
    public class TestScene
    {
        private ResourceManager _resourceManager;
        private LogisticsManager _logisticsManager;

        private GameObject _screenRoot;
        private GameObject _worldRoot;

        private Camera _camera;

        private float _timer;
        private float _tickInterval;

        public TestScene() 
        {
            _screenRoot = new GameObject();
            _screenRoot.AddComponent(new Button());
            _screenRoot.AddComponent(_resourceManager = new ResourceManager());
            _screenRoot.AddComponent(_logisticsManager = new LogisticsManager());
            _screenRoot.AddComponent(new ResourceDisplay(_resourceManager).SetTransform(new Vector2(400, 0), Vector2.One, 0f));

            _worldRoot = new GameObject();
            HexGrid grid = new HexGrid();
            grid.ResourceManager = _resourceManager;
            _worldRoot.AddComponent(grid);

            _camera = new Camera();
        }

        public bool Interact(Input input, GameTime gameTime)
        {
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
