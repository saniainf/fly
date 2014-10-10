using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TiledMap
{
    public static class IOMap
    {
        public static Map Open(string filename)
        {
            Map result = new Map();
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xmlRootNode;

            xmlDoc.Load(filename);
            xmlRootNode = xmlDoc.DocumentElement;
            result.Version = xmlRootNode.ReadTag("version");
            result.Orientaton = xmlRootNode.ReadOrientation("orientation");
            result.Width = xmlRootNode.ReadInt("width");
            result.Height = xmlRootNode.ReadInt("height");
            result.TileWidth = xmlRootNode.ReadInt("tilewidth");
            result.TileHeight = xmlRootNode.ReadInt("tileheight");

            foreach (XmlNode xmlNode in xmlRootNode)
            {
                switch (xmlNode.Name)
                {
                    case "tileset":
                        readTileSet();
                        break;

                    case "layer":
                        break;

                    case "objectgroup" :
                        break;

                }
            }

            return result;
        }

        static void readTileSet()
        {

        }
    }
}
