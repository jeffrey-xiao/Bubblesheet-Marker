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

        private List<Point> _questionBoxes = new List<Point>();

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
        }

        private void btnLoadFilter_Click(object sender, EventArgs e)
        {
            string fileLocation = txtFilterLocation.Text;

            using (StreamReader reader = new StreamReader(File.Open(fileLocation, FileMode.Open)))
            {
                _filterWidth = int.Parse(reader.ReadLine());
                _filterHeight = int.Parse(reader.ReadLine());
                _filter = new float[_filterWidth, _filterHeight];
                
                for (int i = 0; i < _filterWidth; ++i)
                {
                    string[] s = reader.ReadLine().Split();
                    for (int j = 0; j < _filterHeight; ++j)
                    {
                        _filter[i, j] = (float)int.Parse(s[j]) / 255 * 2 - 1;
                    }
                }
            }

            _filterLoaded = true;
        }

        private float _bestSize = 0;
        int bestX = 0, bestY = 0;
        private float _bestValue = -99999;
        bool filterDone = false;

        private void btnRunFilter_Click(object sender, EventArgs e)
        {
            for (float f = 0.5F; f > 0.05F; f -= 0.025F)
            {
                float[,] filter = ResizeFilter(f);
                int width = filter.GetLength(0), height = filter.GetLength(1);
                for (int i = 0; i < _imgWidth - width; i += 5)
                {
                    for (int j = 0; j < _imgHeight - height; j += 5)
                    {
                        float val = GetFilterValue(filter, i, j);
                        if (val>_bestValue)
                        {
                            _bestValue = val;
                            _bestSize = f; bestX = i; bestY = j;
                        }
                    }
                }
                //Refresh();
                this.Text = f.ToString();
            }
            FindQuestionboxes();
        }

        private void FindQuestionboxes()
        {
            float[,] filter = ResizeFilter(_bestSize);
            float cutoff = _bestValue * 0.99F;
            int nextI = 0;
            for (int i = 0; i < _imgWidth-filter.GetLength(0); i=nextI)
            {
                nextI = i + 1;
                for (int j = 0; j < _imgHeight-filter.GetLength(1); ++j)
                {
                    float val = GetFilterValue(filter, i, j);
                    if (val>cutoff)
                    {
                        _questionBoxes.Add(new Point(i, j));
                        j += filter.GetLength(1);
                        nextI = i + filter.GetLength(0);
                        this.Text = "Found " + _questionBoxes.Count + " questionboxes";
                    }
                }
            }
            filterDone = true;
            Refresh();
        }

        private float GetFilterValue(float[,] filter, int x, int y)
        {
            float value = 0;
            for (int i = 0; i < filter.GetLength(0); ++i)
            {
                for (int j = 0; j < filter.GetLength(1); ++j)
                {
                    value += _image[x + i, y + j] * filter[i, j];
                }
            }
            return value * 1000 / filter.GetLength(0) / filter.GetLength(1);
        }

        private float[,] ResizeFilter(float factor)
        {
            float[,] resized = new float[(int)(factor * _filterWidth+2), (int)(factor * _filterHeight+2)];
            float[,] weightSum = new float[resized.GetLength(0), resized.GetLength(1)];
            for (int i = 0; i < _filterWidth; ++i)
            {
                for (int j = 0; j < _filterHeight; ++j)
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
                    if (weightSum[i, j] > 0)
                        resized[i, j] /= weightSum[i, j];
                    else resized[i, j] = 1;
                }
            }
            return resized;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_fileLoaded)
            {
                for (int i = 0; i < _imgWidth; i+=1)
                {
                    for (int j = 0; j < _imgHeight; j += 1)
                    {
                        Pen p = new Pen(Color.FromArgb((int)(_image[i, j] * 255), (int)(_image[i, j] * 255), (int)(_image[i, j] * 255)));
                        e.Graphics.DrawRectangle(p, i, j, 1, 1);
                    }
                }
            }
            if (filterDone)
            {
                for (int i = 0; i < _questionBoxes.Count; ++i)
                {
                    e.Graphics.DrawRectangle(Pens.Red, _questionBoxes[i].X, _questionBoxes[i].Y, _bestSize * _filterWidth, _bestSize * _filterHeight);
                }
            }
            //if (_filterLoaded)
            //{
            //    _fileLoaded = false;
            //    float[,] filter = ResizeFilter(0.2F);
            //    for (int i = 0; i < filter.GetLength(0); ++i)
            //    {
            //        for (int j = 0; j < filter.GetLength(1); ++j)
            //        {
            //            Pen p = new Pen(Color.FromArgb((int)(filter[i, j] * 255), (int)(filter[i, j] * 255), (int)(filter[i, j] * 255)));
            //            e.Graphics.DrawRectangle(p, i*5, j*5, 5, 5);
            //        }
            //    }
            //}
        }
    }
}
