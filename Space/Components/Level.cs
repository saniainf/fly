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
    class Level : SGComponent
    {
        private List<Layer> layers = new List<Layer>();
        private StarField starField = new StarField();

        public Level(ContentManager content)
        {
            loadMap(content);
            Main.sgComponents.Add(this);
        }

        private void loadMap(ContentManager content)
        {
            tm.Map map = tm.IOMap.Open(@"levelTest1.tmx", content, @"Textures\Levels\");
            //---
            Camera.Limits = new Rectangle(0, 0, map.TileWidth * map.Width, map.TileHeight * map.Height);

            foreach (TiledMap.Layer tmLayer in map.Layers)
            {
                float[] properties = tmLayer.Properties["parallax"].ToFloat(';');
                layers.Add(new Layer(tmLayer, map) { Parallax = new Vector2((properties[0]), (properties[1])) });
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
