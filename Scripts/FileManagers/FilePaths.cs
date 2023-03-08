using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.FileManagers
{
    public static class FilePaths
    {
        //Declaring File Paths
        //Player Paths
        public const string PlayerIdleSpritePath = "Sprites/Player/Player_Idle";
        public const string PlayerRunSpritePath = "Sprites/Player/Player_Run";
        public const string PlayerJumpSpritePath = "Sprites/Player/Player_Jump";

        //Platform Paths
        public const string PlatformPassThroughPath = "Sprites/Platforms/Platform_PassThrough";
        public const string PlatformStartingPath = "Sprites/Platforms/Platform_Starting";
        public const string PlatformBreakablePath = "Sprites/Platforms/Platform_Breakable";
        public const string PlatformBreakPath = "Sprites/Platforms/Platform_Break";
        public const string PlatformSolidPath = "Sprites/Platforms/Platform_Solid";
        public const string PlatformMovingPath = "Sprites/Platforms/Platform_Moving";
        public const string PlatformTurretPath = "Sprites/Turret/Turret";

        //Platform Object Paths
        public const string LowCoinPath = "Sprites/Collectibles/Coin_Low";
        public const string HighCoinPath = "Sprites/Collectibles/Coin_High";
        public const string HighJumpPath = "Sprites/Collectibles/High_Jump";
        public const string LowGravityPath = "Sprites/Collectibles/Low_Gravity";
        public const string HealthPickupPath = "Sprites/Collectibles/HealthPickup";

        //UI Paths
        public const string Health0Path = "Sprites/UI/Health_0";
        public const string Health1Path = "Sprites/UI/Health_1";
        public const string Health2Path = "Sprites/UI/Health_2";
        public const string Health3Path = "Sprites/UI/Health_3";

        //Enemy Paths
        public const string EnemyWalkPath = "Sprites/Enemies/EnemyWalk";
        public const string EnemyFloatPath = "Sprites/Enemies/EnemyFloat";

        //Background Paths
        public const string BackgroundBackPath = "Sprites/Backgrounds/BackgroundBack";
        public const string BackgroundMidPath = "Sprites/Backgrounds/BackgroundMid";
        public const string BackgroundForePath = "Sprites/Backgrounds/BackgroundFore";

        //Background for screens
        public const string MainMenuBackgroundPath = "Sprites/Backgrounds/MainMenu";
        public const string EndBackgroundPath = "Sprites/Backgrounds/EndBackground";

        //UI Buttons
        public const string PlayButtonDeSelectedPath = "Sprites/UI/PlayButton_DeSelected";
        public const string PlayButtonSelectedPath = "Sprites/UI/PlayButton_Selected";

        //Music Paths
        public const string MainSongPath = "Audio/Music/MainSong";
        public const string HighScoreSongPath = "Audio/Music/HighScore";
        public const string GameOverSongPath = "Audio/Music/GameOver";

        //Sound Effect Paths
        public const string JumpEffectPath = "Audio/Sound/JumpEffect";
        public const string PickupCoinEffectPath = "Audio/Sound/PickupCoinEffect";
        public const string PickupHealthEffectPath = "Audio/Sound/PickupHealthEffect";
        public const string PlayerHitEffectPath = "Audio/Sound/PlayerHitEffect";
        public const string PowerUpEffectPath = "Audio/Sound/PowerUpEffect";
        public const string KillEffectPath = "Audio/Sound/KillEffect";

        //Bullet File
        public const string BulletPath = "Sprites/Turret/Bullet";
    }
}
