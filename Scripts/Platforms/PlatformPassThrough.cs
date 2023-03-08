using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Platforms
{
    public class PlatformPassThrough : Platform
    {
        public override PlatformType PlatformType { get; set; } = PlatformType.PassThrough;
        public override void Collide(Player player)
        {
            base.Collide(player);
            if (player.IsAbovePlatform(this))
            {
                player.IsGrounded = true;
                player.Acceleration = new Vector2(player.Acceleration.X, 0); //Removes Gravity

                if (player.Velocity.Y >= 0)
                {
                    StandOnPlatform(player);
                }
            }
        }
    }
}
