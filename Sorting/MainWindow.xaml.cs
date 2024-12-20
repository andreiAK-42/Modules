﻿using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sorting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<SortMethods> SorthMethodsTable = new List<SortMethods>();
        int[] massiveInteger;
        double[] massiveDouble;
        byte sortType = 3;

        private bool _isFullscreen;

        public MainWindow()
        {
            InitializeComponent();
            AddElementsInList();
            SetIamge();
        }


        private void Generate_Random_Massive(object sender, MouseButtonEventArgs e)
        {
            CreateRandomMassiveWindow createRandomMassiveWindow = new CreateRandomMassiveWindow();

            if (createRandomMassiveWindow.ShowDialog() == true)
            {
                sortType = createRandomMassiveWindow.SortType;

                if (sortType == 0)
                {
                    massiveInteger = createRandomMassiveWindow.MassiveInteger;
                    tbMassiveVisualisation.Text = string.Join("; ", massiveInteger);
                    tbMassiveVisualisation.Visibility = Visibility.Visible;
                }
                else
                {
                    massiveDouble = createRandomMassiveWindow.MassiveDouble;
                    tbMassiveVisualisation.Text = string.Join("; ", massiveDouble);
                    tbMassiveVisualisation.Visibility = Visibility.Visible;
                }
            }
        }

        private void MouseDown_ReadFromFile(object sender, MouseButtonEventArgs e)
        {
            ReadFromFileWindow readFromFileWindow = new ReadFromFileWindow();

            if (readFromFileWindow.ShowDialog() == true)
            {
                sortType = readFromFileWindow.SortType;

                if (sortType == 0)
                {
                    massiveInteger = readFromFileWindow.MassiveInteger;
                    tbMassiveVisualisation.Text = string.Join("; ", massiveInteger);
                    tbMassiveVisualisation.Visibility = Visibility.Visible;
                }
                else
                {
                    massiveDouble = readFromFileWindow.MassiveDouble;
                    tbMassiveVisualisation.Text = string.Join("; ", massiveDouble);
                    tbMassiveVisualisation.Visibility = Visibility.Visible;
                }
            }
        }

        private void MouseDown_RunTests(object sender, MouseButtonEventArgs e)
        {
            if (massiveInteger == null && massiveDouble == null)
            {
                MessageBox.Show("Массив не был сгенерирован.\nСначала сгенерируйте массив, потом запускайте тесты!");
                return;
            }

            foreach (var method in SorthMethodsTable)
            {
                bool asceding = bool.Parse(rbSortMinToMax.IsChecked.ToString());
                switch (method.Sort_ID)
                {
                    case 0:
                        {
                            method.TemporaryFile = new TemporaryFile(Environment.CurrentDirectory);
                            dgResultTests.Items.Remove(method);

                            Stopwatch st = new Stopwatch();

                            if (sortType == 0)
                            {
                                st.Start();
                                var result = Methods.BubbleSorting(massiveInteger, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }
                            else
                            {
                                st.Start();
                                var result = Methods.BubbleSorting(massiveDouble, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }

                            method.TotalTimeSort = st.Elapsed.TotalMilliseconds;

                            dgResultTests.Items.Add(method);
                            break;
                        }
                    case 1:
                        {
                            method.TemporaryFile = new TemporaryFile(Environment.CurrentDirectory);
                            dgResultTests.Items.Remove(method);

                            Stopwatch st = new Stopwatch();

                            if (sortType == 0)
                            {
                                st.Start();
                                var result = Methods.InsertionSort(massiveInteger, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }
                            else
                            {
                                st.Start();
                                var result = Methods.InsertionSort(massiveDouble, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }

                            method.TotalTimeSort = st.Elapsed.TotalMilliseconds;

                            dgResultTests.Items.Add(method);
                            break;
                        }
                    case 2:
                        {
                            method.TemporaryFile = new TemporaryFile(Environment.CurrentDirectory);
                            dgResultTests.Items.Remove(method);

                            Stopwatch st = new Stopwatch();

                            if (sortType == 0)
                            {
                                st.Start();
                                var result = Methods.QuickSort(massiveInteger, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }
                            else
                            {
                                st.Start();
                                var result = Methods.QuickSort(massiveDouble, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }

                            method.TotalTimeSort = st.Elapsed.TotalMilliseconds;

                            dgResultTests.Items.Add(method);
                            break;
                        }
                    case 3:
                        {
                            method.TemporaryFile = new TemporaryFile(Environment.CurrentDirectory);
                            dgResultTests.Items.Remove(method);

                            Stopwatch st = new Stopwatch();

                            if (sortType == 0)
                            {
                                st.Start();
                                var result = Methods.ShekerSort(massiveInteger, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }
                            else
                            {
                                st.Start();
                                var result = Methods.ShekerSort(massiveDouble, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }

                            method.TotalTimeSort = st.Elapsed.TotalMilliseconds;

                            dgResultTests.Items.Add(method);
                            break;
                        }
                    case 4:
                        {
                            method.TemporaryFile = new TemporaryFile(Environment.CurrentDirectory);
                            dgResultTests.Items.Remove(method);

                            Stopwatch st = new Stopwatch();

                            if (sortType == 0)
                            {
                                st.Start();
                                var result = Methods.BozoSort(massiveInteger, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }
                            else
                            {
                                st.Start();
                                var result = Methods.BozoSort(massiveDouble, asceding);
                                st.Stop();

                                method.CountIteration = result.Item1;
                                File.WriteAllText(method.TemporaryFile.FilePath, MassiveToString(result.Item2));
                            }

                            method.TotalTimeSort = st.Elapsed.TotalMilliseconds;

                            dgResultTests.Items.Add(method);
                            break;
                        }
                }
            }
        }



        #region Работа с таблицой
        private void Checked_Method(object sender, RoutedEventArgs e)
        {
            AddOrDeleteMethodFromTable();
        }

        private void Unchecked_Method(object sender, RoutedEventArgs e)
        {
            AddOrDeleteMethodFromTable();
        }

        private void AddOrDeleteMethodFromTable()
        {
            foreach (SortItem item in ddlCountry.Items)
            {
                if (item.Check_Status == true && SorthMethodsTable.Find(x => x.Sort_ID == item.Sort_ID) == null)
                {

                    SortMethods sortMethods = new SortMethods();
                    sortMethods.Sort_ID = item.Sort_ID;
                    sortMethods.Name = item.Name;
                    sortMethods.CountIteration = 0;
                    sortMethods.TotalTimeSort = 0;

                    SorthMethodsTable.Add(sortMethods);
                    dgResultTests.Items.Add(sortMethods);
                }
                else if (item.Check_Status == false && SorthMethodsTable.Find(x => x.Sort_ID == item.Sort_ID) != null)
                {
                    var itemRemove = SorthMethodsTable.Find(x => x.Sort_ID == item.Sort_ID);

                    SorthMethodsTable.Remove(itemRemove);
                    dgResultTests.Items.Remove(itemRemove);
                }
            }
        }
        #endregion

        #region Визуал
        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.EnterBrush;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.LeaveBrush;
        }


        private void AddElementsInList()
        {
            SortItem obj = new SortItem();

            obj.Name = "Пузырьковая сортировка";
            obj.Sort_ID = 0;
            ddlCountry.Items.Add(obj);

            obj = new SortItem();
            obj.Name = "Сортировка вставками";
            obj.Sort_ID = 1;
            ddlCountry.Items.Add(obj);

            obj = new SortItem();
            obj.Name = "Быстрая сортировка";
            obj.Sort_ID = 2;
            ddlCountry.Items.Add(obj);

            obj = new SortItem();
            obj.Name = "Шейкерная сортировка";
            obj.Sort_ID = 3;
            ddlCountry.Items.Add(obj);

            obj = new SortItem();
            obj.Name = "Болотная сортировка";
            obj.Sort_ID = 4;
            ddlCountry.Items.Add(obj);
        }

        public void SetIamge()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));
            Bitmap icon = (Bitmap)resources.GetObject("sort_icon.Image");

            imSotringMethods.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon.Width, icon.Height));
            Bitmap icon1 = (Bitmap)resources.GetObject("sort_min_to_max.Image");
            Bitmap icon2 = (Bitmap)resources.GetObject("sort_max_to_min.Image");
            Bitmap icon3 = (Bitmap)resources.GetObject("random.Image");
            Bitmap icon4 = (Bitmap)resources.GetObject("file.Image");
            Bitmap icon5 = (Bitmap)resources.GetObject("excel.Image");
            Bitmap icon6 = (Bitmap)resources.GetObject("data.Image");

            imSortMinToMax.Source = Imaging.CreateBitmapSourceFromHBitmap(icon1.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon1.Width, icon1.Height));
            imSortMaxToMin.Source = Imaging.CreateBitmapSourceFromHBitmap(icon2.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon2.Width, icon2.Height));
            imData.Source = Imaging.CreateBitmapSourceFromHBitmap(icon6.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon6.Width, icon6.Height));

            imGenerateRandomMassive.Source = Imaging.CreateBitmapSourceFromHBitmap(icon3.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon3.Width, icon3.Height));
            imReadFromFile.Source = Imaging.CreateBitmapSourceFromHBitmap(icon4.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon4.Width, icon4.Height));
            imReadFromExcel.Source = Imaging.CreateBitmapSourceFromHBitmap(icon5.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon5.Width, icon5.Height));


            Bitmap icon7 = (Bitmap)resources.GetObject("shutdown.Image");
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(icon7.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon7.Width, icon7.Height)); ;
            BtnCloseWindow.Background = ib;
            
            Bitmap icon8 = (Bitmap)resources.GetObject("maximized.Image");
            ImageBrush ib2 = new ImageBrush();
            ib2.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(icon8.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon8.Width, icon8.Height)); ;

            BtnMaxSizeWindow.Background = ib2;
        }
        #endregion

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                SortMethods item = button.DataContext as SortMethods;
                if (item != null)
                {
                    if (item.TemporaryFile != null && item.TemporaryFile.FilePath != null)
                    {
                        Process.Start("notepad.exe", item.TemporaryFile.FilePath);
                    }
                }
            }
        }

        private void MouseDown_ReadFromExcel(object sender, MouseButtonEventArgs e)
        {
            ReadFromExcelWindow readFromFileWindow = new ReadFromExcelWindow();

            if (readFromFileWindow.ShowDialog() == true)
            {
                sortType = readFromFileWindow.SortType;

                if (sortType == 0)
                {
                    massiveInteger = readFromFileWindow.MassiveInteger;
                    tbMassiveVisualisation.Text = string.Join("; ", massiveInteger);
                    tbMassiveVisualisation.Visibility = Visibility.Visible;
                }
                else
                {
                    massiveDouble = readFromFileWindow.MassiveDouble;
                    tbMassiveVisualisation.Text = string.Join("; ", massiveDouble);
                    tbMassiveVisualisation.Visibility = Visibility.Visible;
                }
            }
        }

        private void WindowClosing(object sender, MouseButtonEventArgs e)
        {
            TemporaryFileCleaner.Clear();
            this.Close();
        }

        private void TextMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void FullscreenButton_Click(object sender, MouseButtonEventArgs e)
        {
            _isFullscreen = !_isFullscreen;
            if (_isFullscreen)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void ButtonVisualization_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                SortMethods item = button.DataContext as SortMethods;
                if (item != null)
                {
                    if (item.TemporaryFile != null && item.TemporaryFile.FilePath != null)
                    {
                        WindowVisualization readFromFileWindow = new WindowVisualization(massiveInteger, item.Sort_ID, bool.Parse(rbSortMinToMax.IsChecked.ToString()));

                        if (readFromFileWindow.ShowDialog() == true)
                        {
                           
                        }
                    }
                }
            }
        }

        private static string MassiveToString(int[] array)
        {
            StringBuilder arrayString = new StringBuilder();
            int columnIndex = 0;

            foreach (int num in array)
            {
                // Выводим число, отформатированное с правым выравниванием
                arrayString.Append(num.ToString().PadRight(3) + "; ");

                // Переход на новую строку после каждой пятой колонки
                columnIndex++;
                if (columnIndex % 10 == 0)
                {
                    arrayString.AppendLine();
                }
            }

            // Если массив не делится на 5, добавляем оставшиеся числа на новую строку
            if (columnIndex % 10 != 0)
            {
                arrayString.AppendLine();
            }

            return arrayString.ToString();
        }

        private static string MassiveToString(double[] array)
        {
            StringBuilder arrayString = new StringBuilder();
            int columnIndex = 0;

            foreach (int num in array)
            {
                // Выводим число, отформатированное с правым выравниванием
                arrayString.Append(num.ToString().PadRight(3) + "; ");

                // Переход на новую строку после каждой пятой колонки
                columnIndex++;
                if (columnIndex % 10 == 0)
                {
                    arrayString.AppendLine();
                }
            }

            // Если массив не делится на 5, добавляем оставшиеся числа на новую строку
            if (columnIndex % 10 != 0)
            {
                arrayString.AppendLine();
            }

            return arrayString.ToString();
        }
    }

    public class SortMethods
    {
        public int Sort_ID { get; set; }
        public string Name { get; set; }
        public int CountIteration { get; set; }
        public double TotalTimeSort { get; set; }
        public TemporaryFile TemporaryFile { get; set; }
    }

    public class SortItem
    {
        public string Name { get; set; }
        public int Sort_ID { get; set; }
        public Boolean Check_Status { get; set; }
    }
}