using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts
{
    public class Bullet: GameObject
    {
        public int MoveSpeed { get; set; } = 3;
        public override void Update()
        {
            base.Update();
            HitBox = new Rectangle((int)this.TopLeftPosition.X, (int)this.TopLeftPosition.Y,
                (int)this.Size.X, (int)this.Size.Y);
            if (this.TopLeftPosition.X < 0 && Velocity.X < 0)
            {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
            }
            if (this.TopLeftPosition.X + this.Size.X > GameManager.WindowSize.X && Velocity.X > 0)
            {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
            }

            if (this.TopLeftPosition.Y < 0)
            {
                this.isActive = false;
            }
            if (HitBox.Intersects(GameManager.Player.HitBox) && !GameManager.PlayerIsInvulnerable)
            {
                if (GameManager.Player.Lives > 0)
                {
                    SoundManager.PlaySoundEffect(SoundManager.PlayerHitEffect);

                    GameManager.PlayerIsInvulnerable = true;
                    GameManager.PlayerInvulnerabilityTimer = 0;
                    GameManager.PlayerInvulnerabilityTime = GameManager.RespawnInvulnerabilityTime;
                    GameManager.Player.Lives--;
                }
                this.isActive = false;
            }
        }
    }
}
