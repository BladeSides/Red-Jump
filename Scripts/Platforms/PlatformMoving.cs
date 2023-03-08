using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Platforms
{
    public class PlatformMoving : Platform
    {
        //For Moving Platforms
        public Direction Direction = Direction.Left;
        public int MoveSpeed { get; set; } = 3;
        public override void Update()
        {
            base.Update();
            MovePlatform();
        }
        public override void Collide(Player player)
        {
            base.Collide(player);
            if (player.IsAbovePlatform(this))
            {

                if (player.Velocity.Y >= 0)
                {
                    StandOnMovingPlatform(player);
                }
            }
            else
            {
                PushAgainstPlatform(player);
            }
        }

        private void MovePlatform()
        {
            if (Direction == Direction.Left)
            {
                Velocity = new Vector2(-MoveSpeed, 0);
            }
            else
            {
                Velocity = new Vector2(MoveSpeed, 0);
            }

            if (TopLeftPosition.X <= 0)
            {
                Direction = Direction.Right;
            }
            if (TopLeftPosition.X + Size.X >= GameManager.WindowSize.X)
            {
                Direction = Direction.Left;
            }

            UpdateHitBox();
        }

        private void UpdateHitBox()
        {
            HitBox = new Rectangle(TopLeftPosition.ToPoint(), Size.ToPoint());
        }

    }
}
