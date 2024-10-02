using DichotomyMethod;
using System.Windows;
using System.Windows.Controls;

namespace Dichotomy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        Graph Graph { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Graph = new Graph();
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
        }

        private void buttonDichotomy_Solve_Click(object sender, RoutedEventArgs e)
        {
            Graph.DichotomyMethod(this);
        }
    }
}
