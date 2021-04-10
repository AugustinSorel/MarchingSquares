using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarchingSquares
{
    class MainWindowModel
    {
        private int cols;
        private int rows;
        private OpenSimplexNoise noise;
        private int rez = 30;
        private float[,] field;
        private readonly float increment = 0.1f;
        private float zOff = 0;

        public int Rez
        {
            get { return rez; }
            set { rez = value; }
        }

        public float[,] Field
        {
            get { return field; }
            set { field = value; }
        }

        public MainWindowModel(double actualWidth, double actualHeight)
        {
            cols = 1 + (int)actualWidth / rez;
            rows = 1 + (int)actualHeight / rez;
            field = new float[cols, rows];
            noise = new OpenSimplexNoise();
        }

        internal void SetFields()
        {
            float xOff = 0;
            for (int i = 0; i < cols; i++)
            {
                xOff += increment;
                float yOff = 0;
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = (float)noise.Evaluate(xOff, yOff, zOff);
                    yOff += increment;
                }
            }
            zOff += 0.03f;
        }

        internal int GetState(int i, int j)
        {
            return GetState(Math.Ceiling(field[i, j]), Math.Ceiling(field[i + 1, j]), Math.Ceiling(field[i + 1, j + 1]), Math.Ceiling(field[i, j + 1]));
        }

        private int GetState(double a, double b, double c, double d)
        {
            return (int)(a * 8 + b * 4 + c * 2 + d * 1);
        }
    }
}
