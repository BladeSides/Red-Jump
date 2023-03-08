using Final_Project.Scripts.Managers;
using GeoSketch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts
{
    public class Sprite
    {
        //Properties
        public Texture2D Texture { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 TopLeftPosition { get; set; }

        public Color DrawColor { get; set; } = Color.White;

        public Vector2 Origin
        {
            get
            {
                return TopLeftPosition + Size / 2;
            }
        }
        public float Angle { get; set; } = 0;

        public bool IsAnimated { get; set; }
        public bool IsSpriteSheet { get; set; }
        public int TimePerFrame { get; set; }


        public int CurrentFrame = 0;
        private int Timer = 0;

        //Properties for Animation

        public int Rows { get; set; }
        public int Columns { get; set; }

        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle(TopLeftPosition.ToPoint(), Size.ToPoint());
            }
            set { }
        }
        public Rectangle DrawingRectangle
        {
            get
            {
                return new Rectangle((int)Origin.X,
                    (int)Origin.Y,
                    (int)Size.X, (int)Size.Y);
            }
        }
        public Rectangle HitBox { get; set; }

        //Public Methods
        public void Update()
        {
            if (!IsAnimated || !IsSpriteSheet)
                return;

            Timer++;
            if (Timer >= TimePerFrame)
            {
                CurrentFrame++;
                Timer = 0;
                if (CurrentFrame >= Rows * Columns - 1)
                {
                    CurrentFrame = 0;
                }
            }
        }

        public void Draw()
        {
            if (!IsAnimated || !IsSpriteSheet)
            {
                GameManager.SpriteBatch.Draw(Texture, DrawingRectangle,
                GetSourceRectangle(), DrawColor,
                Angle * (float)Math.PI / 180, new Vector2(Texture.Width / 2, Texture.Height / 2),
                SpriteEffect, 0);
            }
            else
            {
                GameManager.SpriteBatch.Draw(Texture, DrawingRectangle,
                GetSourceRectangle(), DrawColor,
                Angle * (float)Math.PI / 180,
                new Vector2(Texture.Width / Columns / 2, Texture.Height / Rows / 2),
                SpriteEffect, 0); ;
            }
        }

        public void DrawHitbox()
        {
            if(HitBox.Width != 0 && HitBox.Height != 0)
                GameManager.SpriteBatch.DrawRectangle(HitBox.X, HitBox.Y, HitBox.Width, HitBox.Height,
                Color.Transparent, Color.Red, 3);
        }

        //Private Methods
        private Rectangle GetSourceRectangle()
        {
            if (!IsSpriteSheet)
            {
                return new Rectangle(0, 0, Texture.Width, Texture.Height);
            }
            int row = CurrentFrame / Columns;
            int col = CurrentFrame % Columns;

            float width = Texture.Width / (float)Columns;
            float height = Texture.Height / (float)Rows;

            return new Rectangle(
                col * (int)width, row * (int)height,
                (int)width, (int)height);
        }

        /// <summary>
        /// Use if sprite is not animated and is not in a spritesheet
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="size"></param>
        /// <param name="topLeftPosition"></param>
        public Sprite(Texture2D texture, Vector2 size, Vector2 topLeftPosition)
        {
            Texture = texture;
            Size = size;
            TopLeftPosition = topLeftPosition;
            IsAnimated = false;
            IsSpriteSheet = false;
            HitBox = DestinationRectangle;
        }
        /// <summary>
        /// For Animated Sprites
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="size"></param>
        /// <param name="topLeftPosition"></param>
        /// <param name="angle"></param>
        /// <param name="isAnimated"></param>
        /// <param name="timePerFrame"></param>
        /// <param name="rowsAndColumns"></param>
        public Sprite(Texture2D texture, Vector2 size, Vector2 topLeftPosition,
            float angle, bool isAnimated, bool isSpriteSheet, int timePerFrame, int rows, int columns) :
            this(texture, size, topLeftPosition)
        {
            Angle = angle;
            IsAnimated = isAnimated;
            TimePerFrame = timePerFrame;
            Rows = rows;
            Columns = columns;
            IsSpriteSheet = isSpriteSheet;
            HitBox = DestinationRectangle;

        }
        /// <summary>
        /// For Non Animated Sprites in Spritesheets
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="size"></param>
        /// <param name="topLeftPosition"></param>
        /// <param name="angle"></param>
        /// <param name="isAnimated"></param>
        /// <param name="isSpriteSheet"></param>
        /// <param name="timePerFrame"></param>
        /// <param name="rowsAndColumns"></param>
        /// <param name="currentFrame"></param>
        public Sprite(Texture2D texture, Vector2 size, Vector2 topLeftPosition,
        float angle, bool isAnimated, bool isSpriteSheet, int timePerFrame, int rows, int columns
            , int currentFrame) :
        this(texture, size, topLeftPosition)
        {
            Angle = angle;
            IsAnimated = isAnimated;
            TimePerFrame = timePerFrame;
            Rows = rows;
            Columns = columns;
            IsSpriteSheet = isSpriteSheet;
            CurrentFrame = currentFrame;
            HitBox = DestinationRectangle;
        }

    }
}
