using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Enemies
{
    public class EnemyPlatform:GameObject
    {
        public override void Update()
        {
            base.Update();
            //15 is the buffer to make hitbox smaller
            HitBox = new Rectangle((int)ObjectSprite.TopLeftPosition.X + 15, (int)ObjectSprite.TopLeftPosition.Y,
                (int)ObjectSprite.Size.X - 15, (int)ObjectSprite.Size.Y);
        }

        public void Collide(Player player)
        {
            if (!GameManager.PlayerIsInvulnerable && GameManager.Player.CanCollideWithPlatforms)
            {
                if (GameManager.Player.Lives > 0)
                {
                    SoundManager.PlaySoundEffect(SoundManager.PlayerHitEffect);

                    GameManager.PlayerIsInvulnerable = true;
                    GameManager.PlayerInvulnerabilityTimer = 0;
                    GameManager.PlayerInvulnerabilityTime = GameManager.RespawnInvulnerabilityTime;
                    GameManager.Player.Lives--;
                    player.Velocity = new Vector2(0, player.Velocity.Y);
                    player.Acceleration = new Vector2(0, player.Acceleration.Y);
                }
            }
        }
    }
}
