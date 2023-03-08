using Final_Project.Scripts.Collectibles;
using Final_Project.Scripts.FileManagers;
using Final_Project.Scripts.PlatformObjects;
using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    public static class PlatformObjectManager
    {
        public static Random random = new Random();

        public static void LoadObjectTextures(ContentManager Content)
        {
            TextureManager.LowCoinTexture = Content.Load<Texture2D>(FilePaths.LowCoinPath);
            TextureManager.HighCoinTexture = Content.Load<Texture2D>(FilePaths.HighCoinPath);
            TextureManager.HighJumpTexture = Content.Load<Texture2D>(FilePaths.HighJumpPath);
            TextureManager.LowGravityTexture = Content.Load<Texture2D>(FilePaths.LowGravityPath);
            TextureManager.HealthPickupTexture = Content.Load<Texture2D>(FilePaths.HealthPickupPath);
        }

        public static void LoadEnemyTextures(ContentManager Content)
        {
            TextureManager.EnemyWalkTexture = Content.Load<Texture2D>(FilePaths.EnemyWalkPath);
            TextureManager.EnemyFloatTexture = Content.Load<Texture2D>(FilePaths.EnemyFloatPath);
        }
        public static PlatformObject SpawnPlatformObject(Platform platform)
        {
            int ObjectToSpawn = random.Next(GameManager.NumberOfPlatformObjects);
            PlatformObject platformObject = new PlatformObject();
            switch (ObjectToSpawn)
            {

                case 0: //Low Score Coin
                    {
                        Coin coin = new Coin();
                        coin.ObjectType = ObjectType.Coin;
                        coin.ObjectSprite = new Sprite(TextureManager.LowCoinTexture,
                            new Vector2(TextureManager.LowCoinTexture.Width, TextureManager.LowCoinTexture.Height),
                            new Vector2((int)(platform.TopLeftPosition.X + platform.Size.X / 2
                            - TextureManager.LowCoinTexture.Width / 2),
                            (int)(platform.TopLeftPosition.Y
                            - TextureManager.LowCoinTexture.Height)));
                        platformObject = coin;
                        break;
                    }
                case 1: //High Score Coin
                    {
                        Coin coin = new Coin();
                        coin.ObjectType = ObjectType.Coin;
                        coin.ObjectSprite = new Sprite(TextureManager.HighCoinTexture,
                        new Vector2(TextureManager.HighCoinTexture.Width, TextureManager.HighCoinTexture.Height),
                        new Vector2((int)(platform.TopLeftPosition.X + platform.Size.X / 2
                        - TextureManager.LowCoinTexture.Width / 2),
                        (int)(platform.TopLeftPosition.Y
                        - TextureManager.LowCoinTexture.Height)));
                        platformObject = coin;
                        break;
                    }
                case 2: //High Jump
                    {
                        HighJump highJump = new HighJump();
                        highJump.ObjectType = ObjectType.HighJump;
                        highJump.ObjectSprite = new Sprite(TextureManager.HighJumpTexture,
                        new Vector2(TextureManager.HighJumpTexture.Width, TextureManager.HighJumpTexture.Height),
                        new Vector2((int)(platform.TopLeftPosition.X + platform.Size.X / 2
                        - TextureManager.HighJumpTexture.Width / 2),
                        (int)(platform.TopLeftPosition.Y
                        - TextureManager.HighJumpTexture.Height)));
                        platformObject = highJump;
                        break;
                    }
                case 3: //Low Gravity
                    {
                        LowGravity lowGravity = new LowGravity();
                        lowGravity.ObjectType = ObjectType.LowGravity;
                        lowGravity.ObjectSprite = new Sprite(TextureManager.LowGravityTexture,
                        new Vector2(TextureManager.LowGravityTexture.Width, TextureManager.LowGravityTexture.Height),
                        new Vector2((int)(platform.TopLeftPosition.X + platform.Size.X / 2
                        - TextureManager.LowGravityTexture.Width / 2),
                        (int)(platform.TopLeftPosition.Y
                        - TextureManager.LowGravityTexture.Height)));
                        platformObject = lowGravity;
                        break;
                    }
                case 4: //Heatlh Pickup
                    {
                        HealthPickup healthPickup = new HealthPickup();
                        healthPickup.ObjectType = ObjectType.HealthPickup;
                        healthPickup.ObjectSprite = new Sprite(TextureManager.HealthPickupTexture,
                        new Vector2(TextureManager.HealthPickupTexture.Width, TextureManager.HealthPickupTexture.Height),
                        new Vector2((int)(platform.TopLeftPosition.X + platform.Size.X / 2
                        - TextureManager.HealthPickupTexture.Width / 2),
                        (int)(platform.TopLeftPosition.Y
                        - TextureManager.HealthPickupTexture.Height)));
                        platformObject = healthPickup;
                        break;
                    }
            }

            return platformObject;
        }
    }
}
