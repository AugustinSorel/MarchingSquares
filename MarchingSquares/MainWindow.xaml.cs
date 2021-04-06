using System;
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
        private float[,] field;
        private Ellipse[,] ellipses;
        private int rez = 20;
        private int cols;
        private int rows;

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
            cols = (int)canvas.ActualWidth / rez;
            rows = (int)canvas.ActualHeight / rez;
            field = new float[cols, rows];

            Random random = new Random();

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    field[i, j] = (float)random.NextDouble();            

            ellipses = new Ellipse[field.GetLength(0), field.GetLength(1)];
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
                    ellipses[i, j].Opacity = field[i, j];
                    Canvas.SetLeft(ellipses[i, j], i * rez + ellipses[i, j].ActualWidth / 2); // add half of the ellipse;
                    Canvas.SetTop(ellipses[i, j], j * rez + ellipses[i, j].ActualHeight / 2);
                }
        }
    }
}