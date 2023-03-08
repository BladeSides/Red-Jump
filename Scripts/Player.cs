using Final_Project.Scripts.FileManagers;
using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Final_Project.Scripts
{
    public class Player : GameObject
    {
        //Public Members
        public Random random = new Random();
        public Vector2 Acceleration { get; set; } = new Vector2(0, 0);
        public Sprite PlayerIdleSprite { get; set; }
        public Sprite PlayerJumpSprite { get; set; }
        public Sprite PlayerRunSprite { get; set; }
        public Sprite PlayerSplitSprite { get; set; } //The sprite drawn on other end of the screen
        public bool IsGrounded { get; set; } = false;
        public int Lives { get; set; }

        public bool CanCollideWithPlatforms { get; set; } = true;
        public bool IsOutOfScreen
        {
            get
            {
                if (TopLeftPosition.Y > GameManager.WindowSize.Y)
                    return true;
                return false;
            }
        }


        //Private Members
        private float MaxSpeed { get; set; } = 15;
        private float TiltAngle { get; set; } = 30f; //The angle player tilts at while moving


        private List<Sprite> PlayerSpriteList = new List<Sprite>();

        public void KillEnemy()
        {
            Velocity = new Vector2(Velocity.X, -GameManager.JumpVelocity);
            SoundManager.PlaySoundEffect(SoundManager.KillEffect);
        }
        public void LoadPlayerSprites(ContentManager Content)
        {
            //Instantiating _playerSpriteList
            PlayerSpriteList = new List<Sprite>();
            PlayerSpriteList.Clear();

            LoadPlayerTextures(Content);

            //Instantiating Sprites

            PlayerIdleSprite = new Sprite(TextureManager.PlayerIdleSpriteTexture,
                GameManager.PlayerSize,
                GameManager.StartingPosition, 0, true,
            true, 3, 2, 9);


            PlayerRunSprite = new Sprite(TextureManager.PlayerRunSpriteTexture,
                GameManager.PlayerSize,
                GameManager.StartingPosition, 0, true,
            true, 2, 2, 12);

            PlayerJumpSprite = new Sprite(TextureManager.PlayerJumpSpriteTexture,
                GameManager.PlayerSize,
                GameManager.StartingPosition, 0, true,
                true, 3, 2, 4);

            PlayerSplitSprite = new Sprite(TextureManager.PlayerIdleSpriteTexture,
            GameManager.PlayerSize,
            GameManager.StartingPosition, 0, true,
            true, 3, 2, 9);

            //Making Sure Hitboxes are 1/3rd size horizontally and complete vertical size - 10
            PlayerIdleSprite.HitBox = new Rectangle(PlayerIdleSprite.TopLeftPosition.ToPoint() +
            new Point((int)PlayerIdleSprite.Size.X / 3, 10),
            new Point((int)PlayerIdleSprite.Size.X / 3,
            (int)PlayerIdleSprite.Size.Y - 10));

            PlayerRunSprite.HitBox = new Rectangle(PlayerRunSprite.TopLeftPosition.ToPoint() +
            new Point((int)PlayerRunSprite.Size.X / 3, 10),
            new Point((int)PlayerRunSprite.Size.X / 3,
            (int)PlayerRunSprite.Size.Y - 10));

            PlayerJumpSprite.HitBox = new Rectangle(PlayerJumpSprite.TopLeftPosition.ToPoint() +
            new Point((int)PlayerJumpSprite.Size.X / 3, 10),
            new Point((int)PlayerJumpSprite.Size.X / 3, (int)PlayerJumpSprite.Size.Y - 10));

            PlayerSplitSprite.HitBox = new Rectangle(PlayerJumpSprite.TopLeftPosition.ToPoint() +
            new Point((int)PlayerJumpSprite.Size.X / 3, 10),
            new Point((int)PlayerJumpSprite.Size.X / 3, (int)PlayerJumpSprite.Size.Y - 10));

            //Adding them to player sprite list
            PlayerSpriteList.Add(PlayerIdleSprite);
            PlayerSpriteList.Add(PlayerJumpSprite);
            PlayerSpriteList.Add(PlayerRunSprite);
            PlayerSpriteList.Add(PlayerSplitSprite);
        }

        private void LoadPlayerTextures(ContentManager Content)
        {
            TextureManager.PlayerIdleSpriteTexture = Content.Load<Texture2D>(FilePaths.PlayerIdleSpritePath);
            TextureManager.PlayerJumpSpriteTexture = Content.Load<Texture2D>(FilePaths.PlayerJumpSpritePath);
            TextureManager.PlayerRunSpriteTexture = Content.Load<Texture2D>(FilePaths.PlayerRunSpritePath);
        }

        public override void Update()
        {
            //SetPlayerColor();
            base.Update();
            RotatePlayer();

            if (PowerUpManager.LowerGravity)
            {
                CanCollideWithPlatforms = false;
            }
            else
            {
                CanCollideWithPlatforms = true;
            }

            HitBox = new Rectangle((int)(TopLeftPosition.X + GameManager.PlayerSize.X / 3),
                (int)(TopLeftPosition.Y + 10),
                (int)GameManager.PlayerSize.X / 3, (int)(GameManager.PlayerSize.Y - 10));

            SetAllSpriteValues();

            if (Velocity.X > 0)
            {
                ObjectSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            if (Velocity.X < 0)
            {
                ObjectSprite.SpriteEffect = SpriteEffects.None;
            }

            Animate();

            if (IsSplitAcrossScreen())
            {
                if (ObjectSprite.Texture != PlayerSplitSprite.Texture)
                {
                    UpdateSplitSprite();
                }
                if (TopLeftPosition.X < 0 - Size.X / 2 || TopLeftPosition.X > GameManager.WindowSize.X - Size.X / 2)
                {
                    ChangeSplitPositions(); 
                }
            }
            MovePlayer();
            ClampVelocity();
        }

        private void SetPlayerColor()   
        {
            if (GameManager.PlayerIsInvulnerable == true && GameManager.PlayerHasRespawned)
            {
                float ColorValue = ((float)GameManager.PlayerInvulnerabilityTimer / GameManager.PlayerInvulnerabilityTime);
                DrawColor = new Color(ColorValue, ColorValue, ColorValue, ColorValue);
            }
            else if (GameManager.PlayerIsInvulnerable)
            {
                DrawColor = Color.Red;
            }
            else if (GameManager.Gravity < GameManager.NormalGravityValue)
            {
                DrawColor = Color.Gold;
            }
            else
            {
                DrawColor = Color.White;
            }
        }

        /// <summary>
        /// Swaps position of the sprite and actual object
        /// </summary>
        private void ChangeSplitPositions()
        {
            Vector2 TempTopLeftPosition = TopLeftPosition;
            TopLeftPosition = PlayerSplitSprite.TopLeftPosition;
            PlayerSplitSprite.TopLeftPosition = TempTopLeftPosition;
        }

        private void UpdateSplitSprite()
        {
            PlayerSplitSprite = new Sprite(ObjectSprite.Texture, ObjectSprite.Size, ObjectSprite.TopLeftPosition, ObjectSprite.Angle,
                ObjectSprite.IsAnimated, ObjectSprite.IsSpriteSheet, ObjectSprite.TimePerFrame, ObjectSprite.Rows, ObjectSprite.Columns);
        }

        public void DrawSplitPlayer()
        {
            PlayerSplitSprite.CurrentFrame = ObjectSprite.CurrentFrame;
            PlayerSplitSprite.SpriteEffect = ObjectSprite.SpriteEffect;

            if (TopLeftPosition.X < 0)
            {
                PlayerSplitSprite.TopLeftPosition = new Vector2(Math.Abs(TopLeftPosition.X + GameManager.WindowSize.X), TopLeftPosition.Y);
            }
            else
            {
                PlayerSplitSprite.TopLeftPosition = new Vector2((TopLeftPosition.X + PlayerSplitSprite.Size.X) % GameManager.WindowSize.X - PlayerSplitSprite.Size.X, TopLeftPosition.Y);
            }
            PlayerSplitSprite.Draw();
        }

        public bool IsSplitAcrossScreen()
        {
            if (TopLeftPosition.X < 0 || TopLeftPosition.X + Size.X > GameManager.WindowSize.X)
            {
                return true;
            }
            return false;
        }

        private void RotatePlayer()
        {
            Angle = Velocity.X / MaxSpeed * TiltAngle;
        }

        public override void Draw()
        {
            SetPlayerColor();
            base.Draw();
        }
        private void Animate()
        {
            if (Velocity.Y != 0)
            {
                ObjectSprite = PlayerJumpSprite;
            }
            else
            {
                if (Velocity.X * Velocity.X >= GameManager.PlayerAnimationVelocityBuffer)
                {
                    ObjectSprite = PlayerRunSprite;
                }
                else
                {
                    ObjectSprite = PlayerIdleSprite;
                }
            }
        }
        private void ApplyGravity()
        {
            if (IsGrounded == false)
            {
                Acceleration = new Vector2(Acceleration.X, GameManager.Gravity);
            }
            else
            {
                Acceleration = new Vector2(Acceleration.X, 0);
                Velocity = new Vector2(Velocity.X, Math.Min(0,Velocity.Y)); //Makes sure player can only move up
            }
        }

        public void CollideWithPlatforms(List<Platform> platforms)
        {
            if (CanCollideWithPlatforms)
            {
                IsGrounded = false;
                foreach (Platform platform in platforms)
                {
                    if (platform.HitBox.Intersects(HitBox))
                    {
                        platform.Collide(this);
                    }

                }
            }
        }
        public bool IsAbovePlatform(Platform platform)
        {
            if (TopLeftPosition.Y + Size.Y - GameManager.VerticalBuffer < platform.TopLeftPosition.Y)
            {
                return true;
            }
            return false;
        }

        private void MovePlayer()
        {
            Velocity = new Vector2(0, Velocity.Y);

            bool isKeyPressed = false;

            //X-Velocity
            if (GameManager.CurrentKeyboardState.IsKeyDown(Keys.D) || GameManager.CurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                Acceleration += new Vector2(GameManager.MoveAcceleration, 0);
                if (Velocity.X < 0) //Accelerate faster if going opposite direction
                {
                    Acceleration += new Vector2(GameManager.MoveDeceleration, 0);
                }
                isKeyPressed = true;
            }
            if (GameManager.CurrentKeyboardState.IsKeyDown(Keys.A) || GameManager.CurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                Acceleration += new Vector2(-GameManager.MoveAcceleration, 0);
                if (Velocity.X > 0) //Accelerate faster if going opposite direction
                {
                    Acceleration += new Vector2(-GameManager.MoveDeceleration, 0);
                }
                isKeyPressed = true;
            }

            //Y-Velocity - Jump + Gravity
            if (IsGrounded)
            {
                if (GameManager.CurrentKeyboardState.IsKeyDown(Keys.W) || GameManager.CurrentKeyboardState.IsKeyDown(Keys.Up)
               || GameManager.CurrentKeyboardState.IsKeyDown(Keys.Space))
                {
                    SoundManager.PlaySoundEffect(SoundManager.JumpEffect);

                    if (Velocity.Y >= 0)
                        Velocity = new Vector2(Velocity.X, -GameManager.JumpVelocity);
                }
                else //Remove Gravity
                {
                    Velocity = new Vector2(Velocity.X, 0);
                    Acceleration = new Vector2(Acceleration.X, 0);
                }

            }

            ApplyGravity();

            if (!isKeyPressed)
            {
                if (Acceleration.X > 0)
                {
                    Acceleration -= new Vector2(GameManager.MoveDeceleration, 0);
                }
                else if (Acceleration.X < 0)
                {
                    Acceleration -= new Vector2(-GameManager.MoveDeceleration, 0);
                }

                //Reset X Velocity if acceleration is less than max possible deceleration
                if (Acceleration.X * Acceleration.X <= GameManager.MoveDeceleration * GameManager.MoveDeceleration)
                {
                    Acceleration = new Vector2(0, Acceleration.Y);
                    Velocity = new Vector2(0, Velocity.Y);
                }
            }

            Velocity += new Vector2(Acceleration.X*GameManager.DeltaTime, 
                Acceleration.Y*GameManager.DeltaTime);
            ClampVelocity();
        }

        private void ClampVelocity()
        {
            //X-Velocity
            if (Velocity.X < -MaxSpeed)
            {
                Velocity = new Vector2(-MaxSpeed, Velocity.Y);
            }
            if (Velocity.X > MaxSpeed)
            {
                Velocity = new Vector2(MaxSpeed, Velocity.Y);
            }

            //Y-Velocity, Can Jump Up As Fast As Want but reaches Terminal Velocity
            if (Velocity.Y > MaxSpeed)
            {
                Velocity = new Vector2(Velocity.X, MaxSpeed);
            }
        }

        private void SetAllSpriteValues() //Since sprites can have individual values, this makes sure they all share the value from the player so they don't get out of sync
        {
            foreach (Sprite sprite in PlayerSpriteList)
            {
                sprite.TopLeftPosition = TopLeftPosition;
                sprite.HitBox = HitBox;
                sprite.SpriteEffect = ObjectSprite.SpriteEffect;

                //Reset frame to 0 for unused sprites
                if (sprite != ObjectSprite)
                {
                    sprite.CurrentFrame = 0;
                }
            }
        }
    }
}