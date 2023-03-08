using Final_Project.Scripts.Managers;
using Final_Project.Scripts.PlatformObjects;
using Final_Project.Scripts.Platforms;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Collectibles
{
    public class Coin: PlatformObject
    {

        public override void UpdatePlatformObject(Platform platform)
        {
            base.UpdatePlatformObject(platform);
            TopLeftPosition = new Vector2((int)(platform.TopLeftPosition.X + platform.Size.X / 2
            - TextureManager.LowCoinTexture.Width / 2),
            (int)(platform.TopLeftPosition.Y
            - TextureManager.LowCoinTexture.Height));
        }

        public override void Collect()
        {
            SoundManager.PlaySoundEffect(SoundManager.PickupHealthEffect);

            if (ObjectSprite.Texture == TextureManager.LowCoinTexture)
            {
                GameManager.GameTimer += GameManager.LowCoinTime;
                UIManager.AddPopUpText(ObjectSprite.TopLeftPosition - new Vector2(0, 25), $"+{GameManager.LowCoinTime} Sec");
                ScoreUI.CurrentScore += GameManager.LowCoinScore;
                UIManager.AddPopUpText(ObjectSprite.TopLeftPosition, $"+{GameManager.LowCoinScore}");
            }
            else if (ObjectSprite.Texture == TextureManager.HighCoinTexture)
            {
                GameManager.GameTimer += GameManager.HighCoinTime;
                UIManager.AddPopUpText(ObjectSprite.TopLeftPosition - new Vector2(0, 25), $"+{GameManager.HighCoinTime} Sec");
                ScoreUI.CurrentScore += GameManager.HighCoinScore;
                UIManager.AddPopUpText(ObjectSprite.TopLeftPosition, $"+{GameManager.HighCoinScore}");
            }
            base.Collect();
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
