using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fly.Classes
{
    class Enemy
    {
        public CollisionSprite EnemySprite;
        public Vector2 gunOffset = new Vector2(25, 25);
        private float speed = 0.05f;
        public bool Destroyed = false;
        private int enemyRadius = 15;
        private Rectangle fieldBound;
        private Vector2 dimension;

        private Curve xCurve = new Curve();
        private Curve yCurve = new Curve();
        private float time;

        private Vector2 currentWaypoint = Vector2.Zero;
        private Vector2 previousLocation = Vector2.Zero;

        public Enemy(Texture2D texture, Vector2 location, Rectangle initialFrame, int frameCount, Rectangle fieldBound, Vector2 dimension)
        {
            EnemySprite = new CollisionSprite(texture, initialFrame, location, Vector2.Zero);

            for (int i = 1; i < frameCount; i++)
                EnemySprite.AddFrame(new Rectangle(
                    initialFrame.X + (initialFrame.Width * i),
                    initialFrame.Y,
                    initialFrame.Width,
                    initialFrame.Height));

            previousLocation = location;
            currentWaypoint = location;
            EnemySprite.CollisionRadius = enemyRadius;
            this.fieldBound = fieldBound;
            this.dimension = dimension;
        }

        public void AddWaypoint(List<Vector2> waypoint)
        {
            for (int i = 0; i < waypoint.Count; i++)
            {
                float t = (float)(1.0f / (float)waypoint.Count) * (float)i;
                xCurve.Keys.Add(new CurveKey(t, waypoint[i].X));
                yCurve.Keys.Add(new CurveKey(t, waypoint[i].Y));
            }
            xCurve.ComputeTangents(CurveTangent.Smooth);
            yCurve.ComputeTangents(CurveTangent.Smooth);
        }

        public bool WaypointReached()
        {
            Vector2 endPoint = new Vector2(
                (fieldBound.Width / dimension.X) * xCurve.Evaluate(1f),
                (fieldBound.Height / dimension.Y) * yCurve.Evaluate(1f));

            return (Vector2.Distance(EnemySprite.Location, endPoint) < (float)EnemySprite.Source.Width / 2);
        }

        public bool IsActive()
        {
            if (Destroyed)
                return false;

            if (WaypointReached())
                return false;

            return true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive())
            {
                //Vector2 heading = currentWaypoint - EnemySprite.Location;

                //if (heading != Vector2.Zero)
                //    heading.Normalize();

                //heading *= speed;
                //EnemySprite.Velocity = heading;
                //previousLocation = EnemySprite.Location;

                //EnemySprite.Update(gameTime);

                //EnemySprite.Rotation = (float)Math.Atan2(EnemySprite.Location.Y - previousLocation.Y,
                //                                         EnemySprite.Location.X - previousLocation.X);

                time = (speed * (float)gameTime.ElapsedGameTime.TotalSeconds + time) % 1;
                float x = (fieldBound.Width / 4) * xCurve.Evaluate(time) + fieldBound.X;
                float y = (fieldBound.Height / 2) * yCurve.Evaluate(time) + fieldBound.Y;
                EnemySprite.Location = new Vector2(x, y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnemySprite.Draw(spriteBatch);
        }
    }
}
