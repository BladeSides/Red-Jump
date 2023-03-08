using Final_Project.Scripts.FileManagers;
using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.UIObjects
{
    public static class HealthUI
    {
        public static Sprite Health_3 { get; set; }
        public static Sprite Health_2 { get; set; }
        public static Sprite Health_1 { get; set; }
        public static Sprite Health_0 { get; set; }
        public static void LoadTextures(ContentManager Content)
        {
            TextureManager.Health0Texture = Content.Load<Texture2D>(FilePaths.Health0Path);
            TextureManager.Health1Texture = Content.Load<Texture2D>(FilePaths.Health1Path);
            TextureManager.Health2Texture = Content.Load<Texture2D>(FilePaths.Health2Path);
            TextureManager.Health3Texture = Content.Load<Texture2D>(FilePaths.Health3Path);
        }

        public static void InitializeSprites()
        {
            Health_0 = new Sprite(TextureManager.Health0Texture, 
                new Vector2 (TextureManager.Health0Texture.Width,TextureManager.Health0Texture.Height), 
                new Vector2(10, 10));
            Health_1 = new Sprite(TextureManager.Health1Texture,
            new Vector2(TextureManager.Health1Texture.Width, TextureManager.Health1Texture.Height),
            new Vector2(10, 10));
            Health_2 = new Sprite(TextureManager.Health2Texture,
            new Vector2(TextureManager.Health2Texture.Width, TextureManager.Health2Texture.Height),
            new Vector2(10, 10));
            Health_3 = new Sprite(TextureManager.Health3Texture,
            new Vector2(TextureManager.Health3Texture.Width, TextureManager.Health3Texture.Height),
            new Vector2(10, 10));

        }
        public static void Draw()
        {
            switch (GameManager.Player.Lives)
            {
                case 0:
                    Health_0.Draw();
                    break;
                case 1:
                    Health_1.Draw();
                    break;
                case 2:
                    Health_2.Draw();
                    break;
                default:
                    Health_3.Draw();
                    break;
            }
        }
    }
}
