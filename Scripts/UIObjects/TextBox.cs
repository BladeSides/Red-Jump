using Final_Project.Scripts.Managers;
using GeoSketch;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.UIObjects
{
    public class TextBox
    {
        public Vector2 TopLeftPosition { get; set; } 
        public Vector2 Size { get; set; }
        public Color FillNormalColor { get; set; } = Color.DarkGray;
        public Color FillSelectedColor { get; set; } = Color.Gray;
        public Color OutlineNormalColor { get; set; } = Color.Black;
        public Color OutlineSelectedColor { get; set; } = Color.Red;

        public bool HasBeenSelectedOnce { get; set; } = false;
        public Color FillColor { get; set; }
        public Color OutlineColor { get; set; }
        public int TextLengthLimit { get; set; } = 10;

        public bool isSelected { get; set; } = false; 
        public string Text { get; set; }
        public void Draw()
        {
            GameManager.SpriteBatch.DrawRectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y,
                (int)Size.X, (int)Size.Y, FillColor, OutlineColor, 3);
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, Text, TopLeftPosition +
                new Vector2(10, 10), Color.Black);
        }

        public void Update()
        {
            if (new Rectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y,
                (int)Size.X, (int)Size.Y).Contains(GameManager.CurrentMouseState.Position))
            {
                if (GameManager.CurrentMouseState.LeftButton == ButtonState.Pressed
                    && GameManager.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    isSelected = true;
                }
            }
            if (!new Rectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y,
            (int)Size.X, (int)Size.Y).Contains(GameManager.CurrentMouseState.Position))
            {
                if (GameManager.CurrentMouseState.LeftButton == ButtonState.Pressed
                    && GameManager.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    isSelected = false;
                }
            }
            if (isSelected)
            {
                FillColor = FillSelectedColor;
                OutlineColor = OutlineSelectedColor;
                AddInput();
            }

            else
            {
                FillColor = FillNormalColor;
                OutlineColor = OutlineNormalColor;
            }

        }

        private bool IsLetter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }


        private void AddInput()
        {
            Keys[] keys = GameManager.CurrentKeyboardState.GetPressedKeys();
            Keys[] previousKeys = GameManager.PreviousKeyboardState.GetPressedKeys();
            foreach (Keys key in keys)
            {
                if (!previousKeys.Contains(key))
                {
                    if (key != Keys.BrowserBack && key != Keys.Back)
                    {
                        if (Text.Length < TextLengthLimit || !HasBeenSelectedOnce)
                        {
                            if (!HasBeenSelectedOnce)
                            {
                                Text = "";
                                HasBeenSelectedOnce = true;
                            }

                            if (IsLetter((char)key))
                                Text += ($"{((char)key)}");
                        }
                    }
                    else
                    {
                        if (Text.Length > 0)
                            Text = Text.Substring(0, Text.Length - 1);
                    }
                }
            }
        }
    }
}
