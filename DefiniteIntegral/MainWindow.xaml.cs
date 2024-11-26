using DefiniteIntegral.Methods;
using System.Diagnostics.Metrics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DefiniteIntegral
{
    public partial class MainWindow : Window
    {
        ParserSolver ParserSolver = new ParserSolver();
        List<IntegralMethodItem> ItegralMethodsItem = new List<IntegralMethodItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            for (int counterI = 0; counterI < ItegralMethodsItem.Count; ++counterI)
            {
                switch (ItegralMethodsItem[counterI].Name)
                {
                    case "Метод прямоугольников":
                        {
                            var item = ItegralMethodsItem.Find(x => x.Name == "Метод прямоугольников");

                            double result = ParserSolver.RectangleMethodOnly(this);

                            dgResultTests.Items.Remove(item);

                            var newItem = new IntegralMethodItem
                            {
                                Name = "Метод прямоугольников",
                                Result = result
                            };

                            ItegralMethodsItem[counterI] = newItem;
                            dgResultTests.Items.Add(newItem);

                            break;
                        }
                    case "Метод трапеций":
                        {
                            var item = ItegralMethodsItem.Find(x => x.Name == "Метод трапеций");

                            double result = ParserSolver.TrapezoidMethodOnly(this);

                            dgResultTests.Items.Remove(item);

                            var newItem = new IntegralMethodItem
                            {
                                Name = "Метод трапеций",
                                Result = result
                            };

                            ItegralMethodsItem[counterI] = newItem;
                            dgResultTests.Items.Add(newItem);
                            break;
                        }
                    case "Метод Симпсона":
                        {
                            var item = ItegralMethodsItem.Find(x => x.Name == "Метод Симпсона");

                            double result = ParserSolver.SimpsonMethodOnly(this);

                            dgResultTests.Items.Remove(item);

                            var newItem = new IntegralMethodItem
                            {
                                Name = "Метод Симпсона",
                                Result = result
                            };

                            ItegralMethodsItem[counterI] = newItem;
                            dgResultTests.Items.Add(newItem);
                            break;
                        }

                }
            }
        }

        private void TextMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void WindowClosing(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void AddOrDeleteMethodFromTable(object sender, RoutedEventArgs e)
        {
            var checkBox = ((CheckBox)sender);

            if (checkBox.IsChecked == true && ItegralMethodsItem.Find(x => x.Name == checkBox.Content) == null)
            {
                var newItem = new IntegralMethodItem
                {
                    Name = checkBox.Content.ToString(),
                    Result = 0
                };

                ItegralMethodsItem.Add(newItem);
                dgResultTests.Items.Add(newItem);
            }
            else if (checkBox.IsChecked == false && ItegralMethodsItem.Find(x => x.Name == checkBox.Content) != null)
            {
                var itemRemove = ItegralMethodsItem.Find(x => x.Name == checkBox.Content);
                
                dgResultTests.Items.Remove(itemRemove);
                ItegralMethodsItem.Remove(itemRemove);
            }
        }

        private void ButtonVisualization_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                IntegralMethodItem item = button.DataContext as IntegralMethodItem;
                if (item != null)
                {
                    if (item.Result != 0)
                    {
                        var interval = SafeInput.SafeInput.GetSafeInterval(this);
                        WindowVisualization readFromFileWindow = new WindowVisualization(item.Name, tbFunction.Text, tbn.Text, interval.Item1, interval.Item2);

                        if (readFromFileWindow.ShowDialog() == true)
                        {

                        }
                    }
                }
            }
        }
    }

    public class IntegralMethodItem
    {
        public string Name { get; set; }
        public double Result { get; set; }
    }
}
