using csproject2024.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace csproject2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch _UISpriteBatch;
        private GameManager _gameManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Globals.GraphicsDeviceManager = _graphics;
            Globals.GraphicsDevice = GraphicsDevice;
            Globals.Content = Content;

            int monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            int centerX = (monitorWidth - 1920) / 2;
            int centerY = (monitorHeight - 1080) / 2;

            Window.Position = new Point(centerX, centerY);

            _gameManager = new();
            _gameManager.Init();
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;        
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;

            _UISpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.UISpriteBatch = _UISpriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameManager.update();
            InputManager.Update(Keyboard.GetState(),Mouse.GetState());

            Globals.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _gameManager.draw();
            base.Draw(gameTime);
        }
    }
}
