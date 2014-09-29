﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tile_Engine
{
    [Serializable]
    public class MapSquare
    {
        public int[] LayerTiles = new int[3];
        public string CodeValue = "";
        public bool Passable = true;

        public MapSquare(int background, int interactive, int foreground, string code, bool passable)
        {
            LayerTiles[0] = background;
            LayerTiles[1] = interactive;
            LayerTiles[2] = foreground;
            CodeValue = code;
            Passable = passable;
        }

        public void TogglePassable()
        {
            Passable = !Passable;
        }
    }
}
