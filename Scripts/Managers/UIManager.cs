using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    public static class UIManager
    {
        public static List<PopUpText> PopUpTexts { get; set; } = new List<PopUpText>();
        public static void Update()
        {
            UpdateTimeLeft();
            UpdateScoreString();
            foreach (PopUpText popUpText in PopUpTexts)
            {
                popUpText.Update();
            }
            DeleteInactivePopUpTexts();
        }

        private static void DeleteInactivePopUpTexts()
        {
            int i = 0;
            while (i < PopUpTexts.Count)
            {
                if (!PopUpTexts[i].isActive)
                {
                    PopUpTexts.Remove(PopUpTexts[i]);
                }
                else
                {
                    i++;
                }
            }
        }

        private static void UpdateScoreString()
        {
            GameManager.ScoreString = ($"Score: {ScoreUI.CurrentScore}");
        }

        public static void Draw()
        {
            DrawTime();
            DrawScore();
            if(GameManager.HighScore > 0)
                DrawHighScore();
            HealthUI.Draw();
            foreach (PopUpText popUpText in PopUpTexts)
            {
                popUpText.Draw();
            }
        }

        private static void DrawHighScore()
        {
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, 
                ($"HScore: {GameManager.HighScore}"),
            new Vector2(GameManager.WindowSize.X - GameManager.TimeString.Length * 16 - 20,
            73),
            Color.White); //13 is the character
        }

            private static void DrawScore()
        {
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, GameManager.ScoreString, 
            new Vector2(GameManager.WindowSize.X - GameManager.TimeString.Length * 16 - 20, 
            43),
            Color.White); //13 is the character 
        }

        private static void UpdateTimeLeft()
        {
            GameManager.GameTimer -= GameManager.DeltaTime;
            GameManager.TimeString = ($"Time Left: {String.Format("{0:0.00}", GameManager.GameTimer)}");
        }
        private static void DrawTime()
        {
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, GameManager.TimeString, 
            new Vector2(GameManager.WindowSize.X - GameManager.TimeString.Length*16 - 20, 10),
            Color.White); //13 is the character size
        }

        public static void AddPopUpText(Vector2 TopLeftPosition, string Text)
        {
            PopUpText popUpText = new PopUpText()
            {
                TopLeftPosition = TopLeftPosition,
                Text = Text
            };
            PopUpTexts.Add(popUpText);
        }

        public static void MovePopUpTexts(int amountToMove)
        {
            foreach (PopUpText popUpText in PopUpTexts)
            {
                popUpText.TopLeftPosition += new Vector2(0, amountToMove);
            }
        }
    }
}
