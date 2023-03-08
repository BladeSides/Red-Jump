using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Platforms
{
    public class PlatformBreakable : Platform
    {
        //For Breakable Platforms
        //Breaking means it started breaking and needs to be removed
        //Respawning mean it's now broken and you can't collide with it anymore
        //It will be respawned after it's broken
        public int Timer { get; set; } = 0;
        public int TimeToBreak { get; set; } = 90;
        public int TimeToRespawn { get; set; } = 45;
        public override void Update()
        {
            base.Update();
            
            if (Breaking || Respawning)
            {
                Timer++;
            }

            if (GameManager.PlayerIsInvulnerable)
            {
                Breaking = false;
                Respawning = false;
                HitBox = new Rectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y, (int)Size.X, (int)Size.Y);
                IsVisible = true;
                Timer = 0;
            }

            if (Timer > TimeToBreak && Breaking && !GameManager.PlayerIsInvulnerable) 
                //Break the platform
            {
                Breaking = false;
                Respawning = true;
                Timer = 0;
            }

            if (Timer > TimeToRespawn && Respawning) //Respawn
            {
                HitBox = new Rectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y, (int)Size.X, (int)Size.Y);
                IsVisible = true;
                Respawning = false;
                Timer = 0;
            }

            if (Respawning)
            {
                IsVisible = false;
                HitBox = new Rectangle(0, 0, 0, 0);
            }
        }


        public override void Collide(Player player)
        {
            base.Collide(player);
            if (player.IsAbovePlatform(this))
            {
                player.Acceleration = new Vector2(player.Acceleration.X, 0); //Removes Gravity

                if (player.Velocity.Y >= 0)
                {
                    Breaking = true;
                    StandOnPlatform(player);
                    //(makes sure it's always colliding with the ground (if it was going up)
                    //ObjectSprite.TopLeftPosition = this.TopLeftPosition;
                }
            }
        }
    }
}