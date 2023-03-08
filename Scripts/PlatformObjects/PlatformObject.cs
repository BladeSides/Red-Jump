using Final_Project.Scripts.Platforms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.PlatformObjects
{
    public enum ObjectType
    {
        Coin,
        HighJump,
        LowGravity,
        HealthPickup
    }
    public class PlatformObject: GameObject
    {

        public ObjectType ObjectType { get; set; }
        public virtual void UpdatePlatformObject(Platform platform)
        {
            HitBox = new Rectangle((int)ObjectSprite.TopLeftPosition.X, (int)ObjectSprite.TopLeftPosition.Y,
            (int)ObjectSprite.Size.X, (int)ObjectSprite.Size.Y);
            base.Update();
        }

        public virtual void Collect()
        {
            isActive = false;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
