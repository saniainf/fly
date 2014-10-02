using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using System.Drawing;

namespace TiledMax
{
    public class Map
    {
        public Collection<TileSet> TileSets { get; set; }
        public Collection<Layer> Layers { get; set; }
        public Collection<ObjectGroup> ObjectGroups { get; set; }

        public string Version { get; set; }             // The TMX format version, generally 1.0.
        public MapOrientation Orientation { get; set; } // Map orientation. Tiled supports "orthogonal" and "isometric" at the moment.
        public int Width { get; set; }                  // The map width in tiles.
        public int Height { get; set; }                 // The map height in tiles.
        public int TileWidth { get; set; }              // The width of a tile.
        public int TileHeight { get; set; }             // The height of a tile
        public LoadingLog Loaded { get; set; }

        public Map()
        {
            TileSets = new Collection<TileSet>();
            Layers = new Collection<Layer>();
            ObjectGroups = new Collection<ObjectGroup>();
            Version = "1.0";
            Orientation = MapOrientation.Orthogonal;
            Width = 32;
            Height = 32;
            TileWidth = 32;
            TileHeight = 32;
            Loaded = null;
        }

        /// <summary>
        /// Open a TMF map file created with tiled : http://http://www.mapeditor.org/
        /// </summary>
        public static Map Open(Stream stream, string base_path)
        {
            XmlDocument doc = new XmlDocument();
            Map result = new Map();

            //try
            //{
            doc.Load(stream);
            XmlNode xMap = null;

            foreach (XmlNode node in doc.ChildNodes)
            {
                if (node.Name == "map" && node.HasChildNodes) { xMap = node; break; }
            }

            if (xMap == null)
            {
                throw new Exception("Tried to load a file that does not contain map data.");
            }

            result.Version = xMap.ReadTag("version");
            switch (xMap.ReadTag("orientation"))
            {
                case "isometric": result.Orientation = MapOrientation.Isometric; break;
                default: result.Orientation = MapOrientation.Orthogonal; break;
            }
            result.Width = xMap.ReadInt("width");
            result.Height = xMap.ReadInt("height");
            result.TileWidth = xMap.ReadInt("tilewidth");
            result.TileHeight = xMap.ReadInt("tileheight");


            foreach (XmlNode xNode in xMap.ChildNodes)
            {
                switch (xNode.Name)
                {
                    case "tileset": ReadTileset(xNode, ref result, base_path); break;
                    case "layer": ReadLayer(xNode, ref result); break;
                    case "objectgroup": ReadObjectGroup(xNode, ref result); break;
                }
            }

            result.Loaded = new LoadingLog(true, new Exception("Loaded!"));
            //}
            //catch (Exception ex)
            //{
            //    Exception n = new Exception(ex.Message + "\r\n\r\n" + ex.StackTrace);
            //    result.Loaded = new LoadingLog(false, ex);
            //}

            return result;
        }
        /// <summary>
        /// Open a TMF map file created with tiled : http://http://www.mapeditor.org/
        /// </summary>
        public static Map Open(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            return Open(File.OpenRead(filename), fi.DirectoryName);
        }

        static void ReadTileset(XmlNode node, ref Map map, string base_path)
        {
            TileSet r = new TileSet();
            r.FirstGid = node.ReadInt("firstgid");
            r.Source = node.ReadTag("source");
            r.Name = node.ReadTag("name");
            r.TileWidth = node.ReadInt("tilewidth");
            r.TileHeight = node.ReadInt("tileheight");
            r.Spacing = node.ReadInt("spacing");
            r.Margin = node.ReadInt("margin");

            if (node.HasChildNodes)
            {
                foreach (XmlNode img in node.ChildNodes)
                {
                    string c = img.ReadTag("trans", "FFFFFF");
                    if (c.Length == 6 && !c.StartsWith("#")) { c = "#" + c; }
                    Color t = ColorTranslator.FromHtml(c);

                    if (img.Attributes["trans"] != null)
                    {
                        r.Images.Add(new Image() { Source = img.ReadTag("source"), TransColor = t, UseTransColor = true });
                    }
                    else
                    {
                        r.Images.Add(new Image() { Source = img.ReadTag("source"), TransColor = t, UseTransColor = false });
                    }
                }
            }

            r.ReadBitmaps(base_path);

            map.TileSets.Add(r);
        }

