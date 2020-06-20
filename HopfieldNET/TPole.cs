using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace HopfieldNET
{
    class TPole
    {
        readonly Canvas g; // холст для поля
        public int NN; // количество клеток N*N
        readonly double dx; // ширина в пикселях клетки
        readonly string name; 

        readonly Rectangle[,] Rs; // клетки

        public TBox Box;

        public TPole(Canvas g, string name)
        {
            this.g = g;
            this.name = name;
            NN = 5;
            dx = g.Width / NN; // ширина в пикселях клетки

            Box = new TBox(NN, name);

            Rs = new Rectangle[NN, NN];

            Draw(); // перерисовать поле

            g.MouseUp += Check;
        }

        private void Check(object sender, MouseButtonEventArgs e)
        {
            IsCheck(e.GetPosition(g).X, e.GetPosition(g).Y);
        }

        // отметить клетку с заданными координатами
        public void IsCheck(double x, double y)
        {
            // проверить диапазон
            if((x < 0)||(x > g.Width)) { return; }
            if ((y < 0) || (y > g.Height)) { return; }

            // расчитать номер клетки
            int i = Convert.ToInt16(Math.Floor(x / dx));
            int j = Convert.ToInt16(Math.Floor(y / dx));

            if (Rs[i, j] == null)
            {
                DrawCell(i, j);

                Box[i, j] = 1;
            }
            else
            {
                g.Children.Remove(Rs[i, j]);
                Rs[i, j] = null;

                Box[i, j] = -1;
            }
        }

        // рисовать клетку
        public void DrawCell(int i, int j)
        {
            Rs[i, j] = new Rectangle
            {
                Fill = Brushes.Green,
                Margin = new Thickness(i * dx, j * dx, 0, 0),
                Height = dx,
                Width = dx
            };
            g.Children.Add(Rs[i, j]);
        }

        // перерисовать поле
        public string Draw()
        {
            g.Children.Clear();
            g.Background = Brushes.DarkGray; // фон

            for (int k = 0; k <= NN; k++)
            {
                // рисуем горизонтальные линии
                Line l = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 0,
                    X2 = g.Width,
                    Y1 = k * dx,
                    Y2 = k * dx,
                    StrokeThickness = 1
                };
                g.Children.Add(l);

                // рисуем вертикальные линии
                l = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = k * dx,
                    X2 = k * dx,
                    Y1 = 0,
                    Y2 = g.Height,
                    StrokeThickness = 1
                };
                g.Children.Add(l);
            }

            for (int i = 0; i < NN; i++)
            {
                for (int j = 0; j < NN; j++)
                {
                    if(Box[i, j] > 0)
                    {
                        DrawCell(i, j);
                    }
                }
            }
            return Box.name;
        }
    }
}
