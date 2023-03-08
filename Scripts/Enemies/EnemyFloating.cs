using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Enemies
{
    public class EnemyFloating: GameObject
    {

        public float AngleLimit { get; set; } = 30;
        public bool IsRotatingRight { get; set; } = true;
        public float AngleIncrement = 1;
        public override void Update()
        {
            base.Update();
            //5 is the buffer to make hitbox smaller
            HitBox = new Rectangle((int)ObjectSprite.TopLeftPosition.X + 5, (int)ObjectSprite.TopLeftPosition.Y + 5,
                (int)ObjectSprite.Size.X - 5, (int)ObjectSprite.Size.Y - 5);

            if (Velocity.X > 0)
            {
                ObjectSprite.SpriteEffect = SpriteEffects.None;
            }
            else
            {
                ObjectSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (TopLeftPosition.X > GameManager.WindowSize.X - Size.X)
            {
                Velocity = new Vector2(-GameManager.FloatingEnemyVelocity, 0);
            }
            if (TopLeftPosition.X < 0)
            {
                Velocity = new Vector2(GameManager.FloatingEnemyVelocity, 0);
            }
            SetAngle();
        }

        private void SetAngle()
        {
            if (IsRotatingRight)
            {
                Angle += AngleIncrement;
                if (Angle > AngleLimit)
                {
                    IsRotatingRight = false;
                }
            }
            else
            {
                Angle -= AngleIncrement;
                if (Angle < -AngleLimit)
                {
                    IsRotatingRight = true;
                }
            }
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
