using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Final_Project.Scripts.Screens;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts
{
    public static class Initializer
    {
        public static void Initialize(ContentManager Content)
        {
            //Load High Score

            GameManager.HighScoreText = ReadHighScore(@"highscore.txt");
            GameManager.HighScore = GetHighScore(GameManager.HighScoreText);

            //Initialize player
            GameManager.Player = new Player();
            GameManager.Player.Lives = GameManager.PlayerLives;

            //Initialize Screens
            InitializeScreens();

            //Load Fonts
            GameManager.GameFont = Content.Load<SpriteFont>("SpriteFonts/GameFont");

            //Initialize Timer
            GameManager.TimeString = $"Time Left: {GameManager.GameTimer}";
        }

        private static int GetHighScore(string highScoreText)
        {
            string Highscore = "";
            foreach (char c in highScoreText)
            {
                if (Char.IsNumber(c))
                {
                    Highscore += c;
                }
            }
            if (Highscore.Length > 0)
            {
                return int.Parse(Highscore);
            }
            else
            {
                return 0;
            }
        }

        private static string ReadHighScore(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string Text = sr.ReadLine();
                    return Text;
                }
            }
            else
            {
                GameManager.HighScore = 0;
                string Text = "Invalid Score File - 0";
                return Text;

            }

        }

        private static void InitializeScreens()
        {
            GameManager.ScreenGame = new ScreenGame();
            GameManager.ScreenMainMenu = new ScreenMainMenu();
            GameManager.ScreenEndGame = new ScreenEndGame();
            GameManager.ActiveScreen = GameManager.ScreenMainMenu;
        }

        internal static void LoadContent(ContentManager Content)
        {
            //Load Textures and Sprites
            GameManager.Player.LoadPlayerSprites(Content);
            PlatformManager.LoadPlatformTextures(Content);
            PlatformObjectManager.LoadObjectTextures(Content);
            PlatformObjectManager.LoadEnemyTextures(Content);
            HealthUI.LoadTextures(Content);
            HealthUI.InitializeSprites();
            BackgroundManager.LoadTextures(Content);
            SoundManager.LoadSongs(Content);
            SoundManager.LoadSoundEffects(Content);
            GameManager.ScreenMainMenu.Initialize(Content);
            GameManager.ScreenEndGame.Initialize(Content);

            //Set Initial Sprites
            GameManager.Player.ObjectSprite = GameManager.Player.PlayerIdleSprite;

            //Initialize PlatformList
            PlatformManager.Platforms = new List<Platform>();
            PlatformManager.Platforms.Clear();
            PlatformManager.SpawnFirstPlatform();
        }
    }
}
