using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MarchingSquares
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float[,] field;
        private int rez = 10;
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
                    field[i, j] = random.Next(0, 1);

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(HandleDraw);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void HandleDraw(object sender, EventArgs e)
        {
        }
    }
}