using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Sorting
{
    public partial class CreateRandomMassiveWindow : Window
    {
        public int[] MassiveInteger;
        public double[] MassiveDouble;
        public byte SortType;

        public CreateRandomMassiveWindow()
        {
            InitializeComponent();
        }

        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.EnterBrush;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.LeaveBrush;
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
                        massiveDouble[counterI] = new Random().NextDouble() * (maxValue + minValue) - minValue;
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
    }
}
