using Final_Project.Scripts.Enemies;
using Final_Project.Scripts.Platforms;
using Final_Project.Scripts.Screens;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Final_Project.Scripts.Managers
{
    public static class GameManager
    {
        //Debug Mode
        public static bool DebugMode { get; set; } = false;

        //Pause
        public static bool isPaused { get; set; } = false;
        //Drawing Values
        public static Point WindowSize { get; set; } = new Point(800, 600);
        public static SpriteBatch SpriteBatch { get; set; }

        //Background Color
        public static Color BackgroundColor { get; set; } = new Color(64, 78, 152); //the bg color from the image

        //Game Font
        public static SpriteFont GameFont { get; set; }
        public static string TimeString { get; set; }
        public static string ScoreString { get; set; } = "Score: ";

        //Screen
        public static ScreenGame ScreenGame { get; set; }
        public static ScreenMainMenu ScreenMainMenu { get; set; }
        public static ScreenEndGame ScreenEndGame { get; set; }
        public static Screen ActiveScreen { get; set; }

        //GameObjects
        public static Player Player { get; set; }

        //Game Values
        //Scoring Values
        public const int LowCoinScore = 200;
        public const int HighCoinScore = 500;

        public const int FlyingEnemyScore = 1500;
        public const int PlatformEnemyScore = 1000;

        public const int LowCoinTime = 1;
        public const int HighCoinTime = 3;


        //Values related to platforms
        public const int NumberOfPlatforms = 10; //Number of active platforms in game at a time
        public const int NumberOfPlatformObjects = 5; //Coin Low, Coin High, High Jump, Low Gravity, Health Pickup
        public const int TypesOfPlatforms = 4; //Types of platforms
        public const int OddsOfPassThrough = 1; //Makes Sure Pass Through is Generated More than other platforms
        public const int PlatformMoveRange = 5; //Range of platform movement speed
        public const int MinimumXDistancePlatforms = 150; //Minimum distance between two platforms on X-Axis

        public const int OddsOfEnemyOnPlatform = 3; //Odds are 1:3 of having an enemy on platform

        public const int CameraBuffer = 150; //The amount of height player can go up above half the screen before the camera moves 

        public static TurretPlatform Turret { get; set; }
        public static float DeltaTime { get; set; }

        //Enemy Related Values
        public const int WalkingEnemyVelocity = 1;
        public const int FloatingEnemyVelocity = 3;

        public const int NumberOfFloatingEnemies = 10;


        //Scoring related values
        public static string PlayerName { get; set; } = "ENTER NAME";
        public const float GameTime = 35;
        public static float GameTimer { get; set; } = 35;

        //Inputs
        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState PreviousKeyboardState;

        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        //Player Values
        public static Vector2 PlayerSize { get; set; } = new Vector2(80, 80);
        public const int PlayerLives = 3;

        public static float Gravity { get; set; } = 30; //30 is normal
        public const float NormalGravityValue = 30;
        public const float LowerGravityValue = 15;

        public const float JumpVelocity = 15;
        public const float MoveAcceleration = 25;
        public const float MoveDeceleration = 60;

        public static bool PlayerIsInvulnerable = false; //Don't let player get damaged when he respawns
        public static int PlayerInvulnerabilityTimer = 0; //Player invunerability timer
        public static int PlayerInvulnerabilityTime = 180;
        public const int RespawnInvulnerabilityTime = 180; //How long player can't be hit for once player respawned
        public static bool PlayerHasRespawned = false; //Once player has respawned
        
        public const int JumpHeight = 180; //Calculated using Trial and Error, No fancy math here

        //Game Buffer Values
        public const int GroundBuffer = 2; //The player is within this many pixels vertically of ground if it's grounded
        public const int VerticalBuffer = 20; //The amount of pixels player can have below platform to still stand on it 
        public const float PlayerAnimationVelocityBuffer = 0.5f; //Player animates only if value of X-Velocity squared is greater than this


        //Player Starting Position
        public static Vector2 StartingPosition { get { return new Vector2(WindowSize.X / 2, WindowSize.Y - 130); } }

        //High Score Related Values
        public static string HighScoreText { get; set; }
        public static int HighScore { get; set; } = 0;
        public static void MoveCamera(Player _player)
        {
            if (_player.TopLeftPosition.Y <= WindowSize.Y / 2 - CameraBuffer)
            {
                int amountToMove = (int)(WindowSize.Y / 2 - CameraBuffer - _player.TopLeftPosition.Y);
                _player.TopLeftPosition = new Vector2(_player.TopLeftPosition.X, _player.TopLeftPosition.Y + amountToMove);
                _player.HitBox = new Rectangle(new Point(_player.HitBox.X, _player.HitBox.Y + amountToMove), _player.HitBox.Size);

                foreach (Platform platform in PlatformManager.Platforms)
                {
                    platform.SlideDown(amountToMove);
                }
                foreach (EnemyFloating enemy in FloatingEnemyManager.FloatingEnemies)
                {
                    enemy.TopLeftPosition = new Vector2(enemy.TopLeftPosition.X, enemy.TopLeftPosition.Y + amountToMove);
                }


                BackgroundManager.MoveBackground(amountToMove);
                ScoreUI.Update(amountToMove);
                UIManager.MovePopUpTexts(amountToMove);
            }
        }
    }
}
