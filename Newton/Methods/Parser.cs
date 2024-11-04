using OxyPlot;
using System.Windows;
using OxyPlot.Series;
using MathNet.Symbolics;
using Function = org.mariuszgromada.math.mxparser.Function;

namespace Newton.Methods
{
    public class Parser
    {
        private List<DataPoint> Graphic = new List<DataPoint>();
        private const int MaxIterations = 100;

        public void GetFunction(MainWindow window, string function)
        {
            Graphic.Clear();

            Function func = new Function("f(x) = " + function);

            var intervalParse = SafeInput.GetSafeInterval(window);

            for (int counterI = intervalParse.Item1 - 2; counterI <= intervalParse.Item2 + 2; ++counterI)
            {
                Graphic.Add(new DataPoint(counterI, SolveFunc(func, counterI.ToString())));
            }

            var plotModel = new PlotModel { Title = "График функции f(x)" };

            var xLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 2
            };

            xLine.Points.Add(new DataPoint(intervalParse.Item1, 0));
            xLine.Points.Add(new DataPoint(intervalParse.Item2, 0));

            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 2,
            };

            absicc.Points.Add(new DataPoint(0, intervalParse.Item2));
            absicc.Points.Add(new DataPoint(0, intervalParse.Item1));

            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(0, 0, 255),
                LineStyle = LineStyle.Dot
            };

            lineSeries.Points.AddRange(Graphic);

            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(xLine);
            plotModel.Series.Add(absicc);

            window.pvGraph.Model = plotModel;
            window.pvGraph.DataContext = plotModel;
        }

        public void FindRoot(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);
            string fder = FindDerivative(window.tbFunction.Text);
            Function derFunc = new Function("f(x) = " + fder);

            var intervalParse = SafeInput.GetSafeIntervalAB(window);
            double x0 = Convert.ToDouble(window.tba.Text);
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

            var plotModel = window.pvGraph.Model;

            var pointSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 10,
                MarkerFill = OxyColors.Green
            };

            pointSeries.Points.Add(new ScatterPoint(x, SolveFunc(func, x.ToString().Replace(",", ".")), 10));
            plotModel.Series.Add(pointSeries);
            window.pvGraph.InvalidatePlot(true);

            string resultX = Math.Round(x, window.tbe.Text.Length - 2).ToString();

            SafeInput.ShowMessage($"Результат:\n Корень = {resultX}", MessageBoxImage.Information);
        }

        public void FindMinMax(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);
            string fder = FindDerivative(window.tbFunction.Text);
            Function dfFunc = new Function("f(x) = " + fder);
            string ffder = FindDerivative(fder);
            string fffder = FindDerivative(ffder);
            Function dffFunc = new Function("f(x) = " + ffder);
            Function dfffFunc = new Function("f(x) = " + fffder);

            var intervalParse = SafeInput.GetSafeIntervalAB(window);
            double a = intervalParse.Item1;
            double b = intervalParse.Item2;
            double eps = Convert.ToDouble(window.tbe.Text);

            double x = Convert.ToDouble(window.tbx0.Text);

            var plotModel = window.pvGraph.Model;

            var pointSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 10,
                MarkerFill = OxyColors.Black
            };

            var pointSeriesSolver = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerFill = OxyColors.Blue
            };

            while (Math.Abs(SolveFunc(dfFunc, x.ToString().Replace(",", "."))) > eps)
            {
                var lineSeriesPrvev = new LineSeries
                {
                    Title = x.ToString(),
                    Color = OxyColor.FromRgb(0, 0, 255),
                    LineStyle = LineStyle.Dash
                };
                lineSeriesPrvev.Points.Add(new DataPoint(x, SolveFunc(func, x.ToString().Replace(",", "."))));
                lineSeriesPrvev.Points.Add(new DataPoint(x - SolveFunc(dfFunc, x.ToString().Replace(",", ".")) / SolveFunc(dffFunc, x.ToString().Replace(",", ".")), SolveFunc(func, (x - SolveFunc(dfFunc, x.ToString().Replace(",", ".")) / SolveFunc(dffFunc, x.ToString().Replace(",", "."))).ToString().Replace(",", "."))));
                plotModel.Series.Add(lineSeriesPrvev);


                var lineSeries = new LineSeries
                {
                    Title = x.ToString(),
                    Color = OxyColor.FromRgb(0, 0, 255),
                    LineStyle = LineStyle.Dash
                };
                lineSeries.Points.Add(new DataPoint(x, 0));
                lineSeries.Points.Add(new DataPoint(x, SolveFunc(func, x.ToString().Replace(",", "."))));
                plotModel.Series.Add(lineSeries);

                pointSeriesSolver.Points.Add(new ScatterPoint(x, SolveFunc(func, x.ToString().Replace(",", ".")), 5));

                x = x - SolveFunc(dfFunc, x.ToString().Replace(",", ".")) / SolveFunc(dffFunc, x.ToString().Replace(",", "."));

                if (x < a || x > b)
                {
                    SafeInput.ShowMessage($"Выход за интервал изоляции. Ничего не найдено!", MessageBoxImage.Error);
                    return;
                }
            }

            SolveFunc(dffFunc, x.ToString().Replace(",", "."));

            if (SolveFunc(dffFunc, x.ToString().Replace(",", ".")) > 0)
            {
                SafeInput.ShowMessage($"Найден минимум: {Math.Round(x, window.tbe.Text.Length - 2)}:{Math.Round(SolveFunc(func, x.ToString().Replace(",", ".")), window.tbe.Text.Length - 2)}", MessageBoxImage.Information);
            }
            else if (SolveFunc(dffFunc, x.ToString().Replace(",", ".")) < 0)
            {
                SafeInput.ShowMessage($"Найден максимум: {Math.Round(x, window.tbe.Text.Length - 2)}:{Math.Round(SolveFunc(func, x.ToString().Replace(",", ".")), window.tbe.Text.Length - 2)}", MessageBoxImage.Information);
            }
            else
            {
                SafeInput.ShowMessage($"Найдена точка перегиба: {Math.Round(x, window.tbe.Text.Length - 2)}:{Math.Round(SolveFunc(func, x.ToString().Replace(",", ".")), window.tbe.Text.Length - 2)}", MessageBoxImage.Information);
            }

            pointSeries.Points.Add(new ScatterPoint(x, SolveFunc(func, x.ToString().Replace(",", ".")), 10));
            plotModel.Series.Add(pointSeries);
            plotModel.Series.Add(pointSeriesSolver);
            window.pvGraph.InvalidatePlot(true);
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
