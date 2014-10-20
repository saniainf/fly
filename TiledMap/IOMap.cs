using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace TiledMap
{
    public static class IOMap
    {
        public static Map Open(string filename, ContentManager content, string contentPath)
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
                        readTileSet(xmlNode, ref result, content, contentPath);
                        break;

                    case "layer":
                        readLayer(xmlNode, ref result);
                        break;

                    case "objectgroup":
                        readObjectGroup(xmlNode, ref result);
                        break;

                }
            }

            return result;
        }

        static void readTileSet(XmlNode node, ref Map map, ContentManager content, string contentPath)
        {
            TileSet tileSet = new TileSet();
            tileSet.FirstGid = node.ReadInt("firstgid");
            tileSet.Name = node.ReadTag("name");
            tileSet.TileWidth = node.ReadInt("tilewidth");
            tileSet.TileHeight = node.ReadInt("tileheight");
            tileSet.SpriteSheet = content.Load<Texture2D>(contentPath + tileSet.Name);

            if (node.HasChildNodes)
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    switch (subNode.Name)
                    {
                        case "image":
                            tileSet.Source = subNode.ReadTag("source");
                            tileSet.Width = subNode.ReadInt("width");
                            tileSet.Height = subNode.ReadInt("height");
                            break;

                        case "tile":
                            break;
                    }
                }
            }
            map.AddTileSet(tileSet);
        }

        static void readLayer(XmlNode node, ref Map map)
        {
            Layer layer = new Layer(node.ReadInt("width"), node.ReadInt("height"));
            layer.Name = node.ReadTag("name");
            layer.Visible = node.ReadInt("visible") == 1;

            foreach (XmlNode subNode in node)
            {
                switch (subNode.Name)
                {
                    case "properties":
                        foreach (XmlNode propNode in subNode)
                        {
                            layer.Properties.Add(propNode.ReadTag("name"), propNode.ReadTag("value"));
                        }
                        break;

                    case "data":
                        string[] data = subNode.InnerXml.Trim('\n', ' ').Replace("\n", "").Split(',', '\n');
                        int k = 0;
                        for (int y = 0; y < layer.Height; y++)
                        {
                            for (int x = 0; x < layer.Width; x++)
                            {
                                layer.Data[x, y] = int.Parse(data[k]);
                                k++;
                            }
                        }
                        break;
                }
            }
            map.Layers.Add(layer);
        }

        static void readObjectGroup(XmlNode node, ref Map map)
        {
            ObjectGroup objectGroup = new ObjectGroup();
            objectGroup.Name = node.ReadTag("name");
            objectGroup.Visible = node.ReadInt("visible") == 1;

            if (node.HasChildNodes)
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    MapObject mapObject = new MapObject();
                    mapObject.Name = subNode.ReadTag("name");
                    mapObject.Type = subNode.ReadTag("type");
                    mapObject.X = subNode.ReadInt("x");
                    mapObject.Y = subNode.ReadInt("y");
                    mapObject.Width = subNode.ReadInt("width");
                    mapObject.Height = subNode.ReadInt("height");
                    objectGroup.MapObjects.Add(mapObject);
                }
            }
            map.ObjectGroups.Add(objectGroup);
        }
    }
}
