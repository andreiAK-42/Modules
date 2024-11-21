using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sorting
{
    /// <summary>
    /// Логика взаимодействия для BogoSortBorder.xaml
    /// </summary>
    public partial class BogoSortBorder : Window
    {
        public int border = 10000;
        VisualManager visualManager;

        public BogoSortBorder()
        {
            InitializeComponent(); 
            visualManager = new VisualManager(this);
        }


        private void MouseDowmSetBorderButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                border = int.Parse(this.BogoSortBorderNumber.Text);
            }
            catch (Exception ex)
            {

            }

            this.DialogResult = true;
            this.Close();
        }

        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.EnterBrush;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.LeaveBrush;
        }
    }
}
