using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Platforms
{
    public class PlatformSolid : Platform
    {
        public override PlatformType PlatformType { get; set; } = PlatformType.PassThrough;
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
