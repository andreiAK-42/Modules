using GoldenRatio.Methods;
using System.Windows;
using System.Windows.Controls;

namespace GoldenRatio
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        Parser Graph { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Graph = new Parser();
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindMinimum(this);
        }

        private void buttonMaximum_Solve_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
            Graph.FindMaximum(this);
        }
    }
}
