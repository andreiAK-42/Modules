using System.Windows;

namespace Sorting
{
    /// <summary>
    /// Логика взаимодействия для BogoSortBorder.xaml
    /// </summary>
    public partial class BogoSortBorder : Window
    {
        public int border = 10000;

        public BogoSortBorder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}
