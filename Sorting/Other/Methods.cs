using System.Windows;

namespace Sorting
{
    public class Methods
    {
        private static int FastSortIterationCount;

        #region Сортировка пузырьком
        public static (int, int[]) BubbleSorting(int[] originalMassive, bool ascending = true)
        {
            int counterIteration = 0;
            int[] massive = new int[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            while (!Sorted(massive, ascending))
            {
                for (int counterI = 0; counterI < massive.Length - 1; ++counterI)
                {
                    if (ascending ? massive[counterI] > massive[counterI + 1] : massive[counterI] < massive[counterI + 1])
                    {
                        massive.Replace(counterI, counterI + 1);
                    }
                }
                ++counterIteration;
            }
            return (counterIteration, massive);
        }

        public static (int, double[]) BubbleSorting(double[] originalMassive, bool ascending = true)
        {
            int counterIteration = 0;
            double[] massive = new double[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            while (!Sorted(massive, ascending))
            {
                for (int counterI = 0; counterI < massive.Length - 1; ++counterI)
                {
                    if (ascending ? massive[counterI] > massive[counterI + 1] : massive[counterI] < massive[counterI + 1])
                    {
                        massive.Replace(counterI, counterI + 1);
                    }
                }
                ++counterIteration;
            }
            return (counterIteration, massive);
        }
        #endregion

        #region Сортировка вставками

        public static (int, int[]) InsertionSort(int[] originalMassive, bool ascending = true)
        {
            int counterIteration = 0;
            int[] massive = new int[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            while (!Sorted(massive, ascending))
            {
                for (int i = 1; i < massive.Length; i++)
                {
                    int k = massive[i];
                    int j = i - 1;
                    ++counterIteration;
                    while (j >= 0 && (ascending ? massive[j] > k : massive[j] < k))
                    {
                        massive[j + 1] = massive[j];
                        massive[j] = k;
                        j--;
                    }
                }
            }
            return (counterIteration, massive);
        }

        public static (int, double[]) InsertionSort(double[] originalMassive, bool ascending = true)
        {
            int counterIteration = 0;
            double[] massive = new double[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            while (!Sorted(massive, ascending))
            {
                for (int i = 1; i < massive.Length; i++)
                {
                    double k = massive[i];
                    int j = i - 1;
                    ++counterIteration;
                    while (j >= 0 && (ascending ? massive[j] > k : massive[j] < k))
                    {
                        massive[j + 1] = massive[j];
                        massive[j] = k;
                        j--;
                    }
                }
            }
            return (counterIteration, massive);
        }
        #endregion

        #region Быстрая сортировка
        public static (int, int[]) QuickSort(int[] originalMassive, bool ascending)
        {
            int[] array = new int[originalMassive.Length];

            originalMassive.CopyTo(array, 0);

            FastSortIterationCount = 0;
            Sort(array, 0, array.Length - 1, ascending);

            return (FastSortIterationCount, array);
        }

        private static void Sort(int[] array, int left, int right, bool ascending)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, ascending);

                Sort(array, left, pivotIndex - 1, ascending);
                Sort(array, pivotIndex + 1, right, ascending);
                ++FastSortIterationCount;
            }
        }

        private static int Partition(int[] array, int left, int right, bool ascending)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (ascending ? array[j] <= pivot : array[j] >= pivot)
                {
                    i++;
                    array.Replace(i, j);
                }
            }
            array.Replace(i + 1, right);

            return i + 1;
        }

        public static (int, double[]) QuickSort(double[] originalMassive, bool ascending)
        {
            double[] array = new double[originalMassive.Length];

            originalMassive.CopyTo(array, 0);

            FastSortIterationCount = 0;
            Sort(array, 0, array.Length - 1, ascending);
            return (FastSortIterationCount, array);
        }

