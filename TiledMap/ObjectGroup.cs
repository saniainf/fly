using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiledMap
{
    public class ObjectGroup
    {
        public string Name;
        public bool Visible;
        public List<MapObject> MapObjects;

        public ObjectGroup()
        {
            MapObjects = new List<MapObject>();
        }
    }
}
