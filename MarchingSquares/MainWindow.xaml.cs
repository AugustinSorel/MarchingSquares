using System.Windows;
using System.Windows.Input;

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
        }
    }
}