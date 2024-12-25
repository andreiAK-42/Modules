using Newton.Methods;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Newton
{
    public partial class MWindow : Window
    {
        Parser Graph { get; set; }
        public MWindow()
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
