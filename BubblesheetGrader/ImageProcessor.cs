using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubblesheetGrader
{
    class ImageProcessor
    {
        public static float[,] ResizeImage(float[,] img)
        {
            if (img.GetLength(0) * img.GetLength(1) <= 5e5)
                return img;
            float[,] resized = new float[img.GetLength(0) / 2, img.GetLength(1) / 2];
            for (int i = 0; i < img.GetLength(0) / 2; ++i)
            {
                for (int j = 0; j < img.GetLength(1) / 2; ++j)
                {
                    resized[i, j] = (img[i * 2, j * 2] + img[i * 2 + 1, j * 2] + img[i * 2, j * 2 + 1] + img[i * 2 + 1, j * 2 + 1]) / 4;
                }
            }
            return ResizeImage(resized);
        }
    }
}
