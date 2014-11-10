using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space.Classes
{
    public class Sprite
    {
        public Texture2D Texture;
        public Rectangle DestinationRectangle;
        public Rectangle SourceRectangle;
        public Color TintColor = Color.White;
        private float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value % MathHelper.TwoPi; }
        }

        public Vector2 Center
        {
            get { return new Vector2(DestinationRectangle.X, DestinationRectangle.Y) + new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2); }
        }

        public Sprite(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color tintColor)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            SourceRectangle = sourceRectangle;
            TintColor = tintColor;
        }

        public Sprite(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            SourceRectangle = sourceRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Center, SourceRectangle, TintColor, rotation, new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2), 1.0f, SpriteEffects.None, 1.0f);
            }
        }
    }
}
