using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    //Holds the textures, they're loaded in through their respective classes
    //(For eg: player, platform manager etc.
    static public class TextureManager
    {
        //Player Textures
        public static Texture2D PlayerIdleSpriteTexture { get; set; }
        public static Texture2D PlayerRunSpriteTexture { get; set; }
        public static Texture2D PlayerJumpSpriteTexture { get; set; }

        //Platform Textures
        public static Texture2D PlatformBreakableTexture { get; set; }
        public static Texture2D PlatformBreakTexture { get; set; }
        public static Texture2D PlatformMovingTexture { get; set; }
        public static Texture2D PlatformPassThroughTexture { get; set; }
        public static Texture2D PlatformSolidTexture { get; set; }
        public static Texture2D PlatformStartingTexture { get; set; }

        public static Texture2D TurretTexture { get; set; }

        //Platform Object Textures

        public static Texture2D LowCoinTexture { get; set; }
        public static Texture2D HighCoinTexture { get; set; }
        public static Texture2D HighJumpTexture { get; set; }
        public static Texture2D LowGravityTexture { get; set; }
        public static Texture2D HealthPickupTexture { get; set; }

        //UI Textures
        public static Texture2D Health3Texture { get; set; }
        public static Texture2D Health2Texture { get; set; }
        public static Texture2D Health1Texture { get; set; }
        public static Texture2D Health0Texture { get; set; }

        //Enemy Textures
        public static Texture2D EnemyWalkTexture { get; set; }
        public static Texture2D EnemyFloatTexture { get; set; }

        //Background Textures
        public static Texture2D BackgroundBackTexture { get; set; }
        public static Texture2D BackgroundMidTexture { get; set; }
        public static Texture2D BackgroundForeTexture { get; set; }

        //Background for screens
        public static Texture2D MainMenuBackgroundTexture { get; set; }
        public static Texture2D EndBackgroundTexture { get; set; }

        //UI Buttons
        public static Texture2D PlayButtonDeSelectedTexture { get; set; }
        public static Texture2D PlayButtonSelectedTexture { get; set; }

        //Bullet Texture
        public static Texture2D BulletTexture { get; set; }
    }
}
