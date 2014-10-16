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

        public List<Rectangle> SourceRectangle;
        public List<int> SourceTileSet;

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

            SourceRectangle = new List<Rectangle>();
            SourceTileSet = new List<int>();
            SourceTileSet.Add(0);
            SourceRectangle.Add(new Rectangle());
        }

        public void AddTileSet(TileSet tileSet)
        {
            int totalTile;
            int tileByX;
            int tileByY;
            TileSets.Add(tileSet);
            tileByX = tileSet.Width / tileSet.TileWidth;
            tileByY = tileSet.Height / tileSet.TileHeight;
            totalTile = tileByX * tileByY;

            for (int i = 0; i < totalTile; i++)
                SourceTileSet.Add(TileSets.Count);

            for (int y = 0; y < tileByY; y++)
            {
                for (int x = 0; x < tileByX; x++)
                {
                    SourceRectangle.Add(
                        new Rectangle(
                            x * tileSet.TileWidth,
                            y * tileSet.TileHeight,
                            tileSet.TileWidth,
                            tileSet.TileHeight));
                }
            }
        }
    }
}