        private static void Sort(double[] array, int left, int right, bool ascending)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, ascending);

                Sort(array, left, pivotIndex - 1, ascending);
                Sort(array, pivotIndex + 1, right, ascending);
                ++FastSortIterationCount;
            }
        }

        private static int Partition(double[] array, int left, int right, bool ascending)
        {
            double pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (ascending ? array[j] <= pivot : array[j] >= pivot)
                {
                    i++;
                    array.Replace(i, j);
                }
            }
            array.Replace(i + 1, right);

            return i + 1;
        }
        #endregion

        #region Шейкерная сортировка
        public static (int, int[]) ShekerSort(int[] originalMassive, bool ascending = true)
        {
            int counterIteration = 0;

            int[] massive = new int[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            int left = 0, right = massive.Length - 1; // левая и правая границы сортируемой области массива
            int flag = 1;

            while ((left < right) && flag > 0)
            {
                flag = 0;
                ++counterIteration;
                for (int i = left; i < right; i++)  //двигаемся слева направо
                {
                    if (ascending ? massive[i] > massive[i + 1] : massive[i] < massive[i + 1]) // если следующий элемент меньше текущего,
                    {             // меняем их местами
                        int t = massive[i];
                        massive[i] = massive[i + 1];
                        massive[i + 1] = t;
                        flag = 1;      // перемещения в этом цикле были
                    }
                }
                right--; // сдвигаем правую границу на предыдущий элемент
                for (int i = right; i > left; i--)  //двигаемся справа налево
                {
                    if (ascending ? massive[i - 1] > massive[i] : massive[i - 1] < massive[i]) // если предыдущий элемент больше текущего,
                    {            // меняем их местами
                        int t = massive[i];
                        massive[i] = massive[i - 1];
                        massive[i - 1] = t;
                        flag = 1;    // перемещения в этом цикле были
                    }
                }
                ++counterIteration;
                left++; // сдвигаем левую границу на следующий элемент
                if (flag == 0) break;
            }

            return (counterIteration, massive);
        }

        public static (int, double[]) ShekerSort(double[] originalMassive, bool ascending = true)
        {
            int counterIteration = 0;

            double[] massive = new double[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            int left = 0, right = massive.Length - 1; // левая и правая границы сортируемой области массива
            int flag = 1;

            while ((left < right) && flag > 0)
            {
                flag = 0;
                ++counterIteration;
                for (int i = left; i < right; i++)  //двигаемся слева направо
                {
                    if (ascending ? massive[i] > massive[i + 1] : massive[i] < massive[i + 1]) // если следующий элемент меньше текущего,
                    {             // меняем их местами
                        double t = massive[i];
                        massive[i] = massive[i + 1];
                        massive[i + 1] = t;
                        flag = 1;      // перемещения в этом цикле были
                    }
                }
                right--; // сдвигаем правую границу на предыдущий элемент
                ++counterIteration;
                for (int i = right; i > left; i--)  //двигаемся справа налево
                {
                    if (ascending ? massive[i - 1] > massive[i] : massive[i - 1] < massive[i]) // если предыдущий элемент больше текущего,
                    {            // меняем их местами
                        double t = massive[i];
                        massive[i] = massive[i - 1];
                        massive[i - 1] = t;
                        flag = 1;    // перемещения в этом цикле были
                    }
                }
                left++; // сдвигаем левую границу на следующий элемент
                if (flag == 0) break;
            }

            return (counterIteration, massive);
        }
        #endregion

        #region Болотная сортировка

        public static (int, int[]) BozoSort(int[] originalMassive, bool ascending = true)
        {
            Random random = new Random();
            int counterIteration = 0;
            int counterIterationBorder = GetBorderForBogo();
            int[] massive = new int[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            while (!Sorted(massive, ascending))
            {
                if (counterIterationBorder <= counterIteration)
                {
                    break;
                }

                for (int counterI = 0; counterI <= massive.Length - 1; ++counterI)
                {
                    ++counterIteration;

                    int numberOneIndex = random.Next(0, massive.Length);
                    int numberTwoIndex = random.Next(0, massive.Length);
                    int numberOne = massive[numberOneIndex];
                    int numbertwo = massive[numberTwoIndex];
                    massive[numberOneIndex] = numbertwo;
                    massive[numberTwoIndex] = numberOne;
                }
            }
            return (counterIteration, massive);
        }

        public static (int, double[]) BozoSort(double[] originalMassive, bool ascending = true)
        {
            Random random = new Random();
            int counterIteration = 0;
            int counterIterationBorder = GetBorderForBogo();
            double[] massive = new double[originalMassive.Length];

            originalMassive.CopyTo(massive, 0);

            while (!Sorted(massive, ascending))
            {
                if (counterIterationBorder <= counterIteration)
                {
                    break;
                }

                for (int counterI = 0; counterI <= massive.Length - 1; ++counterI)
                {
                    ++counterIteration;

                    int numberOneIndex = random.Next(0, massive.Length);
                    int numberTwoIndex = random.Next(0, massive.Length);
                    double numberOne = massive[numberOneIndex];
                    double numbertwo = massive[numberTwoIndex];
                    massive[numberOneIndex] = numbertwo;
                    massive[numberTwoIndex] = numberOne;
                }
            }
            return (counterIteration, massive);
        }

        public static int GetBorderForBogo()
        {
            BogoSortBorder bogoSortBorder = new BogoSortBorder();

            if (bogoSortBorder.ShowDialog() == true)
            {
                return bogoSortBorder.border;
            }
            else
            {
                return 10000;
            }
        }
        #endregion

        #region Какие-то инстурменты

        private static bool Sorted(int[] massive, bool ascending = true)
        {
            bool sorted = true;

            for (int counterI = 0; counterI < massive.Length - 1; ++counterI)
            {
                if (ascending ? massive[counterI] > massive[counterI + 1] : massive[counterI] < massive[counterI + 1])
                {
                    sorted = false;
                    break;
                }
            }
            return sorted;
        }

        private static bool Sorted(double[] massive, bool ascending = true)
        {
            bool sorted = true;

            for (int counterI = 0; counterI < massive.Length - 1; ++counterI)
            {
                if (ascending ? massive[counterI] > massive[counterI + 1] : massive[counterI] < massive[counterI + 1])
                {
                    sorted = false;
                }
            }
            return sorted;
        }
    }

    public static class MassiveTools
    {
        public static int[] Replace(this int[] massive, int numberOneIndex, int numberTwoIndex)
        {
            int numberOne = massive[numberOneIndex];
            int numberTwo = massive[numberTwoIndex];

            massive[numberOneIndex] = numberTwo;
            massive[numberTwoIndex] = numberOne;

            return massive;
        }

        public static double[] Replace(this double[] massive, int numberOneIndex, int numberTwoIndex)
        {
            double numberOne = massive[numberOneIndex];
            double numberTwo = massive[numberTwoIndex];

            massive[numberOneIndex] = numberTwo;
            massive[numberTwoIndex] = numberOne;

            return massive;
        }
        #endregion
    }
}
