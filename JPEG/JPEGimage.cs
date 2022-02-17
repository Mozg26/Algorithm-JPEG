using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JPEG
{
    class JPEGimage
    {
        public Bitmap img1;
        public Bitmap img2;
        public int Height;
        public int Width;

        public JPEGimage(Bitmap img)
        {
            img1 = img;
            img2 = null;
            Height = img.Height;
            Width = img.Height;
        }

        public double[,,] CreateMatrixes() //создаём трёхмерный массив пикселей (по цветам)
        {
            int i, j;

            Color pixels;
            double[,,] matrixRGB = new double[Height, Width, 3];

            for (i = 0; i < Height; ++i)
                for (j = 0; j < Width; ++j)
                {
                    pixels = img1.GetPixel(j, i);

                    matrixRGB[i, j, 0] = pixels.R;
                    matrixRGB[i, j, 1] = pixels.G;
                    matrixRGB[i, j, 2] = pixels.B;
                }

            return matrixRGB;
        }

        public double[,,] FromRGBtoYCbCr(double[,,] matrixRGB)
        {
            int i, j;
            double[,,] matrixYCbCr = new double[Height, Width, 3];

            for (i = 0; i < Height; ++i)
                for (j = 0; j < Width; ++j)
                {

                    matrixYCbCr[i, j, 0] = 0.299 * matrixRGB[i, j, 0] + 0.587 * matrixRGB[i, j, 1] + 0.114 * matrixRGB[i, j, 2];
                    matrixYCbCr[i, j, 1] = 128 - 0.1687 * matrixRGB[i, j, 0] - 0.3313 * matrixRGB[i, j, 1] + 0.5 * matrixRGB[i, j, 2];
                    matrixYCbCr[i, j, 2] = 128 + 0.5 * matrixRGB[i, j, 0] - 0.4187 * matrixRGB[i, j, 1] - 0.0813 * matrixRGB[i, j, 2];
                }

            return matrixYCbCr;
        }

        public double[,,] FromYCbCrtoRGB(double[,,] MatrixRGB, double[,,] matrixYCbCr)
        {
            int i, j, N = 8;

            for (i = 0; i < Height; ++i)
                for (j = 0; j < Width; ++j)
                {
                    MatrixRGB[i, j, 0] = matrixYCbCr[i, j, 0] + 1.402 * (matrixYCbCr[i, j, 2] - 128);
                    MatrixRGB[i, j, 1] = matrixYCbCr[i, j, 0] - 0.34414 * (matrixYCbCr[i, j, 1] - 128) - 0.71414 * (matrixYCbCr[i, j, 2] - 128);
                    MatrixRGB[i, j, 2] = matrixYCbCr[i, j, 0] + 1.772 * (matrixYCbCr[i, j, 1] - 128);
                }

            return MatrixRGB;
        }

        public Bitmap CreateBitmap(Bitmap img, double[,,] matrixRGB)
        {
            int i, j;

            img = new Bitmap(Height, Width);

            for (i = 0; i < Height; ++i)
                for (j = 0; j < Width; ++j)
                {
                    if (matrixRGB[i, j, 0] < 0) matrixRGB[i, j, 0] = 0;
                    if (matrixRGB[i, j, 1] < 0) matrixRGB[i, j, 1] = 0;
                    if (matrixRGB[i, j, 2] < 0) matrixRGB[i, j, 2] = 0;

                    img.SetPixel(j, i, Color.FromArgb((byte)matrixRGB[i, j, 0], (byte)matrixRGB[i, j, 1], (byte)matrixRGB[i, j, 2]));
                }

            return img;
        }


    }
}
