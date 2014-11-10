using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Space.Classes;

namespace Space.Components
{
    class StarField : SGComponent
    {
        private List<SGAnimationObject> stars;

        public StarField()
        {
            stars = new List<SGAnimationObject>();
            Main.sgComponents.Add("starfield", this);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (SGAnimationObject star in stars)
            {
                star.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (SGAnimationObject star in stars)
            {
                star.Draw(spriteBatch);
            }
        }
    }
}
