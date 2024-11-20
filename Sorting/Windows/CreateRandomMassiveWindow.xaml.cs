using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Sorting
{
    public partial class CreateRandomMassiveWindow : Window
    {
        VisualManager visualManager;
        public int[] MassiveInteger;
        public double[] MassiveDouble;
        public byte SortType;

        public CreateRandomMassiveWindow()
        {
            InitializeComponent();
            visualManager = new VisualManager(this);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            Bitmap icon7 = (Bitmap)resources.GetObject("shutdown.Image");
            shutdown.Source = Imaging.CreateBitmapSourceFromHBitmap(icon7.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon7.Width, icon7.Height));
        }

        private void MouseDowmCreateButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (rbInteger.IsChecked == true)
                {
                    int[] massiveInteger = new int[int.Parse(tbMassiveSize.Text)];

                    int minValue = int.Parse(tbMinValue.Text);
                    int maxValue = int.Parse(tbMaxValue.Text);

                    for (int counterI = 0; counterI <= int.Parse(tbMassiveSize.Text) - 1; ++counterI)
                    {
                        massiveInteger[counterI] = new Random().Next(minValue, maxValue);
                    }
                    MassiveInteger = massiveInteger;
                    SortType = 0;
                }
                else
                {
                    double[] massiveDouble = new double[int.Parse(tbMassiveSize.Text)];

                    double minValue = double.Parse(tbMinValue.Text);
                    double maxValue = double.Parse(tbMaxValue.Text);

                    for (int counterI = 0; counterI <= int.Parse(tbMassiveSize.Text) - 1; ++counterI)
                    {
                        massiveDouble[counterI] = GenerateRandomDouble(minValue, maxValue, 2);
                    }
                    MassiveDouble = massiveDouble;
                    SortType = 1;
                }

                this.DialogResult = true;
                this.Close();
            }
            catch 
            {
                MessageBox.Show("Возникла ошибка при создании массива. Проверьте исходные данные.");
            }
        }

        private void HeaderClick(object sender, MouseButtonEventArgs e)
        {
            visualManager.MoveWindow();
        }

        private void ShutDown(object sender, MouseButtonEventArgs e)
        {
            visualManager.CloseWindow(false);
        }

        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.EnterBrush;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.LeaveBrush;
        }

        public static double GenerateRandomDouble(double min, double max, int decimalPlaces = 2)
        {
            Random random = new Random();
            double randomValue = random.NextDouble() * (max - min) + min;
            return Math.Round(randomValue, decimalPlaces);
        }
    }
}
