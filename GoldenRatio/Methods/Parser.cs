using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;
using System.Windows;

namespace GoldenRatio.Methods
{
    public class Parser
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
            plotModel.Series.Add(absicc);

            // Отображаем график
            window.pvGraph.Model = plotModel;
            window.pvGraph.DataContext = plotModel;        
        }

        public void FindMinimum(MainWindow window)
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

        public void FindMaximum(MainWindow window)
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
                if (f1 > f2)
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
