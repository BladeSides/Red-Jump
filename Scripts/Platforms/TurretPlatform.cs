using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Platforms
{
    public class TurretPlatform: Platform
    {
        public float Timer { get; set; } = 0;
        public float TimePerBullet { get; set; } = 3;

        public List<Bullet> Bullets = new List<Bullet>();
        public override void SlideDown(int amountToMove)
        {
            
        }

        public override void Update()
        {
            base.Update();
            foreach (Bullet bullet in Bullets)
            {
                bullet.Update();
            }
            Timer += GameManager.DeltaTime;
            if (Timer > TimePerBullet)
            {
                Timer = 0;
                Bullet bullet = new Bullet();
                bullet.ObjectSprite = new Sprite(TextureManager.BulletTexture,
                    new Vector2(TextureManager.BulletTexture.Width, TextureManager.BulletTexture.Height),
                    this.TopLeftPosition + new Vector2(this.Size.X / 2, 0)
                    - new Vector2(TextureManager.BulletTexture.Width / 2, TextureManager.BulletTexture.Height / 2));
                Vector2 DirectionVector = GameManager.Player.TopLeftPosition - bullet.TopLeftPosition;
                if (DirectionVector == Vector2.Zero)
                {
                    return;
                }
                DirectionVector.Normalize();
                bullet.Velocity = DirectionVector * bullet.MoveSpeed;
                Bullets.Add(bullet);
            }

            DeleteInactiveBullets();
        }

        private void DeleteInactiveBullets()
        {
            int i = 0;
            while (i < Bullets.Count)
            {
                if (!Bullets[i].isActive)
                {
                    Bullets.Remove(Bullets[i]);
                }
                else
                {
                    i++;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            foreach (Bullet bullet in Bullets)
                bullet.Draw();
        }

        public override void Collide(Player player)
        {
            base.Collide(player);
            if (player.IsAbovePlatform(this))
            {
                if (player.Velocity.Y >= 0)
                {
                    StandOnPlatform(player);
                }
            }
            else
            {
                PushAgainstPlatform(player);
            }
        }

    }
}
