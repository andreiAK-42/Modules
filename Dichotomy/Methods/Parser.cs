using OxyPlot;
using Dichotomy;
using OxyPlot.Series;
using System.Windows;
using DichotomyMethod.Methods;
using org.mariuszgromada.math.mxparser;

namespace DichotomyMethod
{
    public class Graph
    {
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

            SafeInput.ShowMessage($"Количество пересечений функции с осью абсцисс = {SafeInput.CheckNumberIntersections(Graphic)}", MessageBoxImage.Information);
        }

        public void DichotomyMethod(MainWindow window)
        {
            int numberIntersections = SafeInput.CheckNumberIntersections(Graphic);
            if (numberIntersections == 0)
            {
                SafeInput.ShowMessage("График не пересекает ось абсцисс", MessageBoxImage.Error);
                return;
            }

            if (Graphic.Count == 0 || CurrentFunc != window.tbFunction.Text)
            {
                SafeInput.ShowMessage("Сначала постройте график функции", MessageBoxImage.Error);
                return;
            }

            if (numberIntersections == 1)
            {
                SafeInput.ShowMessage($"Результат: {Math.Round(SolveStandartDichotomyMethod(window), window.tbe.Text.Length - 2)}. Других точек пересечения не обнаружено", MessageBoxImage.Information);
            }
            else
            {
                if (MessageBox.Show($"Результат X = {Math.Round(SolveStandartDichotomyMethod(window), window.tbe.Text.Length - 2)}\nНайдены другие точки пересечения. Продолжить автоматическое вычисление точек пересечения?",
                    "Метод дихотомии",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SolveAutomatDichotomyMethod(window);
                }
            }       
            return;
        }

        private double SolveStandartDichotomyMethod(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);

            var intervalParse = SafeInput.GetSafeIntervalAB(window);

            double fa = SolveFunc(func, intervalParse.Item1.ToString());

            double a = Convert.ToDouble(intervalParse.Item1);
            double b = Convert.ToDouble(intervalParse.Item2);
            double eps = Convert.ToDouble(window.tbe.Text);

            while (b - a > eps)
            {
                double c = (a + b) / 2;
                double fc = SolveFunc(func, c.ToString().Replace(",", "."));

                if (Math.Abs(fc) == 0)
                {
                    return c;
                }
                else if (fa * fc < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                    fa = fc;
                }
            }
            double resultNumber = Math.Round((a + b) / 2, window.tbe.Text.Length - 2);

            return resultNumber.ToString() == "-0" ? 0 : resultNumber;
        }

        private void SolveAutomatDichotomyMethod(MainWindow window)
        {
            string result = "";

            Function func = new Function("f(x) = " + window.tbFunction.Text);

            for (int counterI = 2; counterI < Graphic.Count; ++counterI)
            {
                if ((Graphic[counterI - 1].Y < 0 && Graphic[counterI].Y > 0) || (Graphic[counterI - 1].Y > 0 && Graphic[counterI].Y < 0) || (Graphic[counterI - 1].Y == 0 && Graphic[counterI].Y < 0) || (Graphic[counterI - 1].Y < 0 && Graphic[counterI].Y == 0))
                {
                    double fa = SolveFunc(func, Graphic[counterI - 1].X.ToString());
                    
                    double eps = Convert.ToDouble(window.tbe.Text);
                    double a = Graphic[counterI - 2].X;
                    double b = Graphic[counterI].X;

                    while (b - a > eps)
                    {
                        double c = (a + b) / 2;
                        double fc = SolveFunc(func, c.ToString().Replace(",", "."));

                        if (Math.Abs(fc) == 0)
                        {
                            break;
                        }
                        else if (fa * fc < 0)
                        {
                            b = c;
                        }
                        else
                        {
                            a = c;
                            fa = fc;
                        }
                    }
                    double resultNumber = Math.Round((a + b) / 2, window.tbe.Text.Length - 2);
                    result += $"X: = {(resultNumber.ToString() == "-0" ? "0" : resultNumber)}\n";
                }               
            }
            SafeInput.ShowMessage("Результат:\n" + result, MessageBoxImage.Information);
        }

        public double SolveFunc(Function function, string x)
        {
            return new org.mariuszgromada.math.mxparser.Expression($"f({x})", function).calculate();
        }
    }
}
