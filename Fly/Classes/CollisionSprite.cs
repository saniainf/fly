using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fly.Classes
{
    class CollisionSprite : Sprite
    {
        public int CollisionRadius = 0;
        public int BoundingXPadding = 0;
        public int BoundingYPadding = 0;

        public Rectangle BoundingBoxRect
        {
            get
            {
                return new Rectangle(
                    (int)location.X + BoundingXPadding,
                    (int)location.Y + BoundingYPadding,
                    frameWidth - (BoundingXPadding * 2),
                    frameHeight - (BoundingYPadding * 2));
            }
        }

        public bool IsBoxColliding(Rectangle otherBox)
        {
            return BoundingBoxRect.Intersects(otherBox);
        }

        public bool IsCircleColliding(Vector2 otherCenter, float otherRadius)
        {
            if (Vector2.Distance(Center, otherCenter) < (CollisionRadius + otherRadius))
                return true;
            else
                return false;
        }
    }
}
