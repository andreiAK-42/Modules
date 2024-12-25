using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;

namespace MLS
{
    class Parser
    {
        private List<DataPoint> Graphic = new List<DataPoint>();

        public void Linear(double[][] matrix, MainWindow mainWindow)
        {
            Graphic.Clear();

            double sumX = 0;
            double sumY = 0;
            double qX = 0;
            double xy = 0;

            for (int counterI = 0; counterI < matrix[0].Length; ++counterI)
            {
                sumX += matrix[0][counterI];
                sumY += matrix[1][counterI];
                qX += Math.Pow(matrix[0][counterI], 2);
                xy += matrix[0][counterI] * matrix[1][counterI];
            }

            double[][] solveMatrix = new double[2][];
            solveMatrix[0] = new double[] { qX, sumX, xy };
            solveMatrix[1] = new double[] { sumX, matrix[0].Length, sumY };


            double cramerNumber = (qX * matrix[0].Length) - (sumX * sumX);
            double cramerA = (xy * matrix[0].Length) - (sumY * sumX);
            cramerA = cramerA / cramerNumber;
            double cramerB = (qX * sumY) - (sumX * xy);
            cramerB = cramerB / cramerNumber;


            var plotModel = new PlotModel { Title = "График функции f(x)", TextColor = OxyColor.FromRgb(255, 255, 255) };

            Function func = new Function($"f(x) = {cramerA.ToString().Replace(",", ".")} * x + {cramerB.ToString().Replace(",", ".")}");

            mainWindow.tbFormula.Text = $"y = {cramerA.ToString().Replace(",", ".")} * x + {cramerB.ToString().Replace(",", ".")}";

            for (int counterI = 0; counterI < matrix[0].Length; ++counterI)
            {
                var pointSeries = new ScatterSeries
                {
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 10,
                    MarkerFill = OxyColors.Green
                };

                pointSeries.Points.Add(new ScatterPoint(matrix[0][counterI], matrix[1][counterI]));

                plotModel.Series.Add(pointSeries);

                Graphic.Add(new DataPoint(matrix[0][counterI], SolveFunc(func, matrix[0][counterI].ToString().Replace(",", "."))));
            }

            var xLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 255, 255),
                StrokeThickness = 2
            };

