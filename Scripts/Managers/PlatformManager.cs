using Final_Project.Scripts.Enemies;
using Final_Project.Scripts.FileManagers;
using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    static public class PlatformManager
    {
        public static Random random = new Random();
        public static List<Platform> Platforms { get; set; }
        public static void LoadPlatformTextures(ContentManager Content)
        {
            TextureManager.PlatformPassThroughTexture = Content.Load<Texture2D>(FilePaths.PlatformPassThroughPath);
            TextureManager.PlatformSolidTexture = Content.Load<Texture2D>(FilePaths.PlatformSolidPath);
            TextureManager.PlatformMovingTexture = Content.Load<Texture2D>(FilePaths.PlatformMovingPath);
            TextureManager.PlatformBreakableTexture = Content.Load<Texture2D>(FilePaths.PlatformBreakablePath);
            TextureManager.PlatformBreakTexture = Content.Load<Texture2D>(FilePaths.PlatformBreakPath);
            TextureManager.PlatformStartingTexture = Content.Load<Texture2D>(FilePaths.PlatformStartingPath);
            TextureManager.TurretTexture = Content.Load<Texture2D>(FilePaths.PlatformTurretPath);
            TextureManager.BulletTexture = Content.Load<Texture2D>(FilePaths.BulletPath);
        }
        public static void SpawnPlatforms()
        {
            while (Platforms.Count != GameManager.NumberOfPlatforms)
            {
                float HighestY = GameManager.WindowSize.Y;
                float CurrentX = Platforms[Platforms.Count - 1].TopLeftPosition.X;

                for (int i = 0; i < Platforms.Count; i++)
                {
                    if (HighestY > Platforms[i].TopLeftPosition.Y)
                    {
                        HighestY = Platforms[i].TopLeftPosition.Y;
                    }
                }

                Vector2 NewPosition = GenerateRandomPosition(HighestY);
                while (Math.Abs(NewPosition.X - CurrentX) < GameManager.MinimumXDistancePlatforms) //Makes sure platforms don't spawn too close next to each other
                {
                    NewPosition = GenerateRandomPosition(HighestY);
                }

                SpawnPlatform(NewPosition);
            }
        }

        private static void SpawnPlatform(Vector2 PlatformPosition)
        {
            int randomPlatformIndex = random.Next(0, GameManager.TypesOfPlatforms + GameManager.OddsOfPassThrough);
            int hasCollectible = random.Next(0, 2); //Either has or doesn't have a collectible
            int hasEnemy = random.Next(0, GameManager.OddsOfEnemyOnPlatform);

            //Spawning Platform
            switch (randomPlatformIndex)
            {
                case 0: //Solid Platform
                    {
                        Sprite platformSprite = new Sprite(TextureManager.PlatformSolidTexture,
                            new Vector2((int)(TextureManager.PlatformSolidTexture.Width / 1.5),
                            (int)(TextureManager.PlatformSolidTexture.Height / 1.5)),
                            PlatformPosition);

                        PlatformSolid platform = new PlatformSolid();
                        platform.ObjectSprite = platformSprite;
                        platform.PlatformType = PlatformType.Solid;
                        if (hasCollectible == 1)
                        {
                            platform.HasPlatformObject = true;
                            platform.PlatformObject = PlatformObjectManager.SpawnPlatformObject(platform);
                        }
                        if (hasEnemy == 1 && hasCollectible != 1)
                        {
                            platform.HasEnemy = true;
                            //platform.Size = IncreaseWidth(platform);
                            platform.Enemy = SpawnEnemy(platform);
                        }
                        Platforms.Add(platform);

                        break;
                    }
                case 1: //Moving Platform
                    {
                        Sprite platformSprite = new Sprite(TextureManager.PlatformMovingTexture,
                            new Vector2((int)(TextureManager.PlatformMovingTexture.Width / 1.5),
                            (int)(TextureManager.PlatformMovingTexture.Height / 1.5)),
                            PlatformPosition);

                        PlatformMoving platform = new PlatformMoving();
                        platform.ObjectSprite = platformSprite;
                        platform.PlatformType = PlatformType.Moving;
                        platform.MoveSpeed = random.Next(1, GameManager.PlatformMoveRange + 1);
                        if (hasCollectible == 1)
                        {
                            platform.HasPlatformObject = true;
                            platform.PlatformObject = PlatformObjectManager.SpawnPlatformObject(platform);
                        }

                        Platforms.Add(platform);

                        break;
                    }

                case 2: //Breakable
                    {
                        Sprite platformSprite = new Sprite(TextureManager.PlatformBreakableTexture,
                        new Vector2((int)(TextureManager.PlatformBreakableTexture.Width / 1.5),
                        (int)(TextureManager.PlatformBreakableTexture.Height / 1.5)),
                        PlatformPosition);

                        PlatformBreakable platform = new PlatformBreakable();
                        platform.ObjectSprite = platformSprite;
                        platform.PlatformType = PlatformType.Breakable;
                        if (hasCollectible == 1)
                        {
                            platform.HasPlatformObject = true;
                            platform.PlatformObject = PlatformObjectManager.SpawnPlatformObject(platform);
                        }
                        if (hasEnemy == 1 && hasCollectible != 1)
                        {
                            platform.HasEnemy = true;
                            //platform.Size = IncreaseWidth(platform);
                            platform.Enemy = SpawnEnemy(platform);
                        }

                        Platforms.Add(platform);

                        break;
                    }
                default:
                    {
                        Sprite platformSprite = new Sprite(TextureManager.PlatformPassThroughTexture,
                            new Vector2((int)(TextureManager.PlatformPassThroughTexture.Width / 1.5),
                            (int)(TextureManager.PlatformPassThroughTexture.Height / 1.5)),
                            PlatformPosition);

                        PlatformPassThrough platform = new PlatformPassThrough();
                        platform.ObjectSprite = platformSprite;
                        platform.PlatformType = PlatformType.PassThrough;
                        if (hasCollectible == 1)
                        {
                            platform.HasPlatformObject = true;
                            platform.PlatformObject = PlatformObjectManager.SpawnPlatformObject(platform);
                        }
                        if (hasEnemy == 1 && hasCollectible != 1)
                        {
                            platform.HasEnemy = true;
                            //platform.Size = IncreaseWidth(platform);
                            platform.Enemy = SpawnEnemy(platform);
                        }

                        Platforms.Add(platform);

                        break;
                    }
            }
        }



        private static EnemyPlatform SpawnEnemy(Platform platform)
        {
            EnemyPlatform enemy = new EnemyPlatform();
            enemy.ObjectSprite = new Sprite(TextureManager.EnemyWalkTexture,
                new Vector2(TextureManager.EnemyWalkTexture.Width / 9, TextureManager.EnemyWalkTexture.Height),
                new Vector2((int)(platform.TopLeftPosition.X), platform.TopLeftPosition.Y
                - TextureManager.EnemyWalkTexture.Height / 2),
                0, true, true, 5, 1, 9);
            enemy.Velocity = new Vector2(GameManager.WalkingEnemyVelocity, 0);
            enemy.TopLeftPosition += new Vector2(random.Next(0, (int)(platform.Size.X)),0);


            return enemy;
        }

        private static Vector2 GenerateRandomPosition(float highestY)
        {
            return new Vector2(random.Next(0,
                (int)GameManager.WindowSize.X - TextureManager.PlatformPassThroughTexture.Width),
                highestY - random.Next(GameManager.JumpHeight / 2, GameManager.JumpHeight));
        }

        public static void DeleteInactivePlatforms()
        {
            int i = 0;
            while (i != Platforms.Count - 1)
            {
                Platform platform = Platforms[i];
                if (platform.HasPlatformObject)
                {
                    if (platform.PlatformObject.isActive == false)
                    {
                        platform.HasPlatformObject = false;
                        platform.PlatformObject = null;
                    }
                }
                if (platform.isActive == false)
                {
                    Platforms.Remove(platform);
                }
                else
                {
                    i++;
                }
            }
        }
        public static void Update()
        {
            foreach (Platform platform in Platforms)
            {
                platform.Update();
            }
        }
        public static void DeActivatePlatforms()
        {
            foreach (Platform platform in Platforms)
            {
                if (platform.TopLeftPosition.Y > GameManager.WindowSize.Y)
                {
                    platform.isActive = false;
                }
            }
        }

        public static void Draw()
        {
            foreach (Platform platform in Platforms)
            {
                if (platform.Respawning != true)
                    platform.Draw();
                if (GameManager.DebugMode)
                {
                    platform.DrawHitbox();
                }
            }
        }

        public static void BreakPlatforms()
        {
            foreach (Platform platform in Platforms)
            {
                if (platform.PlatformType == PlatformType.Breakable)
                {
                    if (platform.Breaking == false && platform.PlatformType == PlatformType.Breakable
                    && platform.ObjectSprite.Texture != TextureManager.PlatformBreakableTexture)
                    {
                        platform.ObjectSprite = new Sprite(TextureManager.PlatformBreakableTexture,
                            platform.Size, platform.TopLeftPosition);
                    }

                    if (platform.Breaking == true && platform.PlatformType == PlatformType.Breakable
                    && platform.ObjectSprite.Texture != TextureManager.PlatformBreakTexture)
                    {
                        platform.ObjectSprite = new Sprite(TextureManager.PlatformBreakTexture, platform.Size,
                            platform.TopLeftPosition, platform.Angle, true, true, 20, 2, 3);
                    }
                }
            }
        }

        public static void SpawnTurret()
        {
            TurretPlatform Turret = new TurretPlatform();
            Turret.PlatformType = PlatformType.Turret;
            Turret.ObjectSprite = new Sprite(TextureManager.TurretTexture,
                new Vector2(TextureManager.TurretTexture.Width, TextureManager.TurretTexture.Height),
                new Vector2(GameManager.WindowSize.X / 2 - TextureManager.TurretTexture.Width / 2, GameManager.WindowSize.Y - 50));
            Platforms.Add(Turret);
            GameManager.Turret = Turret;
        }
        public static void SpawnFirstPlatform()
        {
            Sprite platformSprite = new Sprite(TextureManager.PlatformStartingTexture,
                new Vector2((int)GameManager.WindowSize.X,
                TextureManager.PlatformStartingTexture.Height * 2),
                new Vector2(0, GameManager.WindowSize.Y - TextureManager.PlatformStartingTexture.Height * 2));

            PlatformSolid platform = new PlatformSolid();
            platform.ObjectSprite = platformSprite;
            platform.PlatformType = PlatformType.Solid;
            Platforms.Add(platform);
        }

        public static void ManagePlatforms()
        {
            SpawnPlatforms();
            Update();
            DeActivatePlatforms();
            BreakPlatforms();
            DeleteInactivePlatforms();
        }

    }
}

