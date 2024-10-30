using CoordinateDescentMethod.Methods;
using System.Windows.Controls;

namespace CoordinateDescentMethod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        Method Graph { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Graph = new Method();
        }


     /*   private void buttonGetMinimum_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindMinimum(this);
        }

        private void buttonGetMaximum_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindMaximum(this);
        }*/

        private void Solve(object sender, System.Windows.RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
           // Graph.FindRoot(this);
        }

        private void buttonGetMaximum_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void buttonGetMinimum_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
