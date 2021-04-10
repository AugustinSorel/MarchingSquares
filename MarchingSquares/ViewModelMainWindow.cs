﻿using System;
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
        private Ellipse[,] ellipses;
        List<Line> listOfLinesToRemove;
        private readonly Canvas canvas;
        private MainWindowModel mainWindowModel;
        public ViewModelMainWindow(Canvas canvas)
        {
            this.canvas = canvas;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            mainWindowModel = new MainWindowModel(canvas.ActualWidth, canvas.ActualHeight);

            listOfLinesToRemove = new List<Line>();
            ellipses = new Ellipse[mainWindowModel.Field.GetLength(0), mainWindowModel.Field.GetLength(1)];

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < ellipses.GetLength(0); i++)
                    for (int j = 0; j < ellipses.GetLength(1); j++)
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
            HandleDraw();
        }

        private void HandleDraw()
        {
            while (true)
            {
                mainWindowModel.SetFields();

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (true)
                    {
                        for (int i = 0; i < mainWindowModel.Field.GetLength(0); i++)
                            for (int j = 0; j < mainWindowModel.Field.GetLength(1); j++)
                                ellipses[i, j].Opacity = (mainWindowModel.Field[i, j]);
                    }


                    listOfLinesToRemove.Clear();
                    foreach (var item in canvas.Children)
                        if (item.GetType() == typeof(Line))
                            listOfLinesToRemove.Add(item as Line);

                    foreach (Line item in listOfLinesToRemove)
                        canvas.Children.Remove(item);


                    for (int i = 0; i < mainWindowModel.Field.GetLength(0) - 1; i++)
                        for (int j = 0; j < mainWindowModel.Field.GetLength(1) - 1; j++)
                        {
                            float x = i * mainWindowModel.Rez;
                            float y = j * mainWindowModel.Rez;
                            Vector a = new Vector(x + mainWindowModel.Rez * 0.5, y);
                            Vector b = new Vector(x + mainWindowModel.Rez, y + mainWindowModel.Rez * 0.5);
                            Vector c = new Vector(x + mainWindowModel.Rez * 0.5, y + mainWindowModel.Rez);
                            Vector d = new Vector(x, y + mainWindowModel.Rez * 0.5);

                            switch (mainWindowModel.GetState(i, j))
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
    }
}