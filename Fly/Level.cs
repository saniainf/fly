#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Fly.Managers;
using Fly.Classes;
#endregion

namespace Fly
{
    public class Level : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameStates { TitleScreen, Playing, PlayerDead, GameOver };
        GameStates gameStates = GameStates.Playing;
        Texture2D spriteSheet;

        StarField starField;
        AsteroidManager asteroidManager;

        public Level()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteSheet = Content.Load<Texture2D>(@"Textures\spriteSheet");

            starField = new StarField(
                this.Window.ClientBounds.Width, this.Window.ClientBounds.Height,
                100, new Vector2(-200f, 0f), spriteSheet, new Rectangle(0, 1020, 2, 2));

            asteroidManager = new AsteroidManager(
                10, spriteSheet, new Rectangle(0, 0, 50, 50), 20,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameStates)
            {
                case GameStates.TitleScreen:
                    break;

                case GameStates.Playing:
                    starField.Update(gameTime);
                    asteroidManager.Update(gameTime);
                    break;

                case GameStates.PlayerDead:
                    break;

                case GameStates.GameOver:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();

            if (gameStates == GameStates.Playing)
            {
                starField.Draw(spriteBatch);
                asteroidManager.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }


}
