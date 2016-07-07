using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BubblesheetGrader
{
    public partial class Form1 : Form
    {
        private const string FILTER_LOCATION = "C:\Users\Josh\Desktop\Projects\BubblesheetGrader\filter.txt";

        private Bitmap _bubbleSheet;
        private float[,] _image;
        private int _imgWidth, _imgHeight;
        private bool _fileLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            string fileLocation = txtBubblesheetLocation.Text;

            if (!File.Exists(fileLocation))
            {
                MessageBox.Show("File not found!");
                return;
            }

            _bubbleSheet = new Bitmap(fileLocation);

            _imgWidth = _bubbleSheet.Width; _imgHeight = _bubbleSheet.Height;
            _image = new float[_imgWidth, _imgHeight];
            for (int i = 0; i < _imgWidth; ++i)
            {
                for (int j = 0; j < _imgHeight; ++j)
                {
                    _image[i, j] = _bubbleSheet.GetPixel(i, j).GetBrightness();
                }
            }

            //resize to image less than 2megapixels
            _image = ImageProcessor.ResizeImage(_image);

            _fileLoaded = true;
            Refresh();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_fileLoaded)
            {
                for (int i = 0; i < _imgWidth; ++i)
                {
                    for (int j = 0; j < _imgHeight; ++j)
                    {
                        Pen p = new Pen(Color.FromArgb((int)(_image[i, j] * 255), (int)(_image[i, j] * 255), (int)(_image[i, j] * 255)));
                        e.Graphics.DrawRectangle(p, i, j, 1, 1);
                    }
                }
                
            }
        }
    }
}
