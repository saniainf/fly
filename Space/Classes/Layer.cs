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

namespace Space.Classes
{
    class Layer
    {
        public Vector2 Parallax;
        public List<Sprite> Sprites = new List<Sprite>();
        private Camera camera;
        

        public Layer(tm.Layer tmLayer, tm.Map tmMap, Camera camera)
        {
            int tileWidth = tmMap.TileWidth;
            int tileHeight = tmMap.TileHeight;

            this.camera = camera;
            Parallax = Vector2.Zero;

            for (int y = 0; y < tmLayer.Data.GetLength(1); y++)
            {
                for (int x = 0; x < tmLayer.Data.GetLength(0); x++)
                {
                    int k = tmLayer.Data[x, y];
                    if (k != 0)
                    {
                        Sprites.Add(new Sprite(
                            tmMap.TileSets[tmMap.SourceTileSet[k]].SpriteSheet,
                            new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight),
                            tmMap.SourceRectangle[k]));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, camera.GetViewMatrix(Parallax));

            foreach (Sprite sprite in Sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
