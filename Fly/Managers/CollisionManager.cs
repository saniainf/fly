using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;

namespace Fly.Managers
{
    class CollisionManager
    {
        private AsteroidManager asteroidManager;
        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private ExplosionManager explosionManager;
        private Vector2 offScreen = new Vector2(-300, -500);
        private Vector2 shotToAsteroidImpact = new Vector2(20, 0);
        private int enemyPointValue = 100;

        private bool iddqd = true;

        public CollisionManager(AsteroidManager asteroidManager, PlayerManager playerManager,
                                EnemyManager enemyManager, ExplosionManager explosionManager)
        {
            this.asteroidManager = asteroidManager;
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.explosionManager = explosionManager;
        }

        private void checkShotToEnemyCollisions()
        {
            foreach (CollisionSprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    if (shot.IsCircleColliding(enemy.EnemySprite.Center, enemy.EnemySprite.CollisionRadius))
                    {
                        shot.Location = offScreen;
                        enemy.Destroyed = true;
                        playerManager.PlayerScore += enemyPointValue;
                        explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);
                    }
                }
            }
        }

        private void checkShotToAsteroidCollisions()
        {
            foreach (CollisionSprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (CollisionSprite asteroid in asteroidManager.Asteroids)
                {
                    if (shot.IsCircleColliding(asteroid.Center, asteroid.CollisionRadius))
                    {
                        shot.Location = offScreen;
                        asteroid.Velocity += shotToAsteroidImpact;
                    }
                }
            }
        }

        private void checkShotToPlayerCollisions()
        {
            foreach (CollisionSprite shot in enemyManager.EnemyShotManager.Shots)
            {
                if (shot.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                {
                    shot.Location = offScreen;
                    if (!iddqd)
                        playerManager.Destroyed = true;
                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
                }
            }
        }

        private void checkEnemyToPlayerCollision()
        {
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                if (enemy.EnemySprite.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                {
                    enemy.Destroyed = true;
                    explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);

                    if (!iddqd)
                        playerManager.Destroyed = true;
                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
                }
            }
        }

        private void checkAsteroidToPlayerCollision()
        {
            foreach (CollisionSprite asteroid in asteroidManager.Asteroids)
            {
                if (asteroid.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                {
                    explosionManager.AddExplosion(asteroid.Center, asteroid.Velocity / 10);
                    asteroid.Location = offScreen;

                    if (!iddqd)
                        playerManager.Destroyed = true;
                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
                }
            }
        }

        public void CheckCollisions()
        {
            checkShotToEnemyCollisions();
            checkShotToAsteroidCollisions();
            if (!playerManager.Destroyed)
            {
                checkShotToPlayerCollisions();
                checkEnemyToPlayerCollision();
                checkAsteroidToPlayerCollision();
            }
        }

    }
}
