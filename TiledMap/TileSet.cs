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

namespace TiledMap
{
    public class TileSet
    {
        public int FirstGid;
        public string Name;
        public int TileWidth;
        public int TileHeight;
        public string Source;
        public int Width;
        public int Height;
        public Texture2D SpriteSheet;
    }
}
