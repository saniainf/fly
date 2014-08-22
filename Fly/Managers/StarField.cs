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
        private List<Sprite> stars = new List<Sprite>();
        private int screenWidth = 800;
        private int screenHeight = 600;
        private Random random = new Random();
        private Color[] colors = { Color.White, Color.Yellow, Color.Wheat, Color.WhiteSmoke, Color.SlateGray };

        public StarField(int screenWidth, int screenHeight, int starCount, Vector2 starVelocity, Texture2D texture, Rectangle frameRectangle)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            for (int i = 0; i < starCount; i++)
            {
                stars.Add(new Sprite(texture, frameRectangle, new Vector2(random.Next(0, screenWidth), random.Next(0, screenHeight)),
                    new Vector2(starVelocity.X, random.Next((int)starVelocity.Y - 20, (int)starVelocity.Y))));
                Color starColor = colors[random.Next(0, colors.Count())];
                starColor *= (float)(random.Next(30, 80) / 100f);
                stars[stars.Count() - 1].TintColor = starColor;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Sprite star in stars)
            {
                star.Update(gameTime);
                if (star.Location.Y > screenHeight)
                    star.Location = new Vector2(random.Next(0, screenWidth), 0);
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
