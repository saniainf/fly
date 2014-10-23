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
    class Level : GameObject
    {
        private List<Layer> layers;

        public Level(ContentManager content, Camera camera)
        {
            tm.Map map = tm.IOMap.Open(@"levelTest1.tmx", content, @"Textures\Levels\");
            //---

            layers = new List<Layer>();
            camera.Limits = new Rectangle(0, 0, map.TileWidth * map.Width, map.TileHeight * map.Height);

            foreach (TiledMap.Layer tmLayer in map.Layers)
            {
                float[] properties = tmLayer.Properties["parallax"].ToFloat(';');
                layers.Add(new Layer(tmLayer, map, camera) { Parallax = new Vector2((properties[0]), (properties[1])) });
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch);
        }
    }
}
