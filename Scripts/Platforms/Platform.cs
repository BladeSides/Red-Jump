using Final_Project.Scripts.Collectibles;
using Final_Project.Scripts.Enemies;
using Final_Project.Scripts.Managers;
using Final_Project.Scripts.PlatformObjects;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Platforms
{
    public enum Direction
    {
        Left,
        Right
    }
    public enum PlatformType
    {
        Solid,
        PassThrough,
        Moving,
        Breakable,
        Turret
    }
    public abstract class Platform : GameObject
    {
        public virtual PlatformType PlatformType { get; set; } = PlatformType.PassThrough;

        //For Breakable Platforms
        public bool Breaking { get; set; } = false;
        public bool Respawning { get; set; } = false;
        //Has a collectible or no
        public bool HasPlatformObject { get; set; } = false;
        //Has an enemy or no
        public bool HasEnemy { get; set; } = false;
        public EnemyPlatform Enemy { get; set; }
        public PlatformObject PlatformObject { get; set; }
        public override void Draw()
        {
            base.Draw();
            if (HasPlatformObject)
            {
                PlatformObject.Draw();
            }
            if (HasEnemy)
            {
                Enemy.Draw();
                if (GameManager.DebugMode)
                    Enemy.DrawHitbox();
            }
        }

        public override void Update()
        {
            base.Update();
            if (HasPlatformObject)
            {
                PlatformObject.UpdatePlatformObject(this);
            }
            if (HasEnemy)
            {
                Enemy.Update();

                if (Enemy.Velocity.X > 0)
                {
                    Enemy.ObjectSprite.SpriteEffect = SpriteEffects.None;
                }
                else
                {
                    Enemy.ObjectSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                }
                if (Enemy.TopLeftPosition.X > this.TopLeftPosition.X + this.Size.X - Enemy.Size.X + 35)
                {
                    Enemy.Velocity = new Vector2(-GameManager.WalkingEnemyVelocity, 0);
                }
                if (Enemy.TopLeftPosition.X < this.TopLeftPosition.X - 20)
                {
                    Enemy.Velocity = new Vector2(GameManager.WalkingEnemyVelocity, 0);
                }
                Enemy.Velocity += this.Velocity;
                Enemy.TopLeftPosition = new Vector2(Enemy.TopLeftPosition.X, this.TopLeftPosition.Y - Enemy.Size.Y);
                if (Enemy.HitBox.Intersects(GameManager.Player.HitBox) && GameManager.Player.Velocity.Y <= 0)
                {
                    Enemy.Collide(GameManager.Player);
                }
                else if (Enemy.HitBox.Intersects(GameManager.Player.HitBox))
                {
                    Enemy.isActive = false;
                    HasEnemy = false;
                    GameManager.Player.KillEnemy();
                    ScoreUI.CurrentScore += 1000;
                    UIManager.AddPopUpText(Enemy.TopLeftPosition, "+1000");
                }
            }
        }

        public virtual void SlideDown(int amountToMove)
        {
            TopLeftPosition = new Vector2(TopLeftPosition.X, TopLeftPosition.Y + amountToMove);
            HitBox = new Rectangle(new Point(HitBox.X, HitBox.Y + amountToMove), HitBox.Size);

        }
        public virtual void Collide(Player player)
        {
            if (HasPlatformObject)
            {
                if (PlatformObject.HitBox.Intersects(player.HitBox))
                {
                    PlatformObject.Collect();
                }
            }
        }

        public virtual void PushAgainstPlatform(Player player)
        {
            if (player.Velocity.Y < 0)
            {
                player.Acceleration = new Vector2(player.Acceleration.X, 0);
                player.Velocity = new Vector2(this.Velocity.X, 1); //Collide with solid platform, make it move 
            
            }
            if (PlatformType == PlatformType.Moving)
            {
                player.TopLeftPosition += new Vector2(Velocity.X, 0); //Push player against the moving platform tooq
            }
        }

        public virtual void StandOnMovingPlatform(Player player)
        {
            player.IsGrounded = true;
            //player.Acceleration = new Vector2(player.Acceleration.X, 0); //Removes Gravity

            player.TopLeftPosition = new Vector2(player.TopLeftPosition.X + Velocity.X,
            TopLeftPosition.Y - GameManager.PlayerSize.Y + GameManager.GroundBuffer);
            //(makes sure it's always colliding with the ground (if it was going up)
            //ObjectSprite.TopLeftPosition = this.TopLeftPosition;
        }
        public virtual void StandOnPlatform(Player player)
        {
            player.IsGrounded = true;

            player.TopLeftPosition = new Vector2(player.TopLeftPosition.X,
                    TopLeftPosition.Y - GameManager.PlayerSize.Y + GameManager.GroundBuffer);
            //(makes sure it's always colliding with the ground (if it was going up)
            //ObjectSprite.TopLeftPosition = this.TopLeftPosition;
        }

    }
}
