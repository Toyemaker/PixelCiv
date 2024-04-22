using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelCiv.Core;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using PixelCiv.Core.UI;
using PixelCiv.GameObjects;
using PixelCiv.GameObjects.Structures;
using PixelCiv.Modules.Logistics;
using PixelCiv.Modules.Scenes;
using System;
using System.Diagnostics;

namespace PixelCiv
{
    public class PixelCivGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TestScene _scene;
        private Input _input;
        private RenderTarget2D _renderTarget;

        public PixelCivGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Window.AllowUserResizing = true;

            GameData.ScreenResolution = new Rectangle(0, 0, 1280, 720);

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, GameData.ScreenResolution.Width, GameData.ScreenResolution.Height);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            GameData.Load(Content, GraphicsDevice);

            _scene = new TestScene(Content, GraphicsDevice);
            _input = new Input();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _input.SetKeyboardState(Keyboard.GetState());
            _input.SetMouseState(Mouse.GetState());

            Vector2 pos = Mouse.GetState().Position.ToVector2();
            pos /= _graphics.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            pos *= GameData.ScreenResolution.Size.ToVector2();

            _input.SetMousePosition(pos);

            _scene.Interact(_input, gameTime);
            _scene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            _graphics.GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _scene.Draw(_spriteBatch, gameTime);

            _graphics.GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, _graphics.GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
