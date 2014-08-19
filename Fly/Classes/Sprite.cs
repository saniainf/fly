using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fly.Classes
{
    class Sprite
    {
        public Texture2D Texture;

        protected List<Rectangle> frames = new List<Rectangle>();
        protected int frameWidth = 0;
        protected int frameHeight = 0;
        protected int currentFrame;
        protected float frameTime = 0.1f;
        protected float timeForCurrentFrame = 0.0f;

        protected Color tintColor = Color.White;
        protected float rotation = 0.0f;

        protected Vector2 location = Vector2.Zero;
        protected Vector2 velocity = Vector2.Zero;

        public Sprite(Texture2D texture, Rectangle initialFrame, Vector2 location, Vector2 velocity)
        {
            this.Texture = texture;
            frames.Add(initialFrame);
            frameWidth = initialFrame.Width;
            frameHeight = initialFrame.Height;

            this.location = location;
            this.velocity = velocity;
        }

        public Color TintColor
        {
            get { return tintColor; }
            set { tintColor = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value % MathHelper.TwoPi; }
        }

        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public int Frame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frames.Count - 1); }
        }

        public Rectangle Source
        {
            get { return frames[currentFrame]; }
        }

        public Rectangle Destination
        {
            get { return new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight); }
        }

        public Vector2 Center
        {
            get { return location + new Vector2(frameWidth / 2, frameHeight / 2); }
        }
    }
}
