using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PictureDownloader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IMGSRC.IMGSRCAlbumParser.PicFound += delegate()
                {
                    label1.Text = IMGSRC.IMGSRCAlbumParser.PicsNum.ToString();
                    Application.DoEvents();
                };
            IMGSRC.IMGSRCAlbumParser.GetPics(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
