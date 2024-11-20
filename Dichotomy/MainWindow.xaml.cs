using DichotomyMethod;
using System.Windows;
using System.Windows.Input;

namespace Dichotomy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graph Graph = new Graph();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            Graph.GetFunction(this, tbFunction.Text);
        }

        private void buttonDichotomy_Solve_Click(object sender, RoutedEventArgs e)
        {
            Graph.DichotomyMethod(this);
        }

        private void WindowClosing(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void TextMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
