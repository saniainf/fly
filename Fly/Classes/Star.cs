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
       

        public Star(Texture2D texture, Rectangle initialFrame, Vector2 location, Vector2 velocity)
            : base(texture, initialFrame, location, velocity)
        {
           
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
        }
    }
}
