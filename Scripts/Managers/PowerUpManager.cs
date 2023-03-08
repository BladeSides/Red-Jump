using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    public static class PowerUpManager
    {
        //Powerup related values
        public static float HighJumpVelocity { get; set; } = 20;
        public static bool LowerGravity { get; set; } = false;
        public static bool LowerGravityPotion { get; set; } = false;

        public static int LowerGravityTimer { get; set; } = 0;
        public static int LowerGravityTime { get; set; } = 45;

        //How long gravity is lowered for each powerup
        public static int LowerGravityTimeHighJump { get; set; } = 45;
        public static int LowerGravityTimeLowGravity { get; set; } = 180;

        //Methods
        public static void UpdatePowerUps()
        {
            //For High Jump Powerup
            if (PowerUpManager.LowerGravity)
            {
                GameManager.Player.Velocity = new Vector2(GameManager.Player.Velocity.X, -PowerUpManager.HighJumpVelocity);
                PowerUpManager.LowerGravityTimer++;
                GameManager.Gravity = GameManager.LowerGravityValue;
            }
            //For Low Gravity Powerup
            else if (PowerUpManager.LowerGravityPotion)
            {
                PowerUpManager.LowerGravityTimer++;
                GameManager.Gravity = GameManager.LowerGravityValue;
            }
            else
            {
                GameManager.Gravity = GameManager.NormalGravityValue;
            }
            if (PowerUpManager.LowerGravityTimer > PowerUpManager.LowerGravityTime)
            {
                PowerUpManager.LowerGravityPotion = false;
                PowerUpManager.LowerGravity = false;
                PowerUpManager.LowerGravityTimer = 0;
            }
        }
    }
}
