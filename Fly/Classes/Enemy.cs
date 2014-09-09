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
        private float speed = 0.2f;
        public bool Destroyed = false;
        private int enemyRadius = 15;
        private Rectangle fieldBound;
        private Vector2 dimension;

        private Curve xCurve = new Curve();
        private Curve yCurve = new Curve();
        private Vector2 endPoint;
        private float time;
        public float spawnTimer = 0.0f;

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

                if (i == waypoint.Count - 1)
                    endPoint = waypoint[i];
            }
            xCurve.ComputeTangents(CurveTangent.Smooth);
            yCurve.ComputeTangents(CurveTangent.Smooth);
        }

        public bool WaypointReached()
        {
            Vector2 endPointDimension = new Vector2(
                (fieldBound.Width / dimension.X) * endPoint.X + fieldBound.X,
                (fieldBound.Height / dimension.Y) * endPoint.Y + fieldBound.Y);
            
            return (Vector2.Distance(EnemySprite.Location, endPointDimension) < (float)EnemySprite.Source.Width / 2);
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
            if (IsActive() && spawnTimer <= 0.0f)
            {
                EnemySprite.Update(gameTime);

                time = (speed * (float)gameTime.ElapsedGameTime.TotalSeconds + time) % 1;
                float x = (fieldBound.Width / 4) * xCurve.Evaluate(time) + fieldBound.X;
                float y = (fieldBound.Height / 2) * yCurve.Evaluate(time) + fieldBound.Y;
                EnemySprite.Location = new Vector2(x, y);
            }

            else
                spawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive() && spawnTimer <= 0.0f)
                EnemySprite.Draw(spriteBatch);
        }
    }
}
