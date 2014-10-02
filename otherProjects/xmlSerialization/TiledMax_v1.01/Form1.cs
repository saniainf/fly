using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TiledMax;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "TMX Map's (*.tmx)|*.tmx";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Map map = Map.Open(dlg.FileName);

                if (map.Loaded.Successful)
                {
                    pictureBox1.Image = map.DrawGdiPreview(checkBox1.Checked);
                }
                else
                {
                    Exception ex = map.Loaded.Error;
                    MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace);
                }
            }
        }

    }
}
