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

        public Level(ContentManager content, Camera camera)
        {
            tm.Map map = tm.IOMap.Open(@"levelTest1.tmx", content, @"Textures\Levels\");
            //---

            layers = new List<Layer>();

            //foreach (TiledMap.Layer tmLayer in map.Layers)
            //{
            //    layers.Add(new Layer(tmLayer, map, camera));
            //}
            camera.Limits = new Rectangle(0, 0, 4096, 640);
            layers.Add(new Layer(map.Layers[0], map, camera) { Parallax = new Vector2(0.8f, 1.0f) });
            layers.Add(new Layer(map.Layers[1], map, camera) { Parallax = new Vector2(1.0f, 1.0f) });
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch);
        }
    }
}
