using System;
using System.ComponentModel;
using System.Windows;

namespace MarchingSquares
{
    class MainWindowModel : INotifyPropertyChanged
    {
        private int cols;
        private int rows;
        private OpenSimplexNoise noise;
        private int rez;
        private float[,] field;
        private readonly float increment = 0.1f;
        private readonly double actualWidth;
        private readonly double actualHeight;
        private float zOff = 0;

        public int Rez
        {
            get { return rez; }
            set 
            { 
                if (value != rez && value > 9 && value < 51)
                {
                    rez = value;
                    cols = 1 + (int)actualWidth / rez;
                    rows = 1 + (int)actualHeight / rez;
                    field = new float[cols, rows];
                    NotifyPropertyChanged("Rez"); 
                }

            }
        }

        public float[,] Field
        {
            get { return field; }
            set { field = value; }
        }

        private float speed;

        public float Speed
        {
            get { return (float)Math.Round(speed, 3); }
            set 
            {
                if (value != speed && value > -1 && value < 1)
                {
                    speed = value;
                    NotifyPropertyChanged("Speed");
                }
            }
        }


        public MainWindowModel(double actualWidth, double actualHeight)
        {
            Rez = 20;
            Speed = 0.03f;
            cols = 1 + (int)actualWidth / rez;
            rows = 1 + (int)actualHeight / rez;
            field = new float[cols, rows];
            noise = new OpenSimplexNoise();
            this.actualWidth = actualWidth;
            this.actualHeight = actualHeight;
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
            zOff += speed;
        }

        internal int GetState(int i, int j)
        {
            return GetState(Math.Ceiling(field[i, j]), Math.Ceiling(field[i + 1, j]), Math.Ceiling(field[i + 1, j + 1]), Math.Ceiling(field[i, j + 1]));
        }

        private int GetState(double a, double b, double c, double d)
        {
            return (int)(a * 8 + b * 4 + c * 2 + d * 1);
        }

        internal Vector[] GetVectors(int i, int j)
        {
            float x = i * rez;
            float y = j * rez;

            return new[] { new Vector(x + rez * 0.5, y), new Vector(x + rez, y + rez * 0.5), new Vector(x + rez * 0.5, y + rez), new Vector(x, y + rez * 0.5)};
        }

        #region Property Changed Event Handler 
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
