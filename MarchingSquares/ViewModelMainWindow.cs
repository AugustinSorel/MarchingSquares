using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MarchingSquares
{
    class ViewModelMainWindow
    {
        private float[,] field;
        private Ellipse[,] ellipses;
        private readonly int rez = 20;
        private int cols;
        private int rows;
        private OpenSimplexNoise noise;
        private readonly float increment = 0.1f;
        private readonly Canvas canvas;
        private float zOff = 0;

        public ViewModelMainWindow(Canvas canvas)
        {
            this.canvas = canvas;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            cols = 1 + (int)canvas.ActualWidth / rez;
            rows = 1 + (int)canvas.ActualHeight / rez;
            field = new float[cols, rows];
            noise = new OpenSimplexNoise();

            ellipses = new Ellipse[field.GetLength(0), field.GetLength(1)];

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < cols; i++)
                    for (int j = 0; j < rows; j++)
                    {
                        ellipses[i, j] = new Ellipse()
                        {
                            Height = rez * 0.4,
                            Width = rez * 0.4,
                            Fill = Brushes.White,
                            Opacity = 0,
                        };
                        Canvas.SetLeft(ellipses[i, j], i * rez - ellipses[i, j].ActualWidth / 2);
                        Canvas.SetTop(ellipses[i, j], j * rez - ellipses[i, j].ActualHeight / 2);
                        canvas.Children.Add(ellipses[i, j]);
                    }
            }));
            HandleDraw();
        }

        private void HandleDraw()
        {
            while (true)
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

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (false)
                    {
                        for (int i = 0; i < cols; i++)
                            for (int j = 0; j < rows; j++)
                                ellipses[i, j].Opacity = (field[i, j]);
                    }

                    List<Line> listOfLinesToRemove = new List<Line>();

                    foreach (var item in canvas.Children)
                        if (item.GetType() == typeof(Line))
                            listOfLinesToRemove.Add(item as Line);

                    foreach (Line item in listOfLinesToRemove)
                        canvas.Children.Remove(item);


                    for (int i = 0; i < cols - 1; i++)
                        for (int j = 0; j < rows - 1; j++)
                        {
                            float x = i * rez;
                            float y = j * rez;
                            Vector a = new Vector(x + rez * 0.5, y);
                            Vector b = new Vector(x + rez, y + rez * 0.5);
                            Vector c = new Vector(x + rez * 0.5, y + rez);
                            Vector d = new Vector(x, y + rez * 0.5);

                            int state = GetState(Math.Ceiling(field[i, j]), Math.Ceiling(field[i + 1, j]), Math.Ceiling(field[i + 1, j + 1]), Math.Ceiling(field[i, j + 1]));

                            switch (state)
                            {
                                case 1:
                                    DrawLine(c, d);
                                    break;
                                case 2:
                                    DrawLine(b, c);
                                    break;
                                case 3:
                                    DrawLine(b, d);
                                    break;
                                case 4:
                                    DrawLine(a, b);
                                    break;
                                case 5:
                                    DrawLine(a, d);
                                    DrawLine(b, c);
                                    break;
                                case 6:
                                    DrawLine(a, c);
                                    break;
                                case 7:
                                    DrawLine(a, d);
                                    break;
                                case 8:
                                    DrawLine(a, d);
                                    break;
                                case 9:
                                    DrawLine(a, c);
                                    break;
                                case 10:
                                    DrawLine(a, b);
                                    DrawLine(c, d);
                                    break;
                                case 11:
                                    DrawLine(a, b);
                                    break;
                                case 12:
                                    DrawLine(b, d);
                                    break;
                                case 13:
                                    DrawLine(b, c);
                                    break;
                                case 14:
                                    DrawLine(c, d);
                                    break;
                            }
                        }

                }));

                Thread.Sleep(100);
            }
        }

        private void DrawLine(Vector v1, Vector v2)
        {
            Line line = new Line()
            {

                X1 = v1.X,
                Y1 = v1.Y,
                X2 = v2.X,
                Y2 = v2.Y,

                Stroke = Brushes.White,
                StrokeThickness = 1,
                Opacity = 1,
            };
            canvas.Children.Add(line);
        }

        private int GetState(double a, double b, double c, double d)
        {
            return (int)(a * 8 + b * 4 + c * 2 + d * 1);
        }
    }
}
