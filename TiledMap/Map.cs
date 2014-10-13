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

        public List<TileSet> TileSets;
        public List<Layer> Layers;
        public List<ObjectGroup> ObjectGroups;

        public enum MapOrientation
        {
            Orthogonal,
            Isometric
        }

        public Map()
        {
            TileSets = new List<TileSet>();
            Layers = new List<Layer>();
            ObjectGroups = new List<ObjectGroup>();
        }
    }
}
