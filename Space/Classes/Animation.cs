using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space.Classes
{
    class Animation
    {
        public Texture2D Texture;
        public Vector2 Source;
        public float FrameTime;
        public bool IsLooping;
        public int FrameCount;
        public int FrameWidth;
        public int FrameHeight;

        public Animation(Texture2D texture, Vector2 source, float frameTime, bool isLooping, int frameWidth, int frameHeight, int frameCount)
        {
            this.Texture = texture;
            this.Source = source;
            this.FrameTime = frameTime;
            this.IsLooping = isLooping;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.FrameCount = frameCount;
        }

        public Animation(Texture2D texture, Rectangle source, float frameTime, bool isLooping, int frameCount)
        {
            this.Texture = texture;
            this.Source = new Vector2(source.X, source.Y);
            this.FrameTime = frameTime;
            this.IsLooping = isLooping;
            this.FrameWidth = source.Width;
            this.FrameHeight = source.Height;
            this.FrameCount = frameCount;
        }

        public Animation(Texture2D texture, float frameTime, bool isLooping)
        {
            this.Texture = texture;
            this.FrameTime = frameTime;
            this.IsLooping = isLooping;
            Source = new Vector2(0f, 0f);
            FrameWidth = texture.Height;
            FrameHeight = texture.Height;
            FrameCount = texture.Width / FrameWidth;
        }
    }
}
