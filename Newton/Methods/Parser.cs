using OxyPlot.Series;
using OxyPlot;
using MathNet.Symbolics;
using Function = org.mariuszgromada.math.mxparser.Function;
using System.Windows;
using MathNet.Numerics;

namespace Newton.Methods
{
    public class Parser
    {
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

        public void FindRoot(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);
            string fder = FindDerivative(window.tbFunction.Text);
            Function derFunc = new Function("f(x) = " + fder);

            var intervalParse = SafeInput.GetSafeIntervalAB(window);
            double x0 = Convert.ToDouble(window.tbx0.Text);
            double eps = Convert.ToDouble(window.tbe.Text);

            double x = x0;
            int i = 0;

            while (Math.Abs(SolveFunc(func, x.ToString().Replace(",", "."))) > eps && i < MaxIterations)
            {
                x = x - SolveFunc(func, x.ToString().Replace(",", ".")) / SolveFunc(derFunc, x.ToString().Replace(",", "."));
                i++;
            }

            if (i >= MaxIterations)
            {
                SafeInput.ShowMessage($"Вероятно корней нет. Превышено количество итераций 100", MessageBoxImage.Error);
                return;
            }

            SafeInput.ShowMessage($"Результат:\n Корень = {x}", MessageBoxImage.Information);
        }

        public void FindMinimum(MainWindow window, string function = null)
        {
            Function func = new Function("f(x) = " + function == null ? window.tbFunction.Text : function);
            string fder = FindDerivative(function == null ? window.tbFunction.Text : function);
            Function dfFunc = new Function("f(x) = " + fder);
            string ffder = FindDerivative(fder);
            string fffder = FindDerivative(ffder);
            Function dffFunc = new Function("f(x) = " + ffder);
            Function dfffFunc = new Function("f(x) = " + fffder);

            var intervalParse = SafeInput.GetSafeIntervalAB(window);
            double a = intervalParse.Item1;
            double b = intervalParse.Item2;
            double eps = Convert.ToDouble(window.tbe.Text);

            double x0 = (a + b) / 2; // Начальное приближение
            double x1;

            while (true)
            {
                double f1 = SolveFunc(dfFunc, x0.ToString().Replace(",", "."));
                double f2 = SolveFunc(dffFunc, x0.ToString().Replace(",", "."));
                double f3 = SolveFunc(dfffFunc, x0.ToString().Replace(",", "."));

                if (Math.Abs(f1) < eps || f2 <= 0) // Условие остановки
                {
                    SafeInput.ShowMessage($"Результат:\n Результат = {x0}", MessageBoxImage.Information);
                    return;
                }

                // Обновляем значение x с учетом первой и второй производной
                x1 = x0 - f1 / f2;

                // Проверка, не вышло ли x1 за пределы отрезка
                if (x1 < a || x1 > b)
                {
                    SafeInput.ShowMessage($"Результат:\n Результат = {x0}", MessageBoxImage.Information); // Возвращаем текущее значение, если вышло за пределы
                    return;
                }

                x0 = x1; // Обновляем значение для следующей итерации
            }
        }

        public void FindMaximum(MainWindow window)
        {
            FindMinimum(window, "-(" + window.tbFunction.Text + ")");
        }

        private static double SolveFunc(Function function, string x)
        {
            return new org.mariuszgromada.math.mxparser.Expression($"f({x})", function).calculate();
        }

        private static string FindDerivative(string functionString)
        {
            var expr = SymbolicExpression.Parse(functionString);
            var x = SymbolicExpression.Variable("x");

            expr.Differentiate(x);

            return expr.Differentiate(x).ToString();
        }
    }
}