//Unused code, kept just in-case purposes
/*private void SpawnHeightTestPlatform()
        {
            Texture2D testPlatformSpriteTexture = Content.Load<Texture2D>(FilePaths.PlatformPassThroughPath);
            Sprite testPlatformSprite = new Sprite(testPlatformSpriteTexture,
                new Vector2((int)(testPlatformSpriteTexture.Width / 1.5), (int)(testPlatformSpriteTexture.Height / 1.5)),

                new Vector2(_random.Next(0, (int)GameManager.WindowSize.X - (int)(testPlatformSpriteTexture.Width / 1.5)),
                GameManager.WindowSize.Y - testPlatformSpriteTexture.Height - 200 - 26)); //(26 is Jumping Platform Height)

            PlatformPassThrough platform = new PlatformPassThrough();
            platform.ObjectSprite = testPlatformSprite;
            platform.PlatformType = PlatformType.PassThrough;
            Platforms.Add(platform);
        }

        private void SpawnTestPlatforms()
        {
            
            for (int i = 0; i < 5; i++)
            {
                Texture2D testPlatformSpriteTexture = Content.Load<Texture2D>(FilePaths.PlatformBreakablePath);

                Sprite testPlatformSprite = new Sprite(testPlatformSpriteTexture,
                    new Vector2((int)(testPlatformSpriteTexture.Width/1.5), (int)(testPlatformSpriteTexture.Height/1.5)),
                    
                    new Vector2(_random.Next(0, (int)GameManager.WindowSize.X - (int)(testPlatformSpriteTexture.Width/1.5)), 
                    GameManager.WindowSize.Y - testPlatformSpriteTexture.Height - 50*i*3 - _random.Next(0, 100)));

                PlatformBreakable platform = new PlatformBreakable();
                platform.ObjectSprite = testPlatformSprite;
                platform.PlatformType = PlatformType.Breakable;
                Platforms.Add(platform);
            }

        }
*/