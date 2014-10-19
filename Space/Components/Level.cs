#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Space.Classes;
using tm = TiledMap;
#endregion

namespace Space.Components
{
    class Level
    {
        private List<Layer> layers;

        public Level(ContentManager content)
        {
            tm.Map map = tm.IOMap.Open(@"levelTest1.tmx", content, @"Textures\Levels\");
            //---

            layers = new List<Layer>();

            foreach (TiledMap.Layer tmLayer in map.Layers)
            {
                layers.Add(new Layer(tmLayer, map));
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
