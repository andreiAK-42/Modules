using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;

namespace DefiniteIntegral.Methods
{
    public class ParserSolver
    {
        private List<DataPoint> Graphic = new List<DataPoint>();
        private string CurrentFunc = "";

        public void GetFunction(WindowVisualization window, string function, int a, int b)
        {
            Graphic.Clear();

            Function func = new Function("f(x) = " + function);

            CurrentFunc = function;

            for (int counterI = a; counterI <= b; ++counterI)
            {
                Graphic.Add(new DataPoint(counterI, SolveFunc(func, counterI.ToString())));
            }

            var plotModel = new PlotModel { Title = "График функции f(x)", TextColor = OxyColor.FromRgb(255, 255, 255) };
            var maxY = Graphic.Max(x => x.Y);
            var minY = Graphic.Min(x => x.Y);

            var xLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 255, 255),
                StrokeThickness = 2
            };

            xLine.Points.Add(new DataPoint(a, 0));
            xLine.Points.Add(new DataPoint(b, 0));

            // Создаем серию точек графика
            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(24, 239, 255), // Синий цвет линии
                LineStyle = LineStyle.Dot
            };

            var bordera = new LineSeries
            {
                Title = "a",
                Color = OxyColor.FromRgb(24, 239, 255), // Синий цвет линии
                LineStyle = LineStyle.DashDashDot
            };

            bordera.Points.Add(new DataPoint(Graphic[0].X, 0));
            bordera.Points.Add(new DataPoint(Graphic[0].X, Graphic[0].Y));

            var borderab = new LineSeries
            {
                Title = "b",
                Color = OxyColor.FromRgb(24, 239, 255), // Синий цвет линии
                LineStyle = LineStyle.DashDashDot
            };

            borderab.Points.Add(new DataPoint(Graphic[Graphic.Count - 1].X, 0));
            borderab.Points.Add(new DataPoint(Graphic[Graphic.Count - 1].X, Graphic[Graphic.Count - 1].Y));

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(Graphic);

            // Создаем ось Y с заданным интервалом
            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = minY < 0 ? minY : 0,
                Maximum = maxY,
                TicklineColor = OxyColor.FromRgb(255, 255, 255),
                IntervalLength = 60, 
                Title = "Y",
                TextColor = OxyColor.FromRgb(255, 255, 255)
            };

            //Создаем ось X
            var axisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = Graphic[0].X,
                Maximum = Graphic[Graphic.Count - 1].X,
                Title = "X",
                TicklineColor = OxyColor.FromRgb(255, 255, 255),
                TextColor = OxyColor.FromRgb(255, 255, 255)
            };

            // Добавляем оси к модели графика
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            // Добавляем серию точек к модели графика
            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(xLine);
            //plotModel.Series.Add(absicc);
            plotModel.Series.Add(bordera);
            plotModel.Series.Add(borderab);

            plotModel.PlotAreaBorderColor = OxyColor.FromRgb(255, 255, 255);
            // Отображаем график
            window.pvGraph.Model = plotModel;
            window.pvGraph.DataContext = plotModel;
        }

        public double SolveFunc(Function function, string x)
        {
            return new Expression($"f({x})", function).calculate();
        }


        public void RectangleMethod(WindowVisualization window, string function, string nn, double a, double b)
        {
            Function func = new Function("f(x) = " + function.Replace(",", "."));
            var model = window.pvGraph.Model;
            var series = new ScatterSeries();
            double n = double.Parse(nn.Replace(".", ","));
            double h = (b - a) / n;
            double counterI = a;
            string x = "";
            double resultSolver = 0;

            while (counterI <= b - h)
            {
                x = counterI.ToString().Replace(",", ".");
                resultSolver = SolveFunc(func, x);

                var rectangle = new RectangleAnnotation
                {
                    MinimumX = counterI,
                    MaximumX = counterI + h,
                    MinimumY = 0,
                    MaximumY = resultSolver,
                    Fill = OxyColors.LightBlue,
                    Stroke = OxyColors.Black,
                    StrokeThickness = 1
                };

                model.Annotations.Add(rectangle);

                counterI += h;
            }
        }

        public double RectangleMethodOnly(MainWindow window)
        {
            double result = 0;

            var intervalParse = SafeInput.SafeInput.GetSafeInterval(window);

            Function func = new Function("f(x) = " + window.tbFunction.Text.Replace(",", "."));

            double n = double.Parse(window.tbn.Text.Replace(".", ","));
            double h = (intervalParse.Item2 - intervalParse.Item1) / n;
            double counterI = intervalParse.Item1;
            string x = "";
            double resultSolver = 0;
            double resultSolverWithN = 0;

            while (counterI <= intervalParse.Item2 - h)
            {
                x = counterI.ToString().Replace(",", ".");
                resultSolver = SolveFunc(func, x);
                resultSolverWithN = resultSolver * h;

                result += Math.Abs(resultSolverWithN);
                counterI += h;
            }

            return result;
        }

        public void SimpsonMethod(WindowVisualization window, string function, string nn, double a, double b)
        {
            List<double> solvePoints = new List<double>();
            double n = double.Parse(nn.Replace(".", ","));
            double h = (b - a) / n;
            var model = window.pvGraph.Model;

            double result = 0;

            Function func = new Function("f(x) = " + function.Replace(",", "."));

            double counterI = a;
            string x = "";

            while (counterI <= b)
            {
                x = counterI.ToString().Replace(",", ".");
                solvePoints.Add(SolveFunc(func, x));

                counterI += h;
            }

            double sumOddNumbers = 0;
            int counterJ = 1;

            while (counterJ <= solvePoints.Count - 2)
            {
                sumOddNumbers += Math.Abs(solvePoints[counterJ]);

                counterJ += 2;
            }

            double resultOddNumber = 4 * sumOddNumbers;

            double sumEvenNumbers = 0;
            int counterB = 2;
            while (counterB <= solvePoints.Count - 2)
            {
                sumEvenNumbers += Math.Abs(solvePoints[counterB]);

                counterB += 2;
            }

            double resultEvenNumber = 2 * sumEvenNumbers;

            result = (h / 3) * ((solvePoints[0] + solvePoints[solvePoints.Count - 1]) + resultEvenNumber + resultOddNumber);
        }

        public double SimpsonMethodOnly(MainWindow window)
        {
            List<double> solvePoints = new List<double>();
            var intervalParse = SafeInput.SafeInput.GetSafeInterval(window);
            double n = double.Parse(window.tbn.Text.Replace(".", ","));
            double h = (intervalParse.Item2 - intervalParse.Item1) / n;

            double result = 0;

            Function func = new Function("f(x) = " + window.tbFunction.Text.Replace(",", "."));

            double counterI = intervalParse.Item1;
            string x = "";

            while (counterI <= intervalParse.Item2)
            {
                x = counterI.ToString().Replace(",", ".");
                solvePoints.Add(Math.Abs(SolveFunc(func, x)));

                counterI += h;
            }

            double sumOddNumbers = 0;
            int counterJ = 1;

            while (counterJ <= solvePoints.Count - 2)
            {
                sumOddNumbers += Math.Abs(solvePoints[counterJ]);

                counterJ += 2;
            }

            double resultOddNumber = 4 * sumOddNumbers;

            double sumEvenNumbers = 0;
            int counterB = 2;
            while (counterB <= solvePoints.Count - 2)
            {
                sumEvenNumbers += Math.Abs(solvePoints[counterB]);

                counterB += 2;
            }

            double resultEvenNumber = 2 * sumEvenNumbers;

            result = (h / 3) * ((solvePoints[0] + solvePoints[solvePoints.Count - 1]) + resultEvenNumber + resultOddNumber);
            return result;
        }


        public void TrapezoidMethod(WindowVisualization window, string function, string nn, double a, double b)
        {
            Function func = new Function("f(x) = " + function.Replace(",", "."));
            var model = window.pvGraph.Model;
            var series = new ScatterSeries();

            double n = double.Parse(nn.Replace(".", ","));
            double h = (b - a) / n;
            double counterI = a + h;
            
            string x1 = ""; // X i-1
            string x2 = ""; // Xi

            double solveFunc1 = 0;
            double solveFunc2 = 0;

            while (counterI <= b)
            {
                x1 = (counterI - h).ToString().Replace(",", ".");
                x2 = counterI.ToString().Replace(",", ".");

                solveFunc1 = (SolveFunc(func, x1));
                solveFunc2 = SolveFunc(func, x2);

                var polygon = new PolygonAnnotation();

                double x1Double = double.Parse(x1.Replace(".", ","));
                double x2Double = double.Parse(x2.Replace(".", ","));

                polygon.Points.Add(new DataPoint(x1Double, 0));
                polygon.Points.Add(new DataPoint(x2Double, 0));
                polygon.Points.Add(new DataPoint(x2Double, 0));
                polygon.Points.Add(new DataPoint(x2Double, solveFunc2));
                polygon.Points.Add(new DataPoint(x1Double, solveFunc1));

                // Добавьте многоугольник в серию
                model.Annotations.Add(polygon);

                counterI += h;
            }
        }

        public double TrapezoidMethodOnly(MainWindow window)
        {
            double result = 0;

            var intervalParse = SafeInput.SafeInput.GetSafeInterval(window);
            double n = double.Parse(window.tbn.Text.Replace(".", ","));
            double h = (intervalParse.Item2 - intervalParse.Item1) / n;
            Function func = new Function("f(x) = " + window.tbFunction.Text.Replace(",", "."));

            double counterI = intervalParse.Item1 + h;

            string x1 = ""; // X i-1
            string x2 = ""; // Xi

            double solveFunc1 = 0;
            double solveFunc2 = 0;

            while (counterI <= intervalParse.Item2)
            {
                x1 = (counterI - h).ToString().Replace(",", ".");
                x2 = counterI.ToString().Replace(",", ".");

                solveFunc1 = (SolveFunc(func, x1));
                solveFunc2 = SolveFunc(func, x2);

                result += Math.Abs((((solveFunc1 + solveFunc2) / 2) * h));
                counterI += h;
            }
            return result;
        }
    }
}
