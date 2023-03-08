using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Screens
{
    public abstract class Screen
    {
        public void Update(GameTime gameTime)
        {
            SetDeltaTime(gameTime);
            UpdateInputs();
            UpdateScreen();
            SoundManager.Update();
        }

        private void SetDeltaTime(GameTime gameTime)
        {
            GameManager.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw()
        {
            DrawScreen();
        }
        private void UpdateInputs()
        {
            GameManager.PreviousKeyboardState = GameManager.CurrentKeyboardState;
            GameManager.CurrentKeyboardState = Keyboard.GetState();
            GameManager.PreviousMouseState = GameManager.CurrentMouseState;
            GameManager.CurrentMouseState = Mouse.GetState();
        }

        protected abstract void UpdateScreen();

        protected abstract void DrawScreen();
    }
}
