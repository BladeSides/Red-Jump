using Final_Project.Scripts.Enemies;
using Final_Project.Scripts.UIObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    public static class FloatingEnemyManager
    {
        public static List<EnemyFloating> FloatingEnemies = new List<EnemyFloating>();
        private static Random random = new Random();
        public static void SpawnFloatingEnemies()
        {
            while (FloatingEnemies.Count != GameManager.NumberOfFloatingEnemies)
            {
                float HighestY = GameManager.WindowSize.Y;
                float CurrentX = 0;
                if(FloatingEnemies.Count > 0)
                    CurrentX = FloatingEnemies[FloatingEnemies.Count - 1].TopLeftPosition.X;

                for (int i = 0; i < FloatingEnemies.Count; i++)
                {
                    if (HighestY > FloatingEnemies[i].TopLeftPosition.Y)
                    {
                        HighestY = FloatingEnemies[i].TopLeftPosition.Y;
                    }
                }

                Vector2 NewPosition = GenerateRandomPosition(HighestY);


                SpawnEnemy(NewPosition);
            }
        }

        public static void Draw()
        {
            foreach (EnemyFloating enemy in FloatingEnemies)
            {
                enemy.Draw();
                if (GameManager.DebugMode)
                {
                    enemy.DrawHitbox();
                }
            }
        }

        public static void Update()
        {
            SpawnFloatingEnemies();
            foreach (EnemyFloating enemy in FloatingEnemies)
            {
                enemy.Update();
                if (enemy.HitBox.Intersects(GameManager.Player.HitBox))
                {
                    if(GameManager.Player.Velocity.Y <= 0)
                        enemy.Collide(GameManager.Player);
                    else //Kill the enemy
                    {
                        enemy.isActive = false;
                        GameManager.Player.KillEnemy();
                        ScoreUI.CurrentScore += 1000;
                        UIManager.AddPopUpText(enemy.TopLeftPosition, "+1000");
                    }
                }
                if (enemy.TopLeftPosition.Y > GameManager.WindowSize.Y)
                {
                    enemy.isActive = false;
                }
            }
            DeleteInactiveEnemies();
        }

        private static void DeleteInactiveEnemies()
        {
            int i = 0;
            while (i != FloatingEnemies.Count - 1)
            {
                if (FloatingEnemies[i].isActive == false)
                {
                    FloatingEnemies.Remove(FloatingEnemies[i]);
                }
                else
                {
                    i++;
                }
            }
        }

        private static Vector2 GenerateRandomPosition(float highestY)
        {
            return new Vector2(random.Next(0,
                (int)GameManager.WindowSize.X),
                highestY - random.Next(GameManager.JumpHeight*2, GameManager.JumpHeight*4));
        }
        private static void SpawnEnemy(Vector2 newPosition)
        {
            EnemyFloating enemy = new EnemyFloating();
            enemy.ObjectSprite = new Sprite(TextureManager.EnemyFloatTexture,
                new Vector2((int)(TextureManager.EnemyFloatTexture.Width / 6 * 1.5f), (int)(TextureManager.EnemyFloatTexture.Height * 1.5f)), newPosition,
                0, true, true, 7, 1, 6);
            enemy.Velocity = new Vector2(GameManager.FloatingEnemyVelocity, 0);
            FloatingEnemies.Add(enemy);
        }
    }
}
