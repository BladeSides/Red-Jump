using Final_Project.Scripts;
using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Public random
        Random _random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GameManager.WindowSize.X;
            _graphics.PreferredBackBufferHeight = GameManager.WindowSize.Y;
        }

        protected override void Initialize()
        {
            Initializer.Initialize(Content);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Setting SpriteBatch as a Constant
            GameManager.SpriteBatch = _spriteBatch;

            Initializer.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if(GameManager.ActiveScreen != GameManager.ScreenGame)
                    Exit();
            }
            GameManager.ActiveScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameManager.BackgroundColor);

            _spriteBatch.Begin();

            GameManager.ActiveScreen.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}