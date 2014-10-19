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
        List<Sprite> sprites = new List<Sprite>();

        public Layer(tm.Layer tmLayer, tm.Map tmMap)
        {
            int sSize = 32;
            for (int y = 0; y < tmLayer.Data.GetLength(1); y++)
            {
                for (int x = 0; x < tmLayer.Data.GetLength(0); x++)
                {
                    int k = tmLayer.Data[x, y];
                    if (k != 0)
                    {
                        sprites.Add(new Sprite(
                            tmMap.TileSets[tmMap.SourceTileSet[k] - 1].SpriteSheet,
                            new Rectangle(x * sSize, y * sSize, sSize, sSize),
                            tmMap.SourceRectangle[k]));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in sprites)
                sprite.Draw(spriteBatch);
        }
    }
}
