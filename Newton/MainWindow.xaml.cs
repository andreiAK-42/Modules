using Newton.Methods;
using System.Windows;
using System.Windows.Controls;

namespace Newton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        Parser Graph { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Graph = new Parser();
        }

        private void Solve(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindRoot(this);
        }

        private void buttonGetMinimum_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindMinMax(this);
        }

        private void buttonGetMaximum_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindMinMax(this);
        }

        private void tba_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
