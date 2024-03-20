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

        public PixelCivGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            GameData.Load(Content, GraphicsDevice);

            _scene = new TestScene();
            _input = new Input();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _input.SetKeyboardState(Keyboard.GetState());
            _input.SetMouseState(Mouse.GetState());
            _input.SetMousePosition(Mouse.GetState().Position.ToVector2());

            _scene.Interact(_input, gameTime);
            _scene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _scene.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
