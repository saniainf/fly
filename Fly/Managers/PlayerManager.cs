using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;
using Microsoft.Xna.Framework.Input;

namespace Fly.Managers
{
    class PlayerManager
    {
        public CollisionSprite playerSprite;
        private float playerSpeed = 260.0f;
        private Rectangle playerAreaLimit;
        private float playerShotSpeed = 400.0f;

        public long PlayerScore = 0;
        public int LivesRemaining = 3;
        public bool Destroyed = false;

        private Vector2 gunOffest = new Vector2(141, 53);
        private float shotTimer = 0.0f;
        private float minShotTimer = 0.2f;
        private int playerRadius = 15;

        public ShotManager PlayerShotManager;

        public PlayerManager(Texture2D texture, Texture2D textureShip, Rectangle initialFrame, int frameCount, Rectangle screenBound)
        {
            playerSprite = new CollisionSprite(textureShip, initialFrame, new Vector2(500, 500), Vector2.Zero);
            PlayerShotManager = new ShotManager(texture, new Rectangle(0, 300, 9, 9), 4, 9, playerShotSpeed, screenBound);
            playerAreaLimit = new Rectangle(0, 0, screenBound.Width / 2, screenBound.Height);

            for (int i = 1; i < frameCount; i++)
                playerSprite.AddFrame(new Rectangle(
                    initialFrame.X + (initialFrame.Width * i),
                    initialFrame.Y,
                    initialFrame.Width,
                    initialFrame.Height));

            playerSprite.CollisionRadius = playerRadius;
        }

        private void FireShot()
        {
            if (shotTimer >= minShotTimer)
            {
                PlayerShotManager.FireShot(playerSprite.Location + gunOffest, new Vector2(1, 0), true);
                shotTimer = 0.0f;
            }
        }

        private void HandleKeyboardInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
                playerSprite.Velocity += new Vector2(0, -1);

            if (keyState.IsKeyDown(Keys.Down))
                playerSprite.Velocity += new Vector2(0, 1);

            if (keyState.IsKeyDown(Keys.Left))
                playerSprite.Velocity += new Vector2(-1, 0);

            if (keyState.IsKeyDown(Keys.Right))
                playerSprite.Velocity += new Vector2(1, 0);

            if (keyState.IsKeyDown(Keys.Space))
                FireShot();
        }

        private void imposeMovementLimits()
        {
            Vector2 location = playerSprite.Location;

            if (location.X < playerAreaLimit.X)
                location.X = playerAreaLimit.X;

            if (location.X > playerAreaLimit.Right - playerSprite.Source.Width)
                location.X = playerAreaLimit.Right - playerSprite.Source.Width;

            if (location.Y < playerAreaLimit.Y)
                location.Y = playerAreaLimit.Y;

            if (location.Y > playerAreaLimit.Bottom - playerSprite.Source.Height)
                location.Y = playerAreaLimit.Bottom - playerSprite.Source.Height;

            playerSprite.Location = location;
        }

        public void Update(GameTime gameTime)
        {
            PlayerShotManager.Update(gameTime);

            if (!Destroyed)
            {
                playerSprite.Velocity = Vector2.Zero;

                shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                HandleKeyboardInput(Keyboard.GetState());

                playerSprite.Velocity.Normalize();
                playerSprite.Velocity *= playerSpeed;

                playerSprite.Update(gameTime);
                imposeMovementLimits();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerShotManager.Draw(spriteBatch);

            if (!Destroyed)
                playerSprite.Draw(spriteBatch);
        }
    }
}
