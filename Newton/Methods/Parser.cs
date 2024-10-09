using OxyPlot.Series;
using OxyPlot;
using MathNet.Symbolics;
using Function = org.mariuszgromada.math.mxparser.Function;
using System.Windows;

namespace Newton.Methods
{
    public class Parser
    {
        private static double Phi = (1 + Math.Sqrt(5)) / 2;

        private List<DataPoint> Graphic = new List<DataPoint>();
        private string CurrentFunc = "";
        private const int MaxIterations = 100;

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

        public static string FindDerivative(string functionString)
        {
            var expr = SymbolicExpression.Parse(functionString);
            var x = SymbolicExpression.Variable("x");

            expr.Differentiate(x);

            return expr.Differentiate(x).ToString();
        }

        public void FindRoot(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);
            
            Function derFunc = new Function("f(x) = " + FindDerivative(window.tbFunction.Text));

            var intervalParse = SafeInput.GetSafeIntervalAB(window);
            double x0 = Convert.ToDouble(intervalParse.Item1);
            double eps = Convert.ToDouble(window.tbe.Text);

            double x = x0;
            int i = 0;

            while (Math.Abs(SolveFunc(func, x.ToString())) > eps && i < MaxIterations)
            {
                x = x - SolveFunc(func, x.ToString()) / SolveFunc(derFunc, x.ToString().Replace(",", "."));
                i++;

            }

            if (i >= MaxIterations)
            {
                throw new Exception("Превышено максимальное число итераций");
            }

            SafeInput.ShowMessage($"Результат:\n Корень = {x}", MessageBoxImage.Information);
        }

        public static double SolveFunc(Function function, string x)
        {
            return new org.mariuszgromada.math.mxparser.Expression($"f({x})", function).calculate();
        }
    }
}
