﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TiledMax
{
    public class Tile
    {
        Image Image;
        Properties Properties;

        public Tile()
        {
            Image = new Image();
            Properties = new Properties();
        }
    }
}
