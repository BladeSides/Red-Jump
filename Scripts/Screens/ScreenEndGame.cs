using Final_Project.Scripts.FileManagers;
using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Screens
{
    public class ScreenEndGame: Screen
    {
        public Sprite BackgroundSprite;
        public String Text = "";
        protected override void DrawScreen()
        {
            BackgroundSprite.Draw();
            DrawText();
        }

        private void DrawText()
        {
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, Text,
                new Vector2(100, 100), Color.White);
        }

        protected override void UpdateScreen()
        {
            BackgroundSprite.Update();
            if (GameManager.CurrentKeyboardState.IsKeyDown(Keys.R) && GameManager.PreviousKeyboardState.IsKeyUp(Keys.R))
            {
                GameManager.ActiveScreen = GameManager.ScreenMainMenu;
                SoundManager.CurrentSong = SoundManager.SongType.MainSong;
            }
        }

        public void EndGame(int currentscore)
        {
            if (currentscore > GameManager.HighScore)
            {
                GameManager.HighScore = currentscore;
                Text = $"New High Score!\n{GameManager.PlayerName} - {currentscore}\n\nPress R to Restart";
                WriteHighScore(@"highscore.txt", currentscore);
                SoundManager.CurrentSong = SoundManager.SongType.HighScore;
            }
            else
            {
                Text = $"Your Score: \n{GameManager.PlayerName} - {currentscore}\n\nHigh Score:\n{GameManager.HighScoreText}\n\nPress R to Restart";
                SoundManager.CurrentSong = SoundManager.SongType.GameOver;
            }
        }

        private void WriteHighScore(string filePath, int currentscore)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                GameManager.HighScore = currentscore;
                GameManager.HighScoreText = $"{GameManager.PlayerName} - {currentscore}";
                sw.WriteLine($"{GameManager.PlayerName} - {currentscore}");
            }
        }

        public void Initialize(ContentManager Content)
        {
            TextureManager.EndBackgroundTexture = Content.Load<Texture2D>(FilePaths.EndBackgroundPath);
            BackgroundSprite = new Sprite(TextureManager.EndBackgroundTexture, new Vector2
                (TextureManager.EndBackgroundTexture.Width, TextureManager.EndBackgroundTexture.Height),
                new Vector2(0, 0));
        }
    }
}
