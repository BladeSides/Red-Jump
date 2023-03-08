using Final_Project.Scripts.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts
{
    public class GameObject
    {
        //Properties
        //Sprite and Sprite Wrapper Functions   
        public Sprite ObjectSprite { get; set; }
        public Vector2 TopLeftPosition
        {
            get { return ObjectSprite.TopLeftPosition; }
            set { ObjectSprite.TopLeftPosition = value; }
        }

        public Color DrawColor
        {
            get { return ObjectSprite.DrawColor; }
            set { ObjectSprite.DrawColor = value; }
        }
        public Vector2 Size
        {
            get { return ObjectSprite.Size; }
            set { ObjectSprite.Size = value; }
        }
        public float Angle
        {
            get { return ObjectSprite.Angle; }
            set { ObjectSprite.Angle = value; }
        }

        public Rectangle DestinationRectangle
        {
            get { return ObjectSprite.DestinationRectangle; }
            set { ObjectSprite.DestinationRectangle = value; }
        }
        public Rectangle HitBox
        {
            get { return ObjectSprite.HitBox; }
            set { ObjectSprite.HitBox = value; }
        }
        public Vector2 Origin { get { return ObjectSprite.Origin; } }

        //Wrapper Methods End Here

        public Vector2 Velocity { get; set; }
        public bool isActive { get; set; } = true;
        public bool IsVisible { get; set; } = true; ///To Draw or Not To Draw
        public bool IsOutOfBounds
        {
            get
            {
                if (TopLeftPosition.X + Size.X < 0 ||
                    TopLeftPosition.Y > GameManager.WindowSize.X)
                {
                    return true;
                }
                if (TopLeftPosition.Y + Size.Y < 0 ||
                    TopLeftPosition.Y > GameManager.WindowSize.Y)
                {
                    return true;
                }
                return false;
            }
        }

        //Methods
        public virtual void Update()
        {
            TopLeftPosition += Velocity;
            ObjectSprite.Update();
        }

        public virtual void Draw()
        {
            if(IsVisible)
                ObjectSprite.Draw();
        }

        public virtual void DrawHitbox()
        {
            ObjectSprite.DrawHitbox();
        }
    }
}
