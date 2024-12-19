namespace SolvingEquations
{
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
