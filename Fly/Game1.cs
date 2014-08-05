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

        private uint[] pixelDeformData; //массив круга деформации
        private List<Vector2> bullets; //массив пуль

        private Vector2 mousePosition;
        private MouseState currentMouseState;

        private float speed = 2f;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
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

            //объявление массива круга деформации
            pixelDeformData = new uint[textureDeform.Width * textureDeform.Height];
            //заполнение массива
            textureDeform.GetData(pixelDeformData, 0, textureDeform.Width * textureDeform.Height);
            //инициализация массива пуль, Count 0
            bullets = new List<Vector2>();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            UpdateMouse();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //блок отрисовки

            spriteBatch.Draw(textureSky, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(textureGround, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(textureDeform, mousePosition, Color.White);

            if (bullets.Count > 0)
            {
                foreach (Vector2 bullet in bullets)
                {
                    spriteBatch.Draw(textureBullet, bullet, Color.White);
                }
            }

            spriteBatch.End();//конец отрисовки

            base.Draw(gameTime);
        }


        protected void UpdateMouse()
        {
            MouseState previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            //fire
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
              currentMouseState.LeftButton == ButtonState.Released)
            {
                bullets.Add(new Vector2(mousePosition.X, mousePosition.Y));
            }

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
                    bullets[i] = new Vector2(bullets[i].X, bullets[i].Y + speed);

                    //проверка выхода за границы экрана
                    if (bullets[i].X < 0 ||
                        bullets[i].Y < 0 ||
                        bullets[i].X > graphics.PreferredBackBufferWidth ||
                        bullets[i].Y > graphics.PreferredBackBufferHeight)
                        bullets.RemoveAt(i);

                    //проверка столкновения с землей
                    else if (pixelGroundData[(int)bullets[i].X + (int)bullets[i].Y * textureGround.Width] != 0)
                    {
                        float correctX = bullets[i].X - textureDeform.Width / 2;
                        float correctY = bullets[i].Y - textureDeform.Height / 2;

                        for (int x = 0; x < textureDeform.Width; x++)
                        {
                            for (int y = 0; y < textureDeform.Height; y++)
                            {
                                if ((correctX + x) < textureGround.Width &&
                                    (correctY + y) < textureGround.Height &&
                                    (correctX + x) >= 0 &&
                                    (correctY + y) >= 0)
                                {
                                    if (pixelDeformData[x + y * textureDeform.Width] != 0 &&
                                        pixelGroundData[((int)correctX + x) + ((int)correctY + y) * textureGround.Width] != 0)
                                    {
                                        //если текущий пиксель в деф.круге белый                
                                        if (pixelDeformData[x + y * textureDeform.Width] == 4294967295)
                                        {
                                            //заменить на альфу в текстуре земли
                                            pixelGroundData[((int)correctX + x) + ((int)correctY + y)
                                              * textureGround.Width] = 0;
                                        }
                                        else
                                        {
                                            //если не белый то пиксель с деф.текстуры поставить в текстуру земли
                                            pixelGroundData[((int)correctX + x) + ((int)correctY + y)
                                              * textureGround.Width] = pixelDeformData[x + y * textureDeform.Width];
                                        }
                                    }
                                }
                            }
                        }
                        bullets.RemoveAt(i);
                    }
                    //обновить текстуру земли
                    textureGround.SetData(pixelGroundData);
                }
            }
        }
    }
}
