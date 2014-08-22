using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fly.Classes
{
    class Star : Sprite
    {
        public int Speed = 1;

        public Star(Texture2D texture, Rectangle initialFrame, Vector2 location, Vector2 velocity, int speed)
            : base(texture, initialFrame, location, velocity)
        {
            Speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeForCurrentFrame += elapsed;

            if (timeForCurrentFrame >= FrameTime)
            {
                currentFrame = (currentFrame + 1) % (frames.Count);
                timeForCurrentFrame = 0;
            }

            location += (velocity * elapsed);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Center, Source, tintColor, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 1), Source, tintColor * 0.90f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 2), Source, tintColor * 0.85f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 3), Source, tintColor * 0.80f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 4), Source, tintColor * 0.75f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 5), Source, tintColor * 0.70f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 6), Source, tintColor * 0.65f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 7), Source, tintColor * 0.60f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 8), Source, tintColor * 0.55f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(Texture, new Vector2(Center.X, Center.Y - 9), Source, tintColor * 0.50f, rotation, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
