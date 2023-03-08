using Final_Project.Scripts.Collectibles;
using Final_Project.Scripts.FileManagers;
using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Screens
{
    public class ScreenMainMenu : Screen
    {
        public TextBox PlayerNameTextBox;
        public Sprite BackgroundSprite;
        public PlayButton PlayButton;
        protected override void UpdateScreen()
        {
            PlayerNameTextBox.Update();
            if (GameManager.DebugMode)
            {
                DebugCommands();
            }
            if (PlayButton.CurrentSprite.HitBox.Contains(GameManager.CurrentMouseState.X, GameManager.CurrentMouseState.Y))
            {
                if (GameManager.CurrentMouseState.LeftButton == ButtonState.Pressed
                    && GameManager.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    StartGame();
                }
            }
            BackgroundSprite.Update();
            PlayButton.Update();
        }

        private void StartGame()
        {
            GameManager.PlayerName = PlayerNameTextBox.Text;
            GameManager.ScreenGame.ResetGame();
            GameManager.ActiveScreen = GameManager.ScreenGame;
        }

        private void DebugCommands()
        {
            
        }

        public void Initialize(ContentManager Content)
        {
            TextureManager.MainMenuBackgroundTexture = Content.Load<Texture2D>(FilePaths.MainMenuBackgroundPath);
            TextureManager.PlayButtonDeSelectedTexture = Content.Load <Texture2D>(FilePaths.PlayButtonDeSelectedPath);
            TextureManager.PlayButtonSelectedTexture = Content.Load<Texture2D>(FilePaths.PlayButtonSelectedPath);
            
            BackgroundSprite = new Sprite(TextureManager.MainMenuBackgroundTexture,
                new Vector2(TextureManager.MainMenuBackgroundTexture.Width, TextureManager.MainMenuBackgroundTexture.Height),
                new Vector2(0, 0));
            PlayerNameTextBox = new TextBox()
            {
                TopLeftPosition = new Vector2(20, 60),
                Size = new Vector2(200, 40)
            };
            PlayerNameTextBox.Text = GameManager.PlayerName;
            InitializePlayButton();
        }

        private void InitializePlayButton()
        {
            PlayButton = new PlayButton();
            PlayButton.SelectedSprite = new Sprite(TextureManager.PlayButtonSelectedTexture,
                new Vector2(TextureManager.PlayButtonSelectedTexture.Width,
                TextureManager.PlayButtonSelectedTexture.Height), new Vector2(20, 450));
            PlayButton.DeSelectedSprite = new Sprite(TextureManager.PlayButtonDeSelectedTexture,
                new Vector2(TextureManager.PlayButtonDeSelectedTexture.Width,
                TextureManager.PlayButtonDeSelectedTexture.Height), new Vector2(20, 450));
            PlayButton.CurrentSprite = PlayButton.DeSelectedSprite;
        }

        protected override void DrawScreen()
        {
            BackgroundSprite.Draw();
            PlayerNameTextBox.Draw();
            PlayButton.Draw();
        }

    }
}
