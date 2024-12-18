using Microsoft.Win32;
using SolvingEquations.Windows;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SolvingEquations
{
    public partial class ReadFromFile : Window
    {
        public double[][] Matrix;
        public double[] RightPart;
        VisualManager VisualManager;

        public ReadFromFile()
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
                var lines = File.ReadAllLines(this.tbFilePath.Text);
                Matrix = new double[lines.Length][];

                for (int counterI = 0; counterI < lines.Length; ++counterI)
                {
                    var parts = lines[counterI].Split(";", StringSplitOptions.RemoveEmptyEntries);

                    Matrix[counterI] = parts.Take(parts.Length - 1).Select(x => double.Parse(x)).ToArray();
                }

                var parts2 = lines[0].Split(";", StringSplitOptions.RemoveEmptyEntries);
                RightPart = new double[parts2.Length];

                for (int counterI = 0; counterI < lines.Length; ++counterI)
                {
                    RightPart[counterI] = double.Parse(lines[counterI].Split(";")[parts2.Length - 1]);
                }

                this.DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при создании матрицы. Проверьте файл на другие символы.");
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
