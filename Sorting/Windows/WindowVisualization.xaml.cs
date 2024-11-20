using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Sorting
{
    public partial class WindowVisualization : Window
    {
        private int[] _data;
        private bool ascending;

        public WindowVisualization(int[] massive, int sortType, bool asceding)
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            Bitmap icon7 = (Bitmap)resources.GetObject("shutdown.Image");
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(icon7.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon7.Width, icon7.Height)); ;
            BtnCloseWindow.Background = ib;

            if (massive.Length > 20)
            {
                _data = massive.Take(20).ToArray();
            }
            else
            {
                _data = new int[massive.Length];
                massive.CopyTo(_data, 0);
            }
            this.ascending = asceding;

            InitializeHistogram();

            switch (sortType)
            {
                case 0:
                    {
                        BubbleSorting();
                        break;
                    }
                case 1:
                    {
                        InsertionSort();
                        break;
                    }
                case 2:
                    {
                        QuickSort(ascending);
                        break;
                    }
                case 3:
                    {
                        ShekerSort();
                        break;
                    }
                case 4:
                    {
                        BozoSort();
                        break;
                    }
            }
        }

        private void InitializeHistogram()
        {
            HistogramCanvas.Children.Clear();
            for (int i = 0; i < _data.Length; i++)
            {
                double height = Math.Abs(_data[i]) * 10;

                if (_data[i] == 0)
                {
                    height = 10;
                }

                var rect = new Rectangle
                {
                    Width = 20,
                    Height = height,
                    Fill = Brushes.White,
                    Margin = new Thickness(i * 25, 100, 0, 0),
                };

                if (_data[i] > 0)
                {
                    Canvas.SetTop(rect, HistogramCanvas.ActualHeight - height);
                }

                HistogramCanvas.Children.Add(rect);
            }
        }

        public async void BozoSort()
        {
            Random random = new Random();

            while (!Sorted(_data, ascending))
            {
                for (int counterI = 0; counterI <= _data.Length - 1; ++counterI)
                {
                    int numberOneIndex = random.Next(0, _data.Length);
                    int numberTwoIndex = random.Next(0, _data.Length);

                    var currentRect = (Rectangle)HistogramCanvas.Children[numberOneIndex];
                    currentRect.Fill = Brushes.Blue;
                    var comparingRect = (Rectangle)HistogramCanvas.Children[numberTwoIndex];
                    comparingRect.Fill = Brushes.Red;

                    await Task.Delay(350);

                    _data.Replace(numberOneIndex, numberTwoIndex);

                    SwapRectangles(comparingRect, currentRect);
                    UpdateHistogram();
                }
            }
        }

        public void QuickSort(bool ascending)
        {
            Sort(_data, 0, _data.Length - 1, ascending);
        }

        public async void Sort(int[] array, int left, int right, bool ascending)
        {
            if (left < right)
            {
                int pivotIndex = await Partition(array, left, right, ascending);

                Sort(array, left, pivotIndex - 1, ascending);
                Sort(array, pivotIndex + 1, right, ascending);
            }
        }

        public async Task<int> Partition(int[] array, int left, int right, bool ascending)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (ascending ? array[j] <= pivot : array[j] >= pivot)
                {
                    i++;

                    var currentRect = (Rectangle)HistogramCanvas.Children[i];
                    currentRect.Fill = Brushes.Blue;
                    var comparingRect = (Rectangle)HistogramCanvas.Children[j];
                    comparingRect.Fill = Brushes.Red;
                    await Task.Delay(350);
                    array.Replace(i, j);

                    SwapRectangles(comparingRect, currentRect);
                    UpdateHistogram();

                }
            }

            var currentRect2 = (Rectangle)HistogramCanvas.Children[i + 1];
            currentRect2.Fill = Brushes.Blue;
            var comparingRect2 = (Rectangle)HistogramCanvas.Children[right];
            comparingRect2.Fill = Brushes.Red;
            await Task.Delay(350);
            array.Replace(i + 1, right);

            SwapRectangles(comparingRect2, currentRect2);
            UpdateHistogram();

            return i + 1;
        }

        private async void InsertionSort()
        {
            while (!Sorted(_data, ascending))
            {
                for (int counterI = 1; counterI < _data.Length; ++counterI)
                {

                    int k = _data[counterI];
                    int j = counterI - 1;

                    while (j >= 0 && (ascending ? _data[j] > k : _data[j] < k))
                    {
                        UpdateHistogram();
                        var currentRect = (Rectangle)HistogramCanvas.Children[j];
                        currentRect.Fill = Brushes.Blue;
                        var comparingRect = (Rectangle)HistogramCanvas.Children[j + 1];
                        comparingRect.Fill = Brushes.Red;

                        _data[j + 1] = _data[j];
                        _data[j] = k;
                        await Task.Delay(250);
                        SwapRectangles(comparingRect, currentRect);
                        UpdateHistogram();
                        --j;
                    }

                }
            }
        }

        private async void ShekerSort()
        {
            int left = 0, right = _data.Length - 1; // левая и правая границы сортируемой области массива
            int flag = 1;

            var currentRect = (Rectangle)HistogramCanvas.Children[0];
            var comparingRect = (Rectangle)HistogramCanvas.Children[1];
            currentRect.Fill = Brushes.Blue;
            comparingRect.Fill = Brushes.Red;

            while ((left < right) && flag > 0)
            {
                flag = 0;
                await Task.Delay(250);
                for (int counterI = left; counterI < right; ++counterI)  //двигаемся слева направо
                {
                    currentRect = (Rectangle)HistogramCanvas.Children[counterI];
                    comparingRect = (Rectangle)HistogramCanvas.Children[counterI + 1];
                    currentRect.Fill = Brushes.Blue;
                    comparingRect.Fill = Brushes.Red;

                    await Task.Delay(250);
                    if (ascending ? _data[counterI] > _data[counterI + 1] : _data[counterI] < _data[counterI + 1]) // если следующий элемент меньше текущего, меняем местами
                    {
                        _data.Replace(counterI, counterI + 1);
                        SwapRectangles(currentRect, comparingRect);
                        UpdateHistogram();
                        flag = 1;      // перемещения в этом цикле были
                    }
                }

                await Task.Delay(250);
                right--; // сдвигаем правую границу на предыдущий элемент
                UpdateHistogram();

                for (int counterI = right; counterI > left; --counterI)  //двигаемся справа налево
                {
                    currentRect = (Rectangle)HistogramCanvas.Children[counterI - 1];
                    comparingRect = (Rectangle)HistogramCanvas.Children[counterI];
                    currentRect.Fill = Brushes.Red;
                    comparingRect.Fill = Brushes.Blue;
                    await Task.Delay(250);
                    if (ascending ? _data[counterI - 1] > _data[counterI] : _data[counterI - 1] < _data[counterI]) // если предыдущий элемент больше текущего,
                    { 
                        _data.Replace(counterI, counterI - 1);
                        SwapRectangles(currentRect, comparingRect);
                        UpdateHistogram();
                        flag = 1;    // перемещения в этом цикле были
                    }
                }
                UpdateHistogram();
                left++; // сдвигаем левую границу на следующий элемент
                if (flag == 0) break;
            }
        }

        private async void BubbleSorting()
        {
            while (!Sorted(_data, ascending))
            {
                await Task.Delay(250);

                var currentRect = (Rectangle)HistogramCanvas.Children[0];
                var comparingRect = (Rectangle)HistogramCanvas.Children[1];

                currentRect.Fill = Brushes.Blue;

                for (int counterI = 0; counterI < _data.Length - 1; ++counterI)
                {
                    currentRect = (Rectangle)HistogramCanvas.Children[counterI];
                    comparingRect = (Rectangle)HistogramCanvas.Children[counterI + 1];
                    currentRect.Fill = Brushes.Blue;
                    comparingRect.Fill = Brushes.Red;
                    await Task.Delay(250);

                    if (ascending ? _data[counterI] > _data[counterI + 1] : _data[counterI] < _data[counterI + 1])
                    {
                        _data.Replace(counterI, counterI + 1);
                        SwapRectangles(currentRect, comparingRect);
                        UpdateHistogram();
                    }
                   ((Rectangle)HistogramCanvas.Children[counterI]).Fill = Brushes.White;
                }
                UpdateHistogram();
            }
        }

        private void SwapRectangles(Rectangle rect1, Rectangle rect2)
        {
            double top1 = Canvas.GetTop(rect1);
            double top2 = Canvas.GetTop(rect2);


            Canvas.SetTop(rect1, top2);
            Canvas.SetTop(rect2, top1);
        }

        private void UpdateHistogram()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                var rect = (Rectangle)HistogramCanvas.Children[i];
                rect.Height = Math.Abs(_data[i]) * 10;
                rect.Fill = Brushes.White;
            }
        }

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

        private void HeaderClick(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void WindowClosing(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
