using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Sorting
{
    public partial class ReadFromFileWindow : Window
    {
        public int[] MassiveInteger;
        public double[] MassiveDouble;
        public byte SortType;
        VisualManager VisualManager;

        public ReadFromFileWindow()
        {
            InitializeComponent();
            VisualManager = new VisualManager(this);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            Bitmap icon7 = (Bitmap)resources.GetObject("shutdown.Image");
            shutdown.Source = Imaging.CreateBitmapSourceFromHBitmap(icon7.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon7.Width, icon7.Height));

        }

        private void MouseDown_SelectFile(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Текстовые документы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                this.tbFilePath.Text = openFileDialog.FileName;
            }
        }

        private void MouseDown_CreateMassive(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (rbInteger.IsChecked == true)
                {
                    string fileText = File.ReadAllText(this.tbFilePath.Text);

                    string[] massiveStringNumbers = fileText.Split(tbSplitSymbol.Text);
                    int[] massiveNumberInteger = new int[massiveStringNumbers.Length];

                    for (int counterI = 0; counterI <= massiveStringNumbers.Length - 1; ++counterI)
                    {
                        massiveNumberInteger[counterI] = int.Parse(massiveStringNumbers[counterI]);
                    }

                    MassiveInteger = massiveNumberInteger;
                    SortType = 0;
                }
                else
                {
                    string fileText = File.ReadAllText(this.tbFilePath.Text);

                    string[] massiveStringNumbers = fileText.Split(tbSplitSymbol.Text);
                    double[] massiveNumberDouble = new double[massiveStringNumbers.Length];

                    for (int counterI = 0; counterI <= massiveStringNumbers.Length - 1; ++counterI)
                    {
                        massiveNumberDouble[counterI] = double.Parse(massiveStringNumbers[counterI]);
                    }
                    MassiveDouble = massiveNumberDouble;
                    SortType = 1;
                }

                this.DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при создании массива. Проверьте файл на другие символы.");
            }
        }

        #region Визуал
        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.EnterBrush;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.LeaveBrush;
        }

        private void HeaderClick(object sender, MouseButtonEventArgs e)
        {
            VisualManager.MoveWindow();
        }

        private void ShutDown(object sender, MouseButtonEventArgs e)
        {
            VisualManager.CloseWindow(false);
        }
        #endregion
    }
}
