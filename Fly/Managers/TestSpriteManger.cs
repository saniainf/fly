using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fly.Classes;

namespace Fly.Managers
{
    class TestSpriteManger
    {
        public Sprite TestSprite;

        private Vector2 location = new Vector2(400, 300);
        private Vector2 velocity = Vector2.Zero;

        public TestSpriteManger(Texture2D texture, Rectangle initialFrame, int frameCount)
        {
            Sprite sprite = new Sprite(texture, initialFrame, location, velocity);

            for (int i = 1; i < frameCount; i++)
                sprite.AddFrame(new Rectangle(
                    initialFrame.X + (initialFrame.Width * i),
                    initialFrame.Y,
                    initialFrame.Width,
                    initialFrame.Height));
            sprite.FrameTime = 0.15f;

            TestSprite = sprite;
        }

        public void Update(GameTime gameTime)
        {
            TestSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                TestSprite.Draw(spriteBatch);
        }
    }
}
