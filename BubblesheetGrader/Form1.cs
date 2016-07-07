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
        private const string FILTER_LOCATION = "C:/User/Josh/Desktop/Projects/BubblesheetGrader/filter.txt";

        private Bitmap _bubbleSheet;
        private float[,] _image;
        private float[,] _filter;
        private int _imgWidth, _imgHeight;
        private int _filterWidth, _filterHeight;
        private bool _fileLoaded = false;
        private bool _filterLoaded = false;

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
            _imgWidth = _image.GetLength(0); _imgHeight = _image.GetLength(1);

            _fileLoaded = true;
            Refresh();
        }

        private void btnLoadFilter_Click(object sender, EventArgs e)
        {
            string fileLocation = txtFilterLocation.Text;

            using (StreamReader reader = new StreamReader(File.Open(fileLocation, FileMode.Open)))
            {
                _filterWidth = int.Parse(reader.ReadLine());
                _filterHeight = int.Parse(reader.ReadLine());
                _filter = new float[_filterWidth, _filterHeight];
                
                for (int j = 0; j < _filterHeight; ++j)
                {
                    string[] s = reader.ReadLine().Split();
                    for (int i = 0; i < _filterWidth; ++i)
                    {
                        _filter[i, j] = (float)int.Parse(s[i]) / 255;
                    }
                }
            }

            _filterLoaded = true;
        }

        private void btnRunFilter_Click(object sender, EventArgs e)
        {
            for (float f = 1; f > 0.1F; f -= 0.05F)
            {
                float[,] filter = ResizeFilter(f);
                int width = filter.GetLength(0), height = filter.GetLength(1);
                for (int i = 0; i < _imgWidth - width; i += 5)
                {
                    for (int j = 0; j < _imgHeight - height; j += 5)
                    {

                    }
                }
            }
        }

        private float GetFilterValue()
        {
            return 0;
        }

        private float[,] ResizeFilter(float factor)
        {
            float[,] resized = new float[(int)Math.Ceiling(factor * _filterWidth), (int)Math.Ceiling(factor * _filterHeight)];
            float[,] weightSum = new float[resized.GetLength(0), resized.GetLength(1)];
            for (int i = 0; i < _filterWidth; ++i)
            {
                for (int j = 0; j < _filterWidth; ++j)
                {
                    int x1 = (int)(factor * i), y1 = (int)(factor * j);
                    int x2 = (int)(factor * i + 0.999F), y2 = (int)(factor * j + 0.999F);
                    double dx, dy, d;

                    dx = 1.0 - Math.Abs(factor * i - x1);
                    dy = 1.0 - Math.Abs(factor * j - y1);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x1, y1] += (float)(_filter[i, j] * d);
                    weightSum[x1, y1] += (float)d;

                    dx = 1.0 - Math.Abs(factor * i - x2);
                    dy = 1.0 - Math.Abs(factor * j - y1);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x2, y1] += (float)(_filter[i, j] * d);
                    weightSum[x2, y1] += (float)d;

                    dx = 1.0 - Math.Abs(factor * i - x1);
                    dy = 1.0 - Math.Abs(factor * j - y2);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x1, y2] += (float)(_filter[i, j] * d);
                    weightSum[x1, y2] += (float)d;

                    dx = 1.0 - Math.Abs(factor * i - x2);
                    dy = 1.0 - Math.Abs(factor * j - y2);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x2, y2] += (float)(_filter[i, j] * d);
                    weightSum[x2, y2] += (float)d;
                }
            }

            for (int i = 0; i < resized.GetLength(0); ++i)
            {
                for (int j = 0; j < resized.GetLength(1); ++j)
                {
                    resized[i, j] /= weightSum[i, j];
                }
            }
            return resized;
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
            if (_filterLoaded)
            {
                float[,] filter = ResizeFilter(0.7F);
                for (int i = 0; i < filter.GetLength(0); ++i)
                {
                    for (int j = 0; j < filter.GetLength(1); ++j)
                    {
                        Pen p = new Pen(Color.FromArgb((int)(filter[i, j] * 255), (int)(filter[i, j] * 255), (int)(filter[i, j] * 255)));
                        e.Graphics.DrawRectangle(p, i, j, 1, 1);
                    }
                }
            }
        }
    }
}
