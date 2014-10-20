using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiledMap
{
    public class Layer
    {
        public string Name;
        public int Width;
        public int Height;
        public bool Visible;
        public int[,] Data;
        public Properties Properties;

        public Layer(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Data = new int[width, height];
            Properties = new Properties();
        }
    }
}
