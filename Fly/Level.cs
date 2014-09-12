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
        Texture2D spaceShip;
        Texture2D interfaceScreen;

        //test
        Texture2D spriteRobots;

        StarField starField;
        AsteroidManager asteroidManager;
        PlayerManager playerManager;
        EnemyManager enemyManager;

        //test
        TestSpriteManger testSprite;

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
            spaceShip = Content.Load<Texture2D>(@"Textures\spaceShip");
            interfaceScreen = Content.Load<Texture2D>(@"Textures\interface");

            //test
            spriteRobots = Content.Load<Texture2D>(@"Textures\asteroid");

            starField = new StarField(
                this.Window.ClientBounds.Width, this.Window.ClientBounds.Height,
                100, new Vector2(-120f, 0f), spriteSheet, new Rectangle(0, 1020, 2, 2));

            asteroidManager = new AsteroidManager(
                10, spriteSheet, new Rectangle(0, 550, 50, 50), 1,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height);

            playerManager = new PlayerManager(spriteSheet, spaceShip, new Rectangle(0, 0, 141, 62), 3,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height));

            enemyManager = new EnemyManager(spriteSheet, new Rectangle(0, 200, 50, 50), 6, playerManager,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height));


            // test
            testSprite = new TestSpriteManger(spriteRobots, new Rectangle(0, 0, 141, 62), 1);

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
                    playerManager.Update(gameTime);
                    enemyManager.Update(gameTime);

                    //test
                    testSprite.Update(gameTime);
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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null);

            if (gameStates == GameStates.Playing)
            {
                starField.Draw(spriteBatch);
                asteroidManager.Draw(spriteBatch);
                playerManager.Draw(spriteBatch);
                enemyManager.Draw(spriteBatch);

                spriteBatch.Draw(interfaceScreen, new Vector2(0, 0), Color.White);
                //test
                //testSprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }


}
