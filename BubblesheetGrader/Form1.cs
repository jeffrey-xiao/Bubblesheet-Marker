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

        private Bitmap _bubbleSheet;
        private float[,] _image;
        private float[,] _filter;
        private int _imgWidth, _imgHeight;
        public static int FilterWidth, FilterHeight;
        private bool _fileLoaded = false;
        private bool _filterLoaded = false;

        private float[,] _blurFilter = new float[,]
        {
            {1,2,1 },
            {2,4,2 },
            {1,2,1 }
        };

        private List<Point> _questionBoxes = new List<Point>();
        private List<float> _boxValues = new List<float>();
        private char[] _answers;
        private Question[] _questions;

        public static float BestSize = 0;
        int bestX = 0, bestY = 0;
        private float _bestValue = -99999;
        bool filterDone = false;
        bool drawBest = false;

        bool answersFound = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadImage()
        {
            this.Text = "Loading Image...";
            _questionBoxes.Clear();
            _boxValues.Clear();
            filterDone = false;
            drawBest = false;
            _bestValue = -99999f;
            answersFound = false;


            string fileLocation = txtBubblesheetLocation.Text;

            if (!File.Exists(fileLocation))
            {
                MessageBox.Show("File not found!");
                return;
            }

            _bubbleSheet = new Bitmap(fileLocation);

            float f = (float)2e5 / _bubbleSheet.Width / _bubbleSheet.Height;
            f = (float)Math.Sqrt(f);

            _bubbleSheet = new Bitmap(_bubbleSheet, new Size((int)(f * _bubbleSheet.Width), (int)(f * _bubbleSheet.Height)));

            _imgWidth = _bubbleSheet.Width; _imgHeight = _bubbleSheet.Height;
            _image = new float[_imgWidth, _imgHeight];
            for (int i = 0; i < _imgWidth; ++i)
            {
                for (int j = 0; j < _imgHeight; ++j)
                {
                    Color c = _bubbleSheet.GetPixel(i, j);
                    _image[i, j] = (float)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B) / 127 - 1;
                }
            }

            float[,] blurred = new float[_imgWidth - 2, _imgHeight - 2];

            for (int i = 0; i < _imgWidth - 2; ++i)
            {
                for (int j = 0; j < _imgHeight - 2; ++j)
                {
                    for (int p = 0; p < 3; ++p)
                    {
                        for (int q = 0; q < 3; ++q)
                        {
                            blurred[i, j] += _image[i + p, j + q] * _blurFilter[p, q];
                        }
                    }
                    blurred[i, j] /= 16;
                }
            }

            _imgWidth -= 2; _imgHeight -= 2;
            _image = blurred;
            float[,] presum = new float[_imgWidth, _imgHeight];

            for (int i = 0; i < _imgWidth; ++i)
            {
                for (int j = 0; j < _imgHeight; ++j)
                {
                    presum[i, j] = _image[i, j];
                    if (i > 0)
                        presum[i, j] += presum[i - 1, j];
                    if (j > 0)
                        presum[i, j] += presum[i, j - 1];
                    if (i > 0 && j > 0)
                        presum[i, j] -= presum[i - 1, j - 1];
                }
            }
            int rWidth = _imgWidth / 24, rHeight = _imgHeight / 24;

            float[,] newImage = new float[_imgWidth, _imgHeight];

            for (int i = 0; i < _imgWidth; ++i)
            {
                for (int j = 0; j < _imgHeight; ++j)
                {
                    float total = presum[Math.Min(i + rWidth - 1, _imgWidth - 1), Math.Min(j + rHeight - 1, _imgHeight - 1)];
                    if (i - rWidth >= 0)
                        total -= presum[i - rWidth, Math.Min(j + rHeight - 1, _imgHeight - 1)];
                    if (j - rHeight >= 0)
                        total -= presum[Math.Min(i + rWidth - 1, _imgWidth - 1), j - rHeight];
                    if (i - rWidth >= 0 && j - rHeight >= 0)
                        total += presum[i - rWidth, j - rHeight];

                    float cnt = (Math.Min(i + rWidth - 1, _imgWidth) - Math.Max(-1, i - rWidth)) *
                              (Math.Min(j + rHeight - 1, _imgHeight) - Math.Max(-1, j - rHeight));
                    
                    //for (int p = Math.Max(0, i-rWidth+1); p < Math.Min(_imgWidth, i+rWidth); ++p)
                    //{
                    //    for (int q = Math.Max(0, j-rHeight+1); q < Math.Min(_imgHeight, j+rHeight); ++q)
                    //    {
                    //        total += _image[p, q]; ++cnt;
                    //    }
                    //}

                    float avg = total / cnt;
                    newImage[i, j] = (float)Math.Tanh((_image[i, j] - avg+0.05)*40);
                }
            }

            _image = newImage;

            //for (int i = 0; i < _imgWidth; ++i)
            //{
            //    for (int j = 0; j < _imgHeight; ++j)
            //    {
            //        _image[i, j] = Contrast(_image[i, j] * 2 - 1);
            //        if (_image[i, j] > 0.4F) _image[i, j] = 1;
            //    }
            //}

            _fileLoaded = true;
            this.Text = "Image Loaded";
        }

        private void LoadFilter()
        {
            string fileLocation = txtFilterLocation.Text;

            using (StreamReader reader = new StreamReader(File.Open(fileLocation, FileMode.Open)))
            {
                FilterWidth = int.Parse(reader.ReadLine());
                FilterHeight = int.Parse(reader.ReadLine());
                _filter = new float[FilterWidth, FilterHeight];
                
                for (int i = 0; i < FilterWidth; ++i)
                {
                    string[] s = reader.ReadLine().Split();
                    for (int j = 0; j < FilterHeight; ++j)
                    {
                        _filter[i, j] = (float)int.Parse(s[j]) / 255 * 2 - 1.8F;
                    }
                }
            }

            _filterLoaded = true;
            //Refresh();
        }

        float Contrast(float x)
        {
            return (float)Math.Sin(x * Math.PI / 2);
        }

        private void RunFilter()
        {
            drawBest = true;
            for (float f = 0.17F; f > 0.13F; f *= 0.99F)
            {
                float[,] filter = ResizeImage(f,_filter);
                int width = filter.GetLength(0), height = filter.GetLength(1);
                for (int i = 0; i < _imgWidth - width; i += 12)
                {
                    for (int j = 0; j < _imgHeight - height; j += 12)
                    {
                        float val = GetFilterValue(filter, i, j);
                        if (val>_bestValue)
                        {
                            _bestValue = val;
                            BestSize = f;
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
            float[,] filter = ResizeImage(BestSize, _filter);
            float[,] blurred = new float[filter.GetLength(0), filter.GetLength(1)];
            for (int i = 1; i < filter.GetLength(0)-1; i++)
            {
                for (int j = 1; j < filter.GetLength(1)-1; j++)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            blurred[i, j] += filter[i + p - 1, j + q - 1];
                        }
                    }
                    blurred[i, j] /= 9;
                }
            }
            float cutoff = 620, cutoff2 = 460;
            int nextI = _imgWidth-filter.GetLength(0)-1;
            for (int p = 3; p< _imgWidth - filter.GetLength(0)-4; p+=6)
            {
                for (int q = 3; q < _imgHeight-filter.GetLength(1)-4; q+=6)
                {
                    float val = GetFilterValue(blurred, p,q);
                    if (val>cutoff2)
                    {
                        for (int i = p-3; i < p+3; i++)
                        {
                            for (int j = q-3; j < q+3; j++)
                            {
                                val = GetFilterValue(filter, i,j);
                                if (val > cutoff)
                                {
                                    bool b = true;
                                    for (int k = 0; k < _questionBoxes.Count; ++k)
                                    {
                                        bool xIntersect = Math.Max(_questionBoxes[k].X, i) < Math.Min(_questionBoxes[k].X + filter.GetLength(0), i + filter.GetLength(0));
                                        bool yIntersect = Math.Max(_questionBoxes[k].Y, j) < Math.Min(_questionBoxes[k].Y + filter.GetLength(1), j + filter.GetLength(1));
                                        if (xIntersect && yIntersect)
                                        {
                                            if (val > _boxValues[k])
                                            {
                                                _questionBoxes[k] = new Point(i, j);
                                                _boxValues[k] = val;
                                            }
                                            b = false; break;
                                        }
                                    }
                                    if (b)
                                    {
                                        _boxValues.Add(val);
                                        _questionBoxes.Add(new Point(i, j));
                                    }
                                    this.Text = "Found " + _questionBoxes.Count + " questionboxes";
                                }
                            }
                        }
                    }
                }
            }
            filterDone = true;
            FindAnswers();
            //Refresh();
        }

        private void FindAnswers()
        {
            int width = (int)(BestSize * FilterWidth), height = (int)(BestSize * FilterHeight);
            _answers = new char[_questionBoxes.Count];
            _questions = new Question[_questionBoxes.Count];
            for (int i = 0; i < _questionBoxes.Count; ++i)
            {
                float bestAvg = 99999999, bestIdx = 0;
                for (int j = 0; j < 4; ++j)
                {
                    int x = _questionBoxes[i].X + j * width / 4;
                    int y = _questionBoxes[i].Y;
                    int cx = x + width / 8;
                    int cy = y + height / 2;
                    float r = height * 0.4F;
                    r *= r;
                    float sum = 0;
                    int cnt = 0;
                    
                    for (int p = 0; p < width/4; ++p)
                    {
                        for (int q = 0; q < height; ++q)
                        {
                            if ((x + p - cx) * (x + p - cx) + (y + q - cy) * (y + q - cy) < r)
                            {
                                sum += _image[x + p, y + q];
                                cnt++;
                            }
                        }
                    }
                    sum /= cnt;
                    if (sum<bestAvg)
                    {
                        bestIdx = j;
                        bestAvg = sum;
                    }
                }
                if (bestAvg > -0.6F)
                    _answers[i] = 'N';
                else if (bestIdx == 0)
                    _answers[i] = 'A';
                else if (bestIdx == 1)
                    _answers[i] = 'B';
                else if (bestIdx == 2)
                    _answers[i] = 'C';
                else if (bestIdx == 3)
                    _answers[i] = 'D';

                _questions[i] = new Question(_questionBoxes[i], _answers[i]);
            }
            answersFound = true;
        }

        List<char> answers;

        private void Run()
        {
            LoadImage();
            LoadFilter();
            RunFilter();

            Array.Sort(_questions);
        }

        private void btnRunAndMark_Click(object sender, EventArgs e)
        {
            LoadAnswers();
            Run();
            int corr=0, total=0;

            for (int i = 0; i < _questions.Length; ++i)
            {
                ++total;
                if (_questions[i].Answer == answers[i])
                    ++corr;
            }
            if (total>0)
                this.Text = corr + " out of " + total + " answers correct. " + Math.Round((float)(corr * 100 / total)) + "%";

            Refresh();
            this.Text = BestSize.ToString();
        }

        private void LoadAnswers()
        {
            answers = new List<char>();
            using (StreamReader reader = new StreamReader(File.Open(txtAnswers.Text, FileMode.Open)))
            {
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    if (s == "") break;
                    answers.Add(char.Parse(s));
                }
            }
        }

        private void SaveAnswers()
        {
            using (StreamWriter writer = new StreamWriter(txtAnswers.Text))
            {
                for (int i = 0; i < _questions.Length; i++)
                {
                    writer.WriteLine(_questions[i].Answer);
                }
                for (int i = _questions.Length; i < 100; i++)
                {
                    writer.WriteLine('N');
                }
            }
        }

        private void btnRunAndSave_Click(object sender, EventArgs e)
        {
            Run();
            SaveAnswers();
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
            return value * 1000 / filter.GetLength(0) / filter.GetLength(1)+500;
        }

        private float[,] ResizeImage(float factor, float[,] image)
        {
            float[,] resized = new float[(int)(factor * image.GetLength(0) + 2), (int)(factor * image.GetLength(1) + 2)];
            float[,] weightSum = new float[resized.GetLength(0), resized.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); ++i)
            {
                for (int j = 0; j < image.GetLength(1); ++j)
                {
                    int x1 = (int)(factor * i), y1 = (int)(factor * j);
                    int x2 = (int)(factor * i + 0.999F), y2 = (int)(factor * j + 0.999F);
                    double dx, dy, d;

                    dx = 1.0 - Math.Abs(factor * i - x1);
                    dy = 1.0 - Math.Abs(factor * j - y1);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x1, y1] += (float)(image[i, j] * d);
                    weightSum[x1, y1] += (float)d;

                    dx = 1.0 - Math.Abs(factor * i - x2);
                    dy = 1.0 - Math.Abs(factor * j - y1);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x2, y1] += (float)(image[i, j] * d);
                    weightSum[x2, y1] += (float)d;

                    dx = 1.0 - Math.Abs(factor * i - x1);
                    dy = 1.0 - Math.Abs(factor * j - y2);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x1, y2] += (float)(image[i, j] * d);
                    weightSum[x1, y2] += (float)d;

                    dx = 1.0 - Math.Abs(factor * i - x2);
                    dy = 1.0 - Math.Abs(factor * j - y2);
                    d = (float)Math.Sqrt(dx * dx + dy * dy);
                    resized[x2, y2] += (float)(image[i, j] * d);
                    weightSum[x2, y2] += (float)d;
                }
            }

            for (int i = 0; i < resized.GetLength(0); ++i)
            {
                for (int j = 0; j < resized.GetLength(1); ++j)
                {
                    if (weightSum[i, j] > 0)
                        resized[i, j] /= weightSum[i, j];
                    else resized[i, j] = 0;
                    
                }
            }
            return resized;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_fileLoaded)
            {
                float f = (float)2e5 / _bubbleSheet.Width / _bubbleSheet.Height;
                f = (float)Math.Sqrt(f);
                e.Graphics.DrawImage(_bubbleSheet, 0,0);
                //for (int i = 0; i < _imgWidth; i++)
                //{
                //    for (int j = 0; j < _imgHeight; j++)
                //    {
                //        Brush p = new SolidBrush(Color.FromArgb((int)((_image[i, j] + 1) * 127), (int)((_image[i, j] + 1) * 127), (int)((_image[i, j] + 1) * 127)));
                //        e.Graphics.FillRectangle(p, i / 2 + f * _bubbleSheet.Width, j / 2, 1, 1);
                //    }
                //}
            }
            if (filterDone)
            {
                e.Graphics.DrawString("Student Answers:", new Font("Courier", 10), Brushes.Black, 10, 430);
                e.Graphics.DrawString("Correct Answers:", new Font("Courier", 10), Brushes.Black, 10, 460);
                for (int i = 0; i < _questionBoxes.Count; ++i)
                {
                    if (answers != null && answers[i] == _questions[i].Answer)
                    {
                        Brush b = new SolidBrush(Color.FromArgb(100, 0, 255, 0));
                        e.Graphics.FillRectangle(b, _questions[i].Location.X, _questions[i].Location.Y, BestSize * FilterWidth, BestSize * FilterHeight);
                    }
                    else
                    {
                        Brush b = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
                        if (answers != null)
                            e.Graphics.DrawString(answers[i].ToString(), new Font("Courier", 8, FontStyle.Bold), Brushes.Green, _questions[i].Location.X + BestSize * FilterWidth + 1, _questions[i].Location.Y);
                        e.Graphics.FillRectangle(b, _questions[i].Location.X, _questions[i].Location.Y, BestSize * FilterWidth, BestSize * FilterHeight);
                    }
                    e.Graphics.DrawString((i+1).ToString(), new Font("Courier", 8), Brushes.Black, i * 15 + 120, 400);
                    e.Graphics.DrawString(_questions[i].Answer.ToString(), new Font("Courier", 9), Brushes.Black, i * 15 + 120, 430);
                    if (answers != null)
                        e.Graphics.DrawString(answers[i].ToString(), new Font("Courier", 9), Brushes.Black, i * 15 + 120, 460);
                }
            }
            //if (drawBest)
            //{
            //    e.Graphics.DrawRectangle(Pens.Red, bestX, bestY, BestSize * FilterWidth, BestSize * FilterHeight);
            //}
            //if (_filterLoaded)
            //{
            //    _fileLoaded = false;
            //    float[,] filter = ResizeFilter(BestSize);
            //    for (int i = 0; i < filter.GetLength(0); ++i)
            //    {
            //        for (int j = 0; j < filter.GetLength(1); ++j)
            //        {
            //            Pen p = new Pen(Color.FromArgb((int)((filter[i, j] + 1) * 127), (int)((filter[i, j] + 1) * 127), (int)((filter[i, j] + 1) * 127)));
            //            e.Graphics.DrawRectangle(p, bestX+ BestSize * FilterWidth+i * 1, bestY+j * 1, 1, 1);
            //        }
            //    }
            //}
        }
    }
}
