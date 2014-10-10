using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiledMap
{
    public class Map
    {
        public string Version;
        public MapOrientation Orientaton;
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;



        public enum MapOrientation
        {
            Orthogonal,
            Isometric
        }
    }
}
