using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.UIObjects
{
    public class PopUpText
    {
        public string Text { get; set; }
        public Vector2 TopLeftPosition { get; set; }
        public float Timer { get; set; } = 0;
        public float TimeToShow { get; set; } = 45;
        public Color Color { get; set; } = Color.White;
        public bool isActive { get; set; } = true;
        public float ColorValue { get { return (TimeToShow - Timer) / TimeToShow; } }

        public void Draw()
        {
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, Text, TopLeftPosition, Color);
        }

        public void Update()
        {
            Timer++;
            if (Timer > TimeToShow)
            {
                isActive = false;
            }
            Color = new Color(ColorValue, ColorValue, ColorValue, ColorValue);
        }
    }
}
