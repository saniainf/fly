﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;

namespace Fly.Managers
{
    class ShotManager
    {
        public List<CollisionSprite> Shots = new List<CollisionSprite>();
        private Rectangle screenBounds;

        private static Texture2D Texture;
        private static Rectangle InitialFrame;
        private static int FrameCount;
        private float shotSpeed;
        private static int CollisionRadius;

        public ShotManager(Texture2D texture, Rectangle initialFrame, int frameCount, int collisionRadius, float shotSpeed, Rectangle screenBounds)
        {
            Texture = texture;
            InitialFrame = initialFrame;
            FrameCount = frameCount;
            CollisionRadius = collisionRadius;
            this.shotSpeed = shotSpeed;
            this.screenBounds = screenBounds;
        }

        public void FireShot(Vector2 location, Vector2 velocity, bool playerFired)
        {
            CollisionSprite thisShot = new CollisionSprite(Texture, InitialFrame, location, velocity);
            thisShot.Velocity *= shotSpeed;

            for (int i = 1; i < FrameCount; i++)
                thisShot.AddFrame(new Rectangle(
                    InitialFrame.X + (InitialFrame.Width * i),
                    InitialFrame.Y,
                    InitialFrame.Width,
                    InitialFrame.Height));
            thisShot.CollisionRadius = CollisionRadius;
            Shots.Add(thisShot);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = Shots.Count - 1; i >= 0; i--)
            {
                Shots[i].Update(gameTime);
                if (!screenBounds.Intersects(Shots[i].Destination))
                    Shots.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionSprite shot in Shots)
                shot.Draw(spriteBatch);
        }
    }
}
