using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;

namespace Fly.Managers
{
    class EnemyManager
    {
        private Texture2D texture;
        private Rectangle initialFrame;
        private int frameCount;

        public List<Enemy> Enemies = new List<Enemy>();

        public ShotManager EnemyShotManager;
        private PlayerManager playerManager;
        private Rectangle screenBound;

        public int MinShipsPerWave = 3;
        public int MaxShipsPerWave = 6;
        private float nextWaveTime = 0.0f;
        private float nextWaveMinTimer = 4f;
        private float nextShipMinTime = 0.2f;
        private float nextShipTimer = 0.0f;

        private float shipShotChance = 0.2f;

        private List<List<Vector2>> pathWaypoints = new List<List<Vector2>>();

        public bool Active = true;

        private Random rand = new Random();

        private void setUpWaypoints()
        {
            List<Vector2> path0 = new List<Vector2>();
            path0.Add(new Vector2(4f, 1f));
            path0.Add(new Vector2(2f, 0f));
            path0.Add(new Vector2(0f, 1f));
            pathWaypoints.Add(path0);
        }

        public EnemyManager(Texture2D texture, Rectangle initialFrame, int frameCount, PlayerManager playerManager, Rectangle screenBound)
        {
            this.texture = texture;
            this.initialFrame = initialFrame;
            this.frameCount = frameCount;
            this.playerManager = playerManager;
            this.screenBound = screenBound;

            EnemyShotManager = new ShotManager(texture, new Rectangle(0, 300, 5, 5), 4, 2, 150f, screenBound);

            setUpWaypoints();
        }

        public void SpawnEnemy(int path, float spawnTimer)
        {
            Enemy thisEnemy = new Enemy(
                texture,
                new Vector2(-500f, -500f),
                initialFrame,
                frameCount,
                new Rectangle(80, 80, screenBound.Width - 160, screenBound.Height - 160),
                new Vector2(4f, 2f));
            thisEnemy.spawnTimer = spawnTimer;
            thisEnemy.AddWaypoint(pathWaypoints[path]);

            Enemies.Add(thisEnemy);
        }

        public void SpawnWave(int waveType)
        {
            int countEnemy = 5;
            nextShipTimer = 0f;
            
            for (int i = 0; i <= countEnemy; i++)
            {
                SpawnEnemy(waveType, nextShipTimer);
                nextShipTimer += nextShipMinTime;
            }
        }

        private void updateWaveSpawn(GameTime gameTime)
        {
            nextWaveTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (nextWaveTime > nextWaveMinTimer)
            {
                SpawnWave(rand.Next(0, pathWaypoints.Count));

                nextWaveTime = 0f;
            }
        }

        private void enemyShot(int i)
        {
            if ((float)rand.Next(0, 1000) / 2 <= shipShotChance)
            {
                Vector2 fireLoc = Enemies[i].EnemySprite.Location;
                fireLoc += Enemies[i].gunOffset;

                Vector2 shotDirection = playerManager.playerSprite.Center - fireLoc;
                shotDirection.Normalize();

                EnemyShotManager.FireShot(fireLoc, shotDirection, false);
            }
        }

        public void Update(GameTime gameTime)
        {
            EnemyShotManager.Update(gameTime);

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime);

                if (!Enemies[i].IsActive())
                    Enemies.RemoveAt(i);
                else
                    enemyShot(i);
            }
            if (Active)
                updateWaveSpawn(gameTime);
        }

        public void Draw(SpriteBatch spriteBath)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.IsActive())
                    enemy.Draw(spriteBath);
            }
        }
    }
}
