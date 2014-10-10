using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TiledMap
{
    public static class Xml
    {
        public static string ReadTag(this XmlNode node, string tagName, string defaultValue = "")
        {
            if (node.Attributes != null)
                return node.Attributes[tagName] != null ? node.Attributes[tagName].Value : defaultValue;
            else
                return defaultValue;
        }

        public static Map.MapOrientation ReadOrientation(this XmlNode node, string tagName, Map.MapOrientation defaultOrientation = Map.MapOrientation.Orthogonal)
        {
            switch (node.ReadTag("orientation"))
            {
                case "isometric":
                    return Map.MapOrientation.Isometric;

                case "orthogonal":
                    return Map.MapOrientation.Orthogonal;

                default:
                    return Map.MapOrientation.Orthogonal;
            }


        }

        public static int ReadInt(this XmlNode node, string tagName, int defaultValue = 0)
        {
            if (node.Attributes != null)
                return node.Attributes[tagName] != null ? int.Parse(node.Attributes[tagName].Value) : defaultValue;
            else
                return defaultValue;
        }

    }
}
