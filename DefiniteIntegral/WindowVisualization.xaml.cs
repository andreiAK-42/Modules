
using DefiniteIntegral.Methods;
using System.Diagnostics.Metrics;
using System.Windows;

namespace DefiniteIntegral
{
    /// <summary>
    /// Логика взаимодействия для WindowVisualization.xaml
    /// </summary>
    public partial class WindowVisualization : Window
    {

        public WindowVisualization(string methodName, string function, string n, double a, double b)
        {
            InitializeComponent();

            ParserSolver parserSolver = new ParserSolver();

            parserSolver.GetFunction(this, function.Replace(",", "."), (int)a, (int)b);

            switch (methodName)
            {
                case "Метод прямоугольников":
                    {
                        parserSolver.RectangleMethod(this, function, n, a, b);
                        break;
                    }
                case "Метод трапеций":
                    {
                        parserSolver.TrapezoidMethod(this, function, n, a, b);
                        break;
                    }
                case "Метод Симпсона":
                    {
                        parserSolver.SimpsonMethod(this, function, n, a, b);
                        break;
                    }

            }
        }

        private void WindowClosing(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
