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
    class ViewModelMainWindow : INotifyPropertyChanged
    {
        private Ellipse[,] ellipses;
        List<Line> listOfLinesToRemove;
        private readonly Canvas canvas;
        private MainWindowModel mainWindowModel;
        private bool ShowCircle;
        private int speed;

        public int Speed
        {
            get { return speed; }
            set 
            {
                if (value > 50 && value < 1000 && value != speed)
                {
                    speed = value;
                    NotifyPropertyChanged("Speed");
                }
            }
        }

        public MainWindowModel MainWindowModel
        {
            get { return mainWindowModel; }
            set 
            { 
                mainWindowModel = value;
                NotifyPropertyChanged("MainWindowModel");
            }
        }

        public ViewModelMainWindow(Canvas canvas)
        {
            this.canvas = canvas;
            ShowCircle = false;
            Speed = 100;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MainWindowModel = new MainWindowModel(canvas.ActualWidth, canvas.ActualHeight);

            listOfLinesToRemove = new List<Line>();
            ellipses = new Ellipse[mainWindowModel.Field.GetLength(0), mainWindowModel.Field.GetLength(1)];

            CreateCircle2DArray();

            HandleDraw();
        }

        private void CreateCircle2DArray()
        {
            if (ShowCircle)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    for (int i = 0; i < mainWindowModel.Field.GetLength(0); i++)
                        for (int j = 0; j < mainWindowModel.Field.GetLength(1); j++)
                        {
                            ellipses[i, j] = new Ellipse()
                            {
                                Height = mainWindowModel.Rez * 0.4,
                                Width = mainWindowModel.Rez * 0.4,
                                Fill = Brushes.White,
                                Opacity = 0,
                            };
                            Canvas.SetLeft(ellipses[i, j], i * mainWindowModel.Rez - ellipses[i, j].ActualWidth / 2);
                            Canvas.SetTop(ellipses[i, j], j * mainWindowModel.Rez - ellipses[i, j].ActualHeight / 2);
                            canvas.Children.Add(ellipses[i, j]);
                        }
                }));
            }
        }

        private void HandleDraw()
        {
            while (true)
            {
                mainWindowModel.SetFields();
                DrawToTheCanvas();
                GetSleep();
            }
        }

        private void DrawToTheCanvas()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                DrawCircles();
                RemoveOldLines();
                AddNewLines();

            }));
        }

        private void GetSleep()
        {
            Thread.Sleep(speed);
        }

        private void AddNewLines()
        {
            for (int i = 0; i < mainWindowModel.Field.GetLength(0) - 1; i++)
                for (int j = 0; j < mainWindowModel.Field.GetLength(1) - 1; j++)
                {
                    Vector[] vectors = mainWindowModel.GetVectors(i, j);

                    switch (mainWindowModel.GetState(i, j))
                    {
                        case 1:
                            DrawLine(vectors[2], vectors[3]);
                            break;
                        case 2:
                            DrawLine(vectors[1], vectors[2]);
                            break;
                        case 3:
                            DrawLine(vectors[1], vectors[3]);
                            break;
                        case 4:
                            DrawLine(vectors[0], vectors[1]);
                            break;
                        case 5:
                            DrawLine(vectors[0], vectors[3]);
                            DrawLine(vectors[1], vectors[2]);
                            break;
                        case 6:
                            DrawLine(vectors[0], vectors[2]);
                            break;
                        case 7:
                            DrawLine(vectors[0], vectors[3]);
                            break;
                        case 8:
                            DrawLine(vectors[0], vectors[3]);
                            break;
                        case 9:
                            DrawLine(vectors[0], vectors[2]);
                            break;
                        case 10:
                            DrawLine(vectors[0], vectors[1]);
                            DrawLine(vectors[2], vectors[3]);
                            break;
                        case 11:
                            DrawLine(vectors[0], vectors[1]);
                            break;
                        case 12:
                            DrawLine(vectors[1], vectors[3]);
                            break;
                        case 13:
                            DrawLine(vectors[1], vectors[2]);
                            break;
                        case 14:
                            DrawLine(vectors[2], vectors[3]);
                            break;
                    }
                }
        }

        private void RemoveOldLines()
        {
            listOfLinesToRemove.Clear();
            foreach (var item in canvas.Children)
                if (item.GetType() == typeof(Line))
                    listOfLinesToRemove.Add(item as Line);

            foreach (Line item in listOfLinesToRemove)
                canvas.Children.Remove(item);
        }

        private void DrawCircles()
        {
            if (ShowCircle)
            {
                for (int i = 0; i < mainWindowModel.Field.GetLength(0); i++)
                    for (int j = 0; j < mainWindowModel.Field.GetLength(1); j++)
                        ellipses[i, j].Opacity = (mainWindowModel.Field[i, j]);
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

        #region Property Changed Event Handler 
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