        static void ReadLayer(XmlNode node, ref Map map)
        {
            Layer r = new Layer(node.ReadInt("width"), node.ReadInt("height"));
            r.Name = node.ReadTag("name");
            r.X = node.ReadInt("X");
            r.Y = node.ReadInt("Y");
            r.Opacity = node.ReadDouble("opacity", 1);
            r.Visible = node.ReadInt("visible") == 1;

            if (node.HasChildNodes)
            {
                XmlNode data = node.FirstChild;
                string dataVal = data.InnerXml.Trim('\n', ' ');
                string encoding = data.ReadTag("encoding");
                string compression = data.ReadTag("compression");

                byte[] dataToParse = new byte[0];
                byte[] dataDecompressed = new byte[0];

                if (encoding == "base64")
                {
                    dataToParse = Convert.FromBase64String(dataVal);
                }
                else if (encoding == "csv")
                {
                    dataVal = dataVal.Replace(',', ' ').Replace("\n", "");
                    dataToParse = Encoding.Unicode.GetBytes(dataVal);
                }

                if (compression == "gzip")
                {
                    dataDecompressed = GZip.Decompress(dataToParse);
                }
                else if (compression == "zlib")
                {
                    dataDecompressed = GZip.DecompressZlib(dataToParse);
                }
                int[] tempArray = new int[768];
                int i = 0;
                using (BinaryReader br = new BinaryReader(new MemoryStream(dataDecompressed)))
                {
                    for (int y = 0; y < r.Height; y++)
                    {
                        for (int x = 0; x < r.Width; x++)
                        {
                            int v = br.ReadInt32();
                            tempArray[i++] = v;
                            r.Data[x, y] = v;
                        }
                    }
                }

                map.Layers.Add(r);
            }

        }

        static void ReadObjectGroup(XmlNode node, ref Map map)
        {
            ObjectGroup r = new ObjectGroup();
            r.Name = node.ReadTag("name");
            r.X = node.ReadInt("x");
            r.Y = node.ReadInt("y");
            r.Width = node.ReadInt("width");
            r.Height = node.ReadInt("height");

            if (node.HasChildNodes)
            {
                ColorConverter cc = new ColorConverter();
                foreach (XmlNode c in node.ChildNodes)
                {
                    MapObject o = new MapObject();
                    o.Name = c.ReadTag("name");
                    o.Type = c.ReadTag("type");
                    o.X = c.ReadInt("x");
                    o.Y = c.ReadInt("y");
                    o.Width = c.ReadInt("width");
                    o.Height = c.ReadInt("height");
                    o.Gid = c.ReadInt("gid", -1);

                    if (c.HasChildNodes)
                    {
                        foreach (XmlNode d in c.ChildNodes)
                        {
                            switch (d.Name)
                            {
                                case "properties":
                                    foreach (XmlNode e in d.ChildNodes)
                                    {
                                        o.Properties.Add(e.ReadTag("name"), e.ReadTag("value"));
                                    }
                                    break;
                                case "image":
                                    string cp = d.ReadTag("trans", "FFFFFF");
                                    if (cp.Length == 6 && !cp.StartsWith("#")) { cp = "#" + cp; }
                                    Color t = ColorTranslator.FromHtml(cp);
                                    if (d.Attributes["trans"] != null)
                                    {
                                        o.Images.Add(new Image() { Source = d.ReadTag("source"), TransColor = t, UseTransColor = true });
                                    }
                                    else
                                    {
                                        o.Images.Add(new Image() { Source = d.ReadTag("source"), TransColor = t, UseTransColor = false });
                                    }
                                    break;
                            }
                        }
                    }
                    r.Add(o);
                }
            }
            map.ObjectGroups.Add(r);
        }

        /// <summary>
        /// Draws a maximum of 100x100 tiles for a preview of a map.
        /// </summary>
        public Bitmap DrawGdiPreview(bool first_layer_only)
        {
            int w = Width > 100 ? 100 : Width;
            int h = Height > 100 ? 100 : Height;
            TileSet set = TileSets[0];
            Bitmap canvas = new Bitmap(TileWidth * w, TileHeight * h);

            using (Graphics g = Graphics.FromImage(canvas))
            {
                foreach (Layer layer in Layers)
                {
                    for (int y = 0; y < Width; y++)
                    {
                        for (int x = 0; x < Height; x++)
                        {
                            int realX = x * TileWidth;
                            int realY = y * TileHeight;
                            int tile_index = layer.Data[y, x] - 1;
                            if (tile_index >= 0)
                            {
                                g.DrawImage(set.Bitmaps[tile_index], realX - 5, realY);
                            }
                            if (x > w) { break; }
                        }
                        if (y > h) { break; }
                    }
                    if (first_layer_only) { break; }
                }
            }

            return canvas;
        }

    }

    public enum MapOrientation
    {
        Orthogonal,
        Isometric
    }
}
