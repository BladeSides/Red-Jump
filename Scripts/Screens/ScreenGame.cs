using Final_Project.Scripts.Collectibles;
using Final_Project.Scripts.Managers;
using Final_Project.Scripts.Platforms;
using Final_Project.Scripts.UIObjects;
using GeoSketch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Screens
{
    public class ScreenGame : Screen
    {
        protected override void UpdateScreen()
        {
            if ((GameManager.CurrentKeyboardState.IsKeyDown(Keys.Escape) && GameManager.PreviousKeyboardState.IsKeyUp(Keys.Escape))
                || (GameManager.CurrentKeyboardState.IsKeyDown(Keys.P) && GameManager.PreviousKeyboardState.IsKeyUp(Keys.P)))

            {
                GameManager.isPaused = !GameManager.isPaused;
            }
            if (GameManager.isPaused)
            {
                return;
            }
            if (GameManager.DebugMode)
            {
                DebugCommands();
            }
            if (GameManager.Player.Lives <= 0 || GameManager.GameTimer < 0)
            {
                GameManager.ScreenEndGame.EndGame(ScoreUI.CurrentScore);
                GameManager.ActiveScreen = GameManager.ScreenEndGame;
            }
            GameManager.Player.Update();
            BackgroundManager.Update();
            UIManager.Update();
            FloatingEnemyManager.Update();
            PowerUpManager.UpdatePowerUps();
            GameManager.Player.CollideWithPlatforms(PlatformManager.Platforms);

            MoveCamera();

            ManagePlatforms();

            //If fell off
            if (GameManager.PlayerIsInvulnerable == true && GameManager.PlayerHasRespawned)
            {
                GameManager.PlayerInvulnerabilityTimer++;
                if (GameManager.PlayerInvulnerabilityTimer > GameManager.PlayerInvulnerabilityTime)
                {
                    GameManager.PlayerIsInvulnerable = false;
                    GameManager.PlayerInvulnerabilityTimer = 0;
                    GameManager.PlayerHasRespawned = false;
                }
            }

            //If hit by enemy
            else if (GameManager.PlayerIsInvulnerable == true)
            {
                GameManager.PlayerInvulnerabilityTimer++;
                if (GameManager.PlayerInvulnerabilityTimer > GameManager.PlayerInvulnerabilityTime)
                {
                    GameManager.PlayerIsInvulnerable = false;
                    GameManager.PlayerInvulnerabilityTimer = 0;
                }
            }

            if (GameManager.Player.IsOutOfScreen)
            {
                if (GameManager.Player.Lives > 0)
                {
                    if(GameManager.Player.Lives > 1)
                    {
                        SoundManager.PlaySoundEffect(SoundManager.PlayerHitEffect);
                        RespawnPlayer();
                    }
                    GameManager.Player.Lives--;
                }
            }
        }

        private void DebugCommands()
        {
            if (GameManager.CurrentKeyboardState.IsKeyDown(Keys.R) && GameManager.PreviousKeyboardState.IsKeyUp(Keys.R))
            {
                ResetGame();
            }
        }

        public void ResetGame()
        {
            PlatformManager.Platforms.Clear();
            PlatformManager.SpawnFirstPlatform();
            PlatformManager.SpawnTurret();
            FloatingEnemyManager.FloatingEnemies.Clear();
            UIManager.PopUpTexts.Clear();
            GameManager.Player.Lives = GameManager.PlayerLives;
            GameManager.Gravity = GameManager.NormalGravityValue;
            GameManager.Player.TopLeftPosition = GameManager.StartingPosition;
            GameManager.GameTimer = GameManager.GameTime;
            GameManager.PlayerIsInvulnerable = false;
            GameManager.PlayerInvulnerabilityTimer = 0;
            GameManager.Player.Velocity = new Vector2(0, 0);
            GameManager.Player.Acceleration = new Vector2(0, 0);
            ScoreUI.CurrentScore = 0;
        }

        private void ManagePlatforms()
        {
            PlatformManager.ManagePlatforms();
        }
        private void MoveCamera()
        {
            GameManager.MoveCamera(GameManager.Player);
        }
       

        private void RespawnPlayer()
        {
            GameManager.PlayerIsInvulnerable = true;
            GameManager.PlayerHasRespawned = true;
            GameManager.PlayerInvulnerabilityTimer = 0;
            GameManager.PlayerInvulnerabilityTime = GameManager.RespawnInvulnerabilityTime;

            SpawnPlayerAtLowestPlatform();
        }

        private void SpawnPlayerAtLowestPlatform()
        {
            Platform lowestPlatform;
            if (PlatformManager.Platforms[0].PlatformType != PlatformType.Turret)
            {
                lowestPlatform = PlatformManager.Platforms[0];
            }
            else
            {
                lowestPlatform = PlatformManager.Platforms[1];
            }
            foreach (Platform platform in PlatformManager.Platforms)
            {
                if (platform.TopLeftPosition.Y > lowestPlatform.TopLeftPosition.Y && platform.PlatformType != PlatformType.Turret)
                {
                    lowestPlatform = platform;
                }
            }
            GameManager.Player.TopLeftPosition = lowestPlatform.TopLeftPosition +
                new Vector2(lowestPlatform.Size.X / 2, -GameManager.PlayerSize.Y);

            //Reset Player Movement
            GameManager.Player.Velocity = new Vector2(0, 0);
            GameManager.Player.Acceleration = new Vector2(0, 0);
        }

        protected override void DrawScreen()
        {
            BackgroundManager.Draw();
            DrawPlayer();
            PlatformManager.Draw();
            FloatingEnemyManager.Draw();
            UIManager.Draw();
            if (GameManager.isPaused)
            {
                DrawPause();
            }

        }

        private void DrawPause()
        {
            GameManager.SpriteBatch.DrawRectangle(0, 0, GameManager.WindowSize.X, GameManager.WindowSize.Y,
                new Color(50, 50, 50, 50), Color.Transparent, 0);
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, "Press Esc to Resume", new Vector2
                (GameManager.WindowSize.X / 2 - 150, GameManager.WindowSize.Y / 2 - 20), Color.Black);
            GameManager.SpriteBatch.DrawString(GameManager.GameFont, "Press Esc to Resume", new Vector2
                (GameManager.WindowSize.X / 2 - 155, GameManager.WindowSize.Y / 2 - 23), Color.White);

            if (GameManager.Turret.Bullets.Count != 1)
            {
                GameManager.SpriteBatch.DrawString(GameManager.GameFont, $"{GameManager.Turret.Bullets.Count} Bullets Active", new Vector2
                    (GameManager.WindowSize.X / 2 - 150, GameManager.WindowSize.Y / 2 + 10), Color.Black);
                GameManager.SpriteBatch.DrawString(GameManager.GameFont, $"{GameManager.Turret.Bullets.Count} Bullets Active", new Vector2
                    (GameManager.WindowSize.X / 2 - 155, GameManager.WindowSize.Y / 2 + 13), Color.White);
            }
            else
            {
                GameManager.SpriteBatch.DrawString(GameManager.GameFont, $"{GameManager.Turret.Bullets.Count} Bullet Active", new Vector2
                    (GameManager.WindowSize.X / 2 - 150, GameManager.WindowSize.Y / 2 + 10), Color.Black);
                GameManager.SpriteBatch.DrawString(GameManager.GameFont, $"{GameManager.Turret.Bullets.Count} Bullet Active", new Vector2
                    (GameManager.WindowSize.X / 2 - 155, GameManager.WindowSize.Y / 2 + 13), Color.White);
            }
        }

        private void DrawPlayer()
        {
            GameManager.Player.Draw();
            if (GameManager.Player.IsSplitAcrossScreen())
            {
                GameManager.Player.DrawSplitPlayer();
            }
            if (GameManager.DebugMode)
            {
                GameManager.Player.DrawHitbox();
                GameManager.Player.Velocity = new Vector2(GameManager.Player.Velocity.X, -20);
                GameManager.Player.Lives = 3;
            }
        }
    }
}
