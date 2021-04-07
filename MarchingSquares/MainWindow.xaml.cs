using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MarchingSquares
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] field;
        private Ellipse[,] ellipses;
        private int rez = 20;
        private int cols;
        private int rows;
        //private Line[,] lines;
        List<Line> lines;

        private DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Key Down Event Handler
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
        }
        #endregion

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            cols = 1 + (int)canvas.ActualWidth / rez;
            rows = 1 + (int)canvas.ActualHeight / rez;
            field = new int[cols, rows];

            Random random = new Random();

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    field[i, j] = random.Next(0, 2);
                    //field[i, j] = (float)random.NextDouble();            

            ellipses = new Ellipse[field.GetLength(0), field.GetLength(1)];
            //lines = new Line[field.GetLength(0) + 30, field.GetLength(1) + 30];
            lines = new List<Line>();
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                {
                    ellipses[i, j] = new Ellipse()
                    {
                        Height = rez * 0.4,
                        Width = rez * 0.4,
                        Fill = Brushes.White,
                        Opacity = 1,
                    };
                    canvas.Children.Add(ellipses[i, j]);

                    Line line = new Line()
                    {
                        Stroke = Brushes.White,
                        StrokeThickness = 1,
                        Opacity = 1,
                    };
                    lines.Add(line);
                    canvas.Children.Add(line);
                }

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(HandleDraw);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void HandleDraw(object sender, EventArgs e)
        {
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j] == 0)
                        ellipses[i, j].Fill = Brushes.Black;

                    //ellipses[i, j].Opacity = field[i, j];
                    Canvas.SetLeft(ellipses[i, j], i * rez - ellipses[i, j].ActualWidth / 2); // add half of the ellipse;
                    Canvas.SetTop(ellipses[i, j], j * rez - ellipses[i, j].ActualHeight / 2);
                }

            for (int i = 0; i < cols - 1; i++)
                for (int j = 0; j < rows - 1; j++)
                {
                    float x = i * rez;
                    float y = j * rez;
                    Vector a = new Vector(x + rez * 0.5, y);
                    Vector b = new Vector(x + rez, y + rez * 0.5);
                    Vector c = new Vector(x + rez * 0.5, y + rez);
                    Vector d = new Vector(x, y + rez * 0.5);

                    //lines[i, j].X1 = a.X ;
                    //lines[i, j].Y1 = a.Y ;
                    //lines[i, j].X2 = b.X ;
                    //lines[i, j].Y2 = b.Y ;

                    int state = GetState(field[i, j], field[i + 1, j], field[i + 1, j + 1], field[i, j + 1]);

                    switch (state)
                    {
                        case 1:
                            DrawLine(c, d, i, j);
                            break;
                        case 2:
                            DrawLine(b, c, i, j);
                            break;
                        case 3:
                            DrawLine(b, d, i, j);
                            break;
                        case 4:
                            DrawLine(a, b, i, j);
                            break;
                        case 5:
                            DrawLine(a, d, i, j);
                            DrawLine(b, c, i, j);
                            break;
                        case 6:
                            DrawLine(a, c, i, j);
                            break;
                        case 7:
                            DrawLine(a, d, i, j);
                            break;
                        case 8:
                            DrawLine(a, d, i, j);
                            break;
                        case 9:
                            DrawLine(a, c, i, j);
                            break;
                        case 10:
                            DrawLine(a, b, i, j);
                            DrawLine(c, d, i, j);
                            break;
                        case 11:
                            DrawLine(a, b, i, j);
                            break;
                        case 12:
                            DrawLine(b, d, i, j);
                            break;
                        case 13:
                            DrawLine(b, c, i, j);
                            break;
                        case 14:
                            DrawLine(c, d, i, j);
                            break;
                    }
                }
        }

        private void DrawLine(Vector v1, Vector v2, int x, int y)
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


           //lines[y + (x * 26)].X1 = v1.X;
           //lines[y + (x * 26)].Y1 = v1.Y;
           //lines[y + (x * 26)].X2 = v2.X;
           //lines[y + (x * 26)].Y2 = v2.Y;
           //MessageBox.Show("");
        }

        private int GetState(int a, int b, int c, int d)
        {
            return a * 8 + b * 4 + c * 2 + d * 1;
        }
    }
}