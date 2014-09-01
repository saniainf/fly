#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Fly
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D textureSky; //текстура неба
        private Texture2D textureGround; //текстура земли
        private Texture2D textureDeform; //текстура круга деформации
        private Texture2D textureBullet; //текстура пули
        private Texture2D texturePlane;

        private uint[] pixelDeformData; //массив круга деформации
        private List<Bullet> bullets; //массив пуль

        private Vector2 planePosition;
        private Vector2 mousePosition;
        private MouseState currentMouseState;
        private KeyboardState currentKeyboardState;

        private float speedBullet = 4f;
        private float speedPlane = 2f;
        private float rotatePlate;
        private float correctGroundX = 112f;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";

            planePosition = new Vector2(400f, 300f);
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

            //загрузка текстур
            textureSky = Content.Load<Texture2D>("sky");
            textureGround = Content.Load<Texture2D>("level");
            textureDeform = Content.Load<Texture2D>("deform");
            textureBullet = Content.Load<Texture2D>("bullet");
            texturePlane = Content.Load<Texture2D>("plane");

            //объявление массива круга деформации
            pixelDeformData = new uint[textureDeform.Width * textureDeform.Height];
            //заполнение массива
            textureDeform.GetData(pixelDeformData, 0, textureDeform.Width * textureDeform.Height);
            //инициализация массива пуль, Count 0
            bullets = new List<Bullet>();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            UpdatePlane();
            UpdateBullets();
            //UpdateTest();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //    PlayerPosition.Y -= 10f;
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //    PlayerPosition.Y += 10f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rotatePlate -= 0.04f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rotatePlate += 0.04f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null); //блок отрисовки

            spriteBatch.Draw(textureSky, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(textureGround, new Vector2(-correctGroundX, 0), Color.White);
            spriteBatch.Draw(texturePlane, planePosition, null, Color.White, rotatePlate, new Vector2(texturePlane.Width / 2, texturePlane.Height / 2), 1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(textureDeform, mousePosition, Color.White);

            if (bullets.Count > 0)
            {
                foreach (Bullet bullet in bullets)
                {
                    spriteBatch.Draw(textureBullet, bullet.position, Color.White);
                }
            }

            spriteBatch.End();//конец отрисовки

            base.Draw(gameTime);
        }

        protected void UpdateTest()
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //{
            //объявление массива земли
            uint[] pixelGroundData = new uint[(textureGround.Width - 1) * textureGround.Height];
            uint[] pixelGroundData1px = new uint[textureGround.Height];
            //заполнение массива земли
            textureGround.GetData(0, new Rectangle(0, 0, 1, 600), pixelGroundData1px, 0, 600);
            textureGround.GetData(0, new Rectangle(1, 0, 1023, 600), pixelGroundData, 0, 613800);
            //обновить текстуру земли
            textureGround.SetData(0, new Rectangle(0, 0, 1023, 600), pixelGroundData, 0, 613800);
            textureGround.SetData(0, new Rectangle(1023, 0, 1, 600), pixelGroundData1px, 0, 600);
            //}
        }

        protected void UpdatePlane()
        {
            MouseState previousMouseState = currentMouseState;
            KeyboardState previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
            mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            Vector2 velocityPlane = speedPlane * new Vector2((float)Math.Cos(rotatePlate), (float)Math.Sin(rotatePlate));
            planePosition += velocityPlane;

            //fire
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Released)
                bullets.Add(new Bullet(rotatePlate, new Vector2(mousePosition.X, mousePosition.Y)));

            if (previousKeyboardState.IsKeyUp(Keys.Space) &&
                currentKeyboardState.IsKeyDown(Keys.Space))
                bullets.Add(new Bullet(rotatePlate, planePosition));
        }

        protected void UpdateBullets()
        {
            if (bullets.Count > 0)
            {
                //объявление массива земли
                uint[] pixelGroundData = new uint[textureGround.Width * textureGround.Height];
                //заполнение массива земли
                textureGround.GetData(pixelGroundData, 0, textureGround.Width * textureGround.Height);

                //перебор всех пуль
                for (int i = 0; i < bullets.Count; i++)
                {
                    //движение пули
                    //bullets[i].position = new Vector2(bullets[i].position.X, bullets[i].position.Y + speed);
                    Vector2 velocityBullet = speedBullet * new Vector2((float)Math.Cos(bullets[i].angle), (float)Math.Sin(bullets[i].angle));
                    bullets[i].position += velocityBullet;

                    //проверка выхода за границы экрана
                    if (bullets[i].position.X < 0 ||
                        bullets[i].position.Y < 0 ||
                        bullets[i].position.X >= graphics.PreferredBackBufferWidth ||
                        bullets[i].position.Y >= graphics.PreferredBackBufferHeight)
                        bullets.RemoveAt(i);

                    //проверка столкновения с землей
                    else if (pixelGroundData[(int)bullets[i].position.X + (int)bullets[i].position.Y * textureGround.Width] != 0)
                    {
                        float correctX = bullets[i].position.X - textureDeform.Width / 2;
                        float correctY = bullets[i].position.Y - textureDeform.Height / 2;

                        for (int x = 0; x < textureDeform.Width; x++)
                        {
                            for (int y = 0; y < textureDeform.Height; y++)
                            {
                                if ((correctY + y) < textureGround.Height &&
                                    (correctY + y) >= 0)
                                {
                                    if (pixelDeformData[x + y * textureDeform.Width] != 0 &&
                                        pixelGroundData[(x + (int)correctX + ((int)correctY + y)) * textureGround.Width] != 0)
                                    {
                                        //если текущий пиксель в деф.круге белый                
                                        if (pixelDeformData[x + y * textureDeform.Width] == 4294967295)
                                        {
                                            //заменить на альфу в текстуре земли
                                            pixelGroundData[(x + (int)correctX) + ((int)correctY + y) * textureGround.Width] = 0;
                                        }
                                        else
                                        {
                                            //если не белый то пиксель с деф.текстуры поставить в текстуру земли
                                            pixelGroundData[(x + (int)correctX) + ((int)correctY + y) * textureGround.Width] = pixelDeformData[x + y * textureDeform.Width];
                                        }
                                    }
                                }
                            }
                        }
                        bullets.RemoveAt(i);
                    }
                }
                //обновить текстуру земли
                textureGround.SetData(pixelGroundData);
            }
        }
    }

    public class Bullet
    {
        public float angle;
        public Vector2 position;

        public Bullet(float _angle, Vector2 _position)
        {
            angle = _angle;
            position = _position;
        }
    }
}
