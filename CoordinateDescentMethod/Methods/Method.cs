using OxyPlot.Series;
using OxyPlot;
using org.mariuszgromada.math.mxparser;
using System.Windows;
using OxyPlot.Axes;

namespace CoordinateDescentMethod.Methods
{
    public class Method
    {
        private static double Phi = (1 + Math.Sqrt(5)) / 2;

        private List<DataPoint> Graphic = new List<DataPoint>();
        private string CurrentFunc = "";

        public void GetFunction(MainWindow window, string function)
        {
            Graphic.Clear();
            
            Function func = new Function("f(x) = " + function);
            
            CurrentFunc = function;
            
            var intervalParse = SafeInput.GetSafeInterval(window);

            for (int counterI = intervalParse.Item1 - 2; counterI <= intervalParse.Item2 + 2; ++counterI)
            {
                 Graphic.Add(new DataPoint(counterI, SolveFunc(func, counterI.ToString())));
            }
            
            var plotModel = new PlotModel { Title = "График функции f(x)" };
            
            var xLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2
            };
            
            xLine.Points.Add(new DataPoint(intervalParse.Item1, 0));
            xLine.Points.Add(new DataPoint(intervalParse.Item2, 0));
            
            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2,
            };
            
            absicc.Points.Add(new DataPoint(0, intervalParse.Item2));
            absicc.Points.Add(new DataPoint(0, intervalParse.Item1));

             // Создаем серию точек графика
             var lineSeries = new LineSeries
             {
                 Title = "f(x)",
                 Color = OxyColor.FromRgb(0, 0, 255), // Синий цвет линии
                 LineStyle = LineStyle.Dot
             };

             // Добавляем все точки в серию
             lineSeries.Points.AddRange(Graphic);

             // Добавляем серию точек к модели графика
             plotModel.Series.Add(lineSeries);
             plotModel.Series.Add(xLine);
             plotModel.Series.Add(absicc);*/

            // Создаем модель графика
            var plotModel = new PlotModel { Title = "График f(x, y) = x^2 + y^2" };

            // Оси
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "X" });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Y" });

            // Создаем серию для графика
            var series = new FunctionSeries(
                x => x * x, // Функция f(x) для y = 0
                -5, 5, // Диапазон значений x
                0.1 // Шаг по x
            );

            plotModel.Series.Add(series);

            // Отображаем график
            window.pvGraph.Model = plotModel;
            window.pvGraph.DataContext = plotModel;
        }
        
        public void CoordinateDescentMethod(MainWindow window)
        {
            double x0 = Convert.ToDouble(window.tbx0.Text);
            double y0 = Convert.ToDouble(window.tby0.Text);

            double x = x0;
            double y = y0;
            double eps = Convert.ToDouble(window.tbe.Text);

            for (int i = 0; i < 100; i++)
            {
                // Оптимизация по x
                double minX = FindMinimum(x, y, eps, Function, "x");
                x = minX;

                // Оптимизация по y
                double minY = FindMinimum(x, y, eps, Function, "y");
                y = minY;

                Console.WriteLine($"Iteration {i + 1}: x = {x}, y = {y}, f(x, y) = {Function(x, y)}");

                // Критерий остановки - малое изменение значения функции
                if (Math.Abs(Function(x, y) - Function(x0, y0)) < eps)
                {
                    break;
                }
                x0 = x;
                y0 = y;
            }

            return (x, y);
        }

        private double FindMinimum(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);

            var intervalParse = SafeInput.GetSafeIntervalAB(window);

            double a = Convert.ToDouble(intervalParse.Item1);
            double b = Convert.ToDouble(intervalParse.Item2);
            double eps = Convert.ToDouble(window.tbe.Text);

            if (a >= b)
            {
                throw new ArgumentException("a must be less than b");
            }

            // Вычисляем начальные точки
            double x1 = b - (b - a) / Phi;
            double x2 = a + (b - a) / Phi;

            double f1 = SolveFunc(func, x1.ToString().Replace(",", "."));
            double f2 = SolveFunc(func, x2.ToString().Replace(",", "."));

            while (Math.Abs(b - a) > eps)
            {
                if (f1 < f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = b - (b - a) / Phi;
                    f1 = SolveFunc(func, x1.ToString().Replace(",", "."));
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + (b - a) / Phi;
                    f2 = SolveFunc(func, x2.ToString().Replace(",", "."));
                }
            }
            string resultX = Math.Round(x1, window.tbe.Text.Length - 2).ToString();
            string resultY = Math.Round(SolveFunc(func, x1.ToString().Replace(",", ".")), window.tbe.Text.Length - 2).ToString();

            SafeInput.ShowMessage($"Результат:\n X = {resultX} Y = {resultY}", MessageBoxImage.Information);
        }

       

        public static double SolveFunc(Function function, string x)
        {
            return new org.mariuszgromada.math.mxparser.Expression($"f({x})", function).calculate();
        }
    }
}
