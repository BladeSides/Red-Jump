using Final_Project.Scripts.FileManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.Managers
{
    public static class BackgroundManager
    {
        public static List<Sprite> Foregrounds = new List<Sprite>();
        public static List<Sprite> Midgrounds = new List<Sprite>();
        public static List<Sprite> Backgrounds = new List<Sprite>();

        public static float ForegroundParallax = 0.7f;
        public static float MidgroundParallax = 0.5f;
        public static float BackgroundParallax = 0.3f;

        public static void LoadTextures(ContentManager Content)
        {
            TextureManager.BackgroundBackTexture = Content.Load<Texture2D>(FilePaths.BackgroundBackPath);
            TextureManager.BackgroundMidTexture = Content.Load<Texture2D>(FilePaths.BackgroundMidPath);
            TextureManager.BackgroundForeTexture = Content.Load<Texture2D>(FilePaths.BackgroundForePath);
        }

        public static void MoveBackground(int amountToMove)
        {
            foreach (Sprite bg in Backgrounds)
            {
                bg.TopLeftPosition += new Vector2(0, amountToMove * BackgroundParallax);
            }
            foreach (Sprite mg in Midgrounds)
            {
                mg.TopLeftPosition += new Vector2(0, amountToMove * MidgroundParallax);
            }
            foreach (Sprite fg in Foregrounds)
            {
                fg.TopLeftPosition += new Vector2(0, amountToMove * ForegroundParallax);
            }
        }

        public static void Update()
        {
            SpawnBackgrounds();
            DeleteBackgrounds();
        }

        private static void DeleteBackgrounds()
        {
            int i = 0;
            while(i<Backgrounds.Count)
            {
                Sprite bg = Backgrounds[i];
                if (bg.TopLeftPosition.Y > GameManager.WindowSize.Y)
                {
                    Backgrounds.Remove(bg);
                }
                else
                {
                    i++;
                }
            }
            i = 0;
            while (i < Midgrounds.Count)
            {
                Sprite mg = Midgrounds[i];
                if (mg.TopLeftPosition.Y > GameManager.WindowSize.Y)
                {
                    Midgrounds.Remove(mg);
                }
                else
                {
                    i++;
                }
            }
            i = 0;
            while (i < Foregrounds.Count)
            {
                Sprite fg = Foregrounds[i];
                if (fg.TopLeftPosition.Y > GameManager.WindowSize.Y)
                {
                    Foregrounds.Remove(fg);
                }
                else
                {
                    i++;
                }
            }
        }

        public static void Draw()
        {
            foreach (Sprite bg in Backgrounds)
            {
                bg.Draw();
            }
            foreach (Sprite mg in Midgrounds)
            {
                mg.Draw();
            }
            foreach (Sprite fg in Foregrounds)
            {
                fg.Draw();
            }
        }

        private static void SpawnBackgrounds()
        {

            //Back Backgrounds
            {
                int x = 0, y = (int)GameManager.WindowSize.Y;
                while (y > 0)
                {
                    foreach (Sprite bg in Backgrounds)
                    {
                        if (bg.TopLeftPosition.Y < y)
                        {
                            y = (int)bg.TopLeftPosition.Y;
                        }
                    }

                    if(y+TextureManager.BackgroundBackTexture.Height > 0) //If It Will Be Displayed, then add, else no
                    Backgrounds.Add(new Sprite(TextureManager.BackgroundBackTexture,
                        new Vector2(GameManager.WindowSize.X, TextureManager.BackgroundBackTexture.Height),
                        new Vector2(x, y) - new Vector2(0, TextureManager.BackgroundBackTexture.Height)));
                }
            }
            //Mid Backgrounds
            {
                int x = 0, y = (int)GameManager.WindowSize.Y;
                while (y > 0)
                {
                    foreach (Sprite mg in Midgrounds)
                    {
                        if (mg.TopLeftPosition.Y < y)
                        {
                            y = (int)mg.TopLeftPosition.Y;
                        }
                    }
                    if (y + TextureManager.BackgroundMidTexture.Height > 0) //If It Will Be Displayed, then add, else no
                        Midgrounds.Add(new Sprite(TextureManager.BackgroundMidTexture,
                        new Vector2(GameManager.WindowSize.X, TextureManager.BackgroundMidTexture.Height),
                        new Vector2(x, y) - new Vector2(0, TextureManager.BackgroundMidTexture.Height)));
                }
            }
            //Fore Backgrounds
            {
                int x = 0, y = (int)GameManager.WindowSize.Y;
                while (y > 0)
                {
                    foreach (Sprite fg in Foregrounds)
                    {
                        if (fg.TopLeftPosition.Y < y)
                        {
                            y = (int)fg.TopLeftPosition.Y;
                        }
                    }
                    if (y + TextureManager.BackgroundForeTexture.Height > 0) //If It Will Be Displayed, then add, else no
                        Foregrounds.Add(new Sprite(TextureManager.BackgroundForeTexture,
                        new Vector2(GameManager.WindowSize.X, TextureManager.BackgroundForeTexture.Height),
                        new Vector2(x, y) - new Vector2(0, TextureManager.BackgroundForeTexture .Height)));
                }
            }
        }
    }
}