            xLine.Points.Add(new DataPoint(matrix[0].Min(), 0));
            xLine.Points.Add(new DataPoint(matrix[0].Max(), 0));

            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 255, 255), // Красный цвет
                StrokeThickness = 2,
            };

            absicc.Points.Add(new DataPoint(0, matrix[1].Min()));
            absicc.Points.Add(new DataPoint(0, matrix[1].Max()));

            // Создаем серию точек графика
            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(24, 239, 255), // Синий цвет линии
                LineStyle = LineStyle.Dot
            };

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(Graphic);

            // Добавляем серию точек к модели графика
            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(xLine);
            plotModel.Series.Add(absicc);


            // Отображаем график
            mainWindow.pvGraph.Model = plotModel;
            mainWindow.pvGraph.DataContext = plotModel;
        }

        public void Parabola(double[][] matrix, MainWindow mainWindow)
        {
            Graphic.Clear();

            double sumX = 0;
            double sumY = 0;
            double qX = 0;
            double tX = 0;
            double cX = 0;
            double x2y = 0;
            double xy = 0;

            for (int counterI = 0; counterI < matrix[0].Length; ++counterI)
            {
                sumX += matrix[0][counterI];
                sumY += matrix[1][counterI];
                qX += Math.Pow(matrix[0][counterI], 2);
                tX += Math.Pow(matrix[0][counterI], 3);
                cX += Math.Pow(matrix[0][counterI], 4);
                xy += matrix[0][counterI] * matrix[1][counterI];
                x2y += Math.Pow(matrix[0][counterI], 2) * matrix[1][counterI];
            }

            double[][] solveMatrix = new double[3][];
            solveMatrix[0] = new double[] { cX, tX, qX, x2y };
            solveMatrix[1] = new double[] { tX, qX, sumX, xy };
            solveMatrix[2] = new double[] { qX, sumX, matrix[0].Length, sumY };


            double[] cramer = Cramer.Solve(solveMatrix);

            var plotModel = new PlotModel { Title = "График функции f(x)", TextColor = OxyColor.FromRgb(255, 255, 255) };

            Function func = new Function($"f(x) = {cramer[0].ToString().Replace(",", ".")} * x^2 + {cramer[1].ToString().Replace(",", ".")} * x + {cramer[2].ToString().Replace(",", ".")}");

            mainWindow.tbFormula.Text = $"y = {cramer[0].ToString().Replace(",", ".")} * x^2 + {cramer[1].ToString().Replace(",", ".")} * x + {cramer[2].ToString().Replace(",", ".")}";

            for (int counterI = 0; counterI < matrix[0].Length; ++counterI)
            {
                var pointSeries = new ScatterSeries
                {
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 10,
                    MarkerFill = OxyColors.Green
                };

                pointSeries.Points.Add(new ScatterPoint(matrix[0][counterI], matrix[1][counterI]));

                plotModel.Series.Add(pointSeries);

                Graphic.Add(new DataPoint(matrix[0][counterI], SolveFunc(func, matrix[0][counterI].ToString().Replace(",", "."))));
            }

            var xLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 255, 255),
                StrokeThickness = 2
            };

            xLine.Points.Add(new DataPoint(matrix[0].Min(), 0));
            xLine.Points.Add(new DataPoint(matrix[0].Max(), 0));

            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 255, 255), // Красный цвет
                StrokeThickness = 2,
            };

            absicc.Points.Add(new DataPoint(0, matrix[1].Min()));
            absicc.Points.Add(new DataPoint(0, matrix[1].Max()));

            // Создаем серию точек графика
            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(24, 239, 255), // Синий цвет линии
                LineStyle = LineStyle.Dot
            };

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(Graphic);

            // Добавляем серию точек к модели графика
            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(xLine);
            plotModel.Series.Add(absicc);


            // Отображаем график
            mainWindow.pvGraph.Model = plotModel;
            mainWindow.pvGraph.DataContext = plotModel;
        }



        public static double SolveFunc(Function function, string x)
        {
            return new org.mariuszgromada.math.mxparser.Expression($"f({x})", function).calculate();
        }
    }

    public static class Cramer
    {
        public static double[] Solve(double[][] augmentedMatrix)
        {
            if (augmentedMatrix == null || augmentedMatrix.Length == 0)
            {
                throw new ArgumentException("Matrix cannot be null or empty.");
            }

            int n = augmentedMatrix.Length; // Количество уравнений
            int m = augmentedMatrix[0].Length; // Количество переменных + 1 (для правой части)

            if (n != m - 1)
            {
                throw new ArgumentException("The number of rows must be equal to the number of columns - 1 in the augmented matrix.");
            }

            // 1. Извлечение матрицы коэффициентов (A) и вектора правой части (b)
            double[][] coefficientMatrix = new double[n][];
            double[] rightPart = new double[n];

            for (int i = 0; i < n; i++)
            {
                coefficientMatrix[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    coefficientMatrix[i][j] = augmentedMatrix[i][j];
                }
                rightPart[i] = augmentedMatrix[i][n];
            }

            // 2. Вычисление определителя основной матрицы (det(A))
            double detA = Determinant(coefficientMatrix);

            if (Math.Abs(detA) < 1e-8)
            {
                throw new ArgumentException("Determinant of the coefficient matrix is zero. The system may not have a unique solution.");
            }

            // 3. Вычисление определителей для каждой переменной и нахождение решения
            double[] solution = new double[n];
            for (int i = 0; i < n; i++)
            {
                // Создание матрицы для текущей переменной путем замены i-го столбца на вектор правой части
                double[][] tempMatrix = new double[n][];
                for (int j = 0; j < n; j++)
                {
                    tempMatrix[j] = new double[n];
                    for (int k = 0; k < n; k++)
                    {
                        tempMatrix[j][k] = coefficientMatrix[j][k];
                    }
                    tempMatrix[j][i] = rightPart[j];
                }


                // Вычисление определителя для текущей переменной
                double detXi = Determinant(tempMatrix);
                // Нахождение решения для текущей переменной
                solution[i] = detXi / detA;
            }

            return solution;
        }

        private static double Determinant(double[][] matrix)
        {
            int n = matrix.Length;
            if (n == 1)
            {
                return matrix[0][0];
            }
            if (n == 2)
            {
                return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
            }

            double det = 0;
            for (int k = 0; k < n; k++)
            {
                double[][] subMatrix = new double[n - 1][];

                for (int i = 1; i < n; i++)
                {
                    subMatrix[i - 1] = new double[n - 1];
                    for (int j = 0, col = 0; j < n; j++)
                    {
                        if (j != k)
                        {
                            subMatrix[i - 1][col] = matrix[i][j];
                            col++;
                        }
                    }
                }
                det += (k % 2 == 0 ? 1 : -1) * matrix[0][k] * Determinant(subMatrix);
            }
            return det;
        }
    }
}
