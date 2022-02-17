using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPEG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap img = (Bitmap)pictureBox1.Image;
            JPEGimage obj = new JPEGimage(img);

            double[,,] matrixRGB;
            matrixRGB = obj.CreateMatrixes();

            double[,,] matrixYCbCr;
            matrixYCbCr = obj.FromRGBtoYCbCr(matrixRGB);

            double[,,] NewMatrixRGB = new double[obj.Height, img.Width, 3];

            NewMatrixRGB = obj.FromYCbCrtoRGB(NewMatrixRGB, matrixYCbCr);

            obj.img2 = obj.CreateBitmap(obj.img2, NewMatrixRGB);
            pictureBox2.Image = obj.img2;
    }
    }
}
