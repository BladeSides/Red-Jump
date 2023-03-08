using Final_Project.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.UIObjects
{
    public class PlayButton
    {
        public Sprite SelectedSprite { get; set; }
        public Sprite DeSelectedSprite { get; set; }

        public Sprite CurrentSprite { get; set; }

        public void Update()
        {
            if (CurrentSprite.HitBox.Contains(GameManager.CurrentMouseState.X, GameManager.CurrentMouseState.Y))
            {
                CurrentSprite = SelectedSprite;
            }
            else
            {
                CurrentSprite = DeSelectedSprite;
            }
        }

        public void Draw()
        {
            CurrentSprite.Draw();
        }
    }
}
