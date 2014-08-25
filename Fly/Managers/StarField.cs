using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;

namespace Fly.Managers
{
    class StarField
    {
        private List<Star> stars = new List<Star>();
        private int screenWidth = 800;
        private int screenHeight = 600;
        private Random rand = new Random();
        private Color[] colors = { Color.White, Color.Yellow, Color.Wheat, Color.WhiteSmoke, Color.SlateGray };

        public StarField(int screenWidth, int screenHeight, int starCount, Vector2 starVelocity, Texture2D texture, Rectangle frameRectangle)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            for (int i = 0; i < starCount; i++)
            {
                stars.Add(new Star(
                    texture,
                    frameRectangle,
                    new Vector2(rand.Next(0, screenWidth), rand.Next(0, screenHeight)),
                    starVelocity));

                Color starColor = colors[rand.Next(0, colors.Count())];
                starColor *= (float)(rand.Next(30, 80) / 100f);
                stars[stars.Count() - 1].TintColor = starColor;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Sprite star in stars)
            {
                star.Update(gameTime);
                //if (star.Location.Y > screenHeight)
                //    star.Location = new Vector2(random.Next(0, screenWidth), 0);
                if (star.Location.X < 0)
                    star.Location = new Vector2(screenWidth, rand.Next(0, screenHeight));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite star in stars)
            {
                star.Draw(spriteBatch);
            }
        }
    }
}
