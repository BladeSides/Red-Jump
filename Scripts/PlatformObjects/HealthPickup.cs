using Final_Project.Scripts.Collectibles;
using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.PlatformObjects
{
    internal class HealthPickup : PlatformObject
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
            if (GameManager.Player.Lives < 3)
            {
                base.Collect();
                SoundManager.PlaySoundEffect(SoundManager.PickupHealthEffect);

                GameManager.Player.Lives++;
            }
        }
    }
}
