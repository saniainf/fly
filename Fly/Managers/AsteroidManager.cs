using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;

namespace Fly.Managers
{
    class AsteroidManager
    {
        private int screenWidth = 800;
        private int screenHeight = 600;
        private int screenPadding = 10;

        private Rectangle initialFrame;
        private int asteroidFrames;
        private Texture2D texture;

        public List<CollisionSprite> Asteroids = new List<CollisionSprite>();
        private int minSpeed = 60;
        private int maxSpeed = 120;

        private Random rand = new Random();

        public void AddAsteroid()
        {
            CollisionSprite newAsteroid = new CollisionSprite(texture, initialFrame, new Vector2(-500f, -500f), Vector2.Zero);

            for (int i = 1; i < asteroidFrames; i++)
            {
                newAsteroid.AddFrame(new Rectangle(
                    initialFrame.X + (initialFrame.Width * i),
                    initialFrame.Y,
                    initialFrame.Width,
                    initialFrame.Height));
            }

            newAsteroid.Rotation = MathHelper.ToRadians((float)rand.Next(0, 360));
            newAsteroid.CollisionRadius = 15;
            Asteroids.Add(newAsteroid);
        }

        public void Clear()
        {
            Asteroids.Clear();
        }

        public AsteroidManager(int asteroidCount, Texture2D texture, Rectangle initialFrame, int asteroidFrames, int screenWidth, int screenHeight)
        {
            this.texture = texture;
            this.initialFrame = initialFrame;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            for (int i = 0; i < asteroidCount; i++)
                AddAsteroid();
        }

        private Vector2 randomLocation()
        {
            Vector2 location = Vector2.Zero;
            bool locationOK = true;
            int tryCount = 0;

            do
            {
                locationOK = true;
                switch (rand.Next(0, 3))
                {
                    case 0:
                        location.X = -initialFrame.Width;
                        location.Y = rand.Next(0, screenHeight);
                        break;

                    case 1:
                        location.X = screenWidth;
                        location.Y = rand.Next(0, screenHeight);
                        break;

                    case 2:
                        location.X = rand.Next(0, screenWidth);
                        location.Y = -initialFrame.Height;
                        break;
                }

                foreach (CollisionSprite asteroid in Asteroids)
                {
                    if (asteroid.IsBoxColliding(new Rectangle((int)location.X, (int)location.Y, initialFrame.Width, initialFrame.Height)))
                        locationOK = false;
                }
                tryCount++;
                if ((tryCount > 5) && locationOK == false)
                {

                }
            } while (locationOK == false);

            return location;
        }
    }
}
