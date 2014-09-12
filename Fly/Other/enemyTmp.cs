using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarOnInfinityCourse
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
	//float H = w*sinA + h*cosA;
	//float W = w*cosA + h*sinA;
        const float SPEED = 0.2f;           // laps per second
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle viewport;
        Texture2D car;
        Vector2 carCenter;
        Curve xCurve = new Curve();
        Curve yCurve = new Curve();
        Vector2 position;
        float rotation = 0f;
        float time;
        bool isActive = false;

        //float[] xValues = { 4f, 2f, 1f, 0.323f, 0f, 0.323f, 1f, 2f, 4f };
        //float[] yValues = { 0.25f, 1.277f, 2f, 1.677f, 1f, 0.323f, 0f, 0.802f, 1.75f };

        //float[] xValues = { 4f, 3.5f, 3.0f, 2.5f, 2.0f, 1.5f, 1f, 0.5f, 0f };
        //float[] yValues = { 0.5f, 1.5f, 0.5f, 1.5f, 0.5f, 1.5f, 0.5f, 1.5f, 0.5f };

        float[] xValues = { 4f, 0f, 0f, 4f, 4f };
        float[] yValues = { 0f, 0f, 2f, 2f, 0f };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //float[] xValues = { 0, 0.293f, 1, 1.707f, 2, 2.293f, 3, 3.707f, 
            //                    4, 3.707f, 3, 2.293f, 2, 1.707f, 1, 0.293f };
            //float[] yValues = { 1, 0.293f, 0, 0.293f, 1, 1.707f, 2, 1.707f, 
            //                    1, 0.293f, 0, 0.293f, 1, 1.707f, 2, 1.707f };

            //for (int i = -1; i < 18; i++)
            //{
            //    int index = (i + 16) % 16;
            //    float t = 0.0625f * i;
            //    xCurve.Keys.Add(new CurveKey(t, xValues[index]));
            //    yCurve.Keys.Add(new CurveKey(t, yValues[index]));
            //}
            //xCurve.ComputeTangents(CurveTangent.Smooth);
            //yCurve.ComputeTangents(CurveTangent.Smooth);

            for (int i = 0; i < xValues.Length; i++)
            {
                float t = (float)(1.0f / (float)xValues.Length) * (float)i;
                xCurve.Keys.Add(new CurveKey(t, xValues[i]));
                yCurve.Keys.Add(new CurveKey(t, yValues[i]));
            }
            //xCurve.ComputeTangents(CurveTangent.Smooth);
            //yCurve.ComputeTangents(CurveTangent.Smooth);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewport = new Rectangle(10, 10, this.Window.ClientBounds.Width - 20, this.Window.ClientBounds.Height - 20);
            car = this.Content.Load<Texture2D>("Car");
            carCenter = new Vector2(car.Width / 2, car.Height / 2);
        }

        protected override void UnloadContent()
        {
        }

        public bool WaypointReached()
        {
            Vector2 endPoint = new Vector2((viewport.Width / 4) * xValues[xValues.Length - 1], (viewport.Height / 2) * yValues[yValues.Length - 1]);
            return (Vector2.Distance(position, endPoint) < (float)car.Width / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyS;
            keyS = Keyboard.GetState();

            if (!isActive && keyS.IsKeyDown(Keys.Space))
                isActive = true;

            if (isActive)
            {
                time = (SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds + time) % 1;
                float x = (viewport.Width / 4) * xCurve.Evaluate(time) + viewport.X;
                float y = (viewport.Height / 2) * yCurve.Evaluate(time) + viewport.Y;
                position = new Vector2(x, y);
                System.Diagnostics.Debug.WriteLine(time);

                //if (WaypointReached())
                //    isActive = false;
            }
            //rotation = MathHelper.PiOver2 + (float)
            //    Math.Atan2(GetValue(t + 0.001f, false) - GetValue(t - 0.001f, false),
            //               GetValue(t + 0.001f, true) - GetValue(t - 0.001f, true));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(car, position, null, Color.White, rotation,
                             carCenter, 1, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
