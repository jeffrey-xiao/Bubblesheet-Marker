using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BubblesheetGrader
{
    class Question : IComparable
    {
        public Point Location;
        public char Answer;
        public Question(Point p, char ans)
        {
            Location = p; Answer = ans;
        }

        public int CompareTo(object obj)
        {
            Question q = (Question)obj;
            if (Math.Abs(Location.X - q.Location.X) < Form1.FilterWidth * Form1.BestSize / 4)
            {
                return Location.Y - q.Location.Y;
            }
            else return Location.X - q.Location.X;
        }
    }
}
