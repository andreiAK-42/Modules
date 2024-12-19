namespace SolvingEquations
{
    public class GausMethod
    {
        public uint RowCount;
        public uint ColumCount;
        public double[][] Matrix { get; set; }
        public double[] RightPart { get; set; }
        public double[] Answer { get; set; }

        public GausMethod(uint Row, uint Colum)
        {
            RightPart = new double[Row];
            Answer = new double[Row];
            Matrix = new double[Row][];
            for (int i = 0; i < Row; i++)
                Matrix[i] = new double[Colum];
            RowCount = Row;
            ColumCount = Colum;

            //обнулим массив
            for (int i = 0; i < Row; i++)
            {
                Answer[i] = 0;
                RightPart[i] = 0;
                for (int j = 0; j < Colum; j++)
                    Matrix[i][j] = 0;
            }
        }

        private void SortRows(int SortIndex)
        {
            double MaxElement = Matrix[SortIndex][SortIndex];
            int MaxElementIndex = SortIndex;
            for (int i = SortIndex + 1; i < RowCount; i++)
            {
                if (Matrix[i][SortIndex] > MaxElement)
                {
                    MaxElement = Matrix[i][SortIndex];
                    MaxElementIndex = i;
                }
            }

            //теперь найден максимальный элемент ставим его на верхнее место
            if (MaxElementIndex > SortIndex)//если это не первый элемент
            {
                double Temp;

                Temp = RightPart[MaxElementIndex];
                RightPart[MaxElementIndex] = RightPart[SortIndex];
                RightPart[SortIndex] = Temp;

                for (int i = 0; i < ColumCount; i++)
                {
                    Temp = Matrix[MaxElementIndex][i];
                    Matrix[MaxElementIndex][i] = Matrix[SortIndex][i];
                    Matrix[SortIndex][i] = Temp;
                }
            }
        }

        public int SolveGauss()
        {
            if (RowCount != ColumCount)
                return 1; //нет решения

            for (int i = 0; i < RowCount - 1; i++)
            {
                SortRows(i);
                for (int j = i + 1; j < RowCount; j++)
                {
                    if (Matrix[i][i] != 0) //если главный элемент не 0, то производим вычисления
                    {
                        double MultElement = Matrix[j][i] / Matrix[i][i];
                        for (int k = i; k < ColumCount; k++)
                            Matrix[j][k] -= Matrix[i][k] * MultElement;
                        RightPart[j] -= RightPart[i] * MultElement;
                    }
                    //для нулевого главного элемента просто пропускаем данный шаг
                }
            }

            //ищем решение
            for (int i = (int)(RowCount - 1); i >= 0; i--)
            {
                Answer[i] = RightPart[i];

                for (int j = (int)(RowCount - 1); j > i; j--)
                    Answer[i] -= Matrix[i][j] * Answer[j];

                if (Matrix[i][i] == 0)
                    if (RightPart[i] == 0)
                        return 2; //множество решений
                    else
                        return 1; //нет решения

                Answer[i] /= Matrix[i][i];

            }
            return 0;
        }

        public int SolveGaussJordan()
        {
            double[][] matrix = new double[Matrix.Length][];

            for (var counterI = 0; counterI < matrix.Length; ++counterI)
            {
                matrix[counterI] = new double[matrix.Length + 1];
                Matrix[counterI].CopyTo(matrix[counterI], 0);
                matrix[counterI][matrix[counterI].Length - 1] = RightPart[counterI];
            }

            int n = matrix.Length;

            // Прямой ход (приведение к ступенчатому виду)
            for (int k = 0; k < n; k++)
            {
                // Поиск опорного элемента (ведущего элемента) в столбце
                int maxRow = k;
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(matrix[i][k]) > Math.Abs(matrix[maxRow][k]))
                    {
                        maxRow = i;
                    }
                }

                // Меняем строки местами, если опорный элемент не на k-й строке
                if (maxRow != k)
                {
                    for (int i = 0; i <= n; i++)
                    {
                        double temp = matrix[k][i];
                        matrix[k][i] = matrix[maxRow][i];
                        matrix[maxRow][i] = temp;
                    }
                }
                // Делаем опорный элемент равным 1
                double pivot = matrix[k][k];
                if (Math.Abs(pivot) < 1e-8)
                {
                    throw new ArgumentException("The system of equations has no unique solution or no solution.");
                }
                for (int i = k; i <= n; i++)
                {
                    matrix[k][i] /= pivot;
                }

                // Обнуляем все элементы столбца выше и ниже опорного
                for (int i = 0; i < n; i++)
                {
                    if (i != k)
                    {
                        double factor = matrix[i][k];
                        for (int j = k; j <= n; j++)
                        {
                            matrix[i][j] -= factor * matrix[k][j];
                        }
                    }
                }
            }
            Matrix = matrix;
            double[] x = new double[n];
            for (int m = 0; m < n; m++)
            {
                x[m] = matrix[m][n];
            }
            Answer = x;
            return 1;
        }

        public override String ToString()
        {
            String S = "";
            for (int i = 0; i < RowCount; i++)
            {
                S += "\r\n";
                for (int j = 0; j < ColumCount; j++)
                {
                    S += Matrix[i][j].ToString("F04") + "\t";
                }

                S += "\t" + Answer[i].ToString("F08");
                S += "\t" + RightPart[i].ToString("F04");
            }
            return S;
        }
    }
}

