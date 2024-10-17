using System.Windows;

namespace Newton.Methods
{
    public class SafeInput
    {
        public static (int, int) GetSafeInterval(MainWindow window)
        {
            if (window.tbXStart.Text == "Min" || window.tbXEnd.Text == "Max")
            {
                return (-10, 10);
            }
            else
            {
                if (int.TryParse(window.tbXStart.Text, out int a) && (int.TryParse(window.tbXEnd.Text, out int b)))
                {
                    if (a < b)
                    {
                        return (int.Parse(window.tbXStart.Text), int.Parse(window.tbXEnd.Text));
                    }
                    else
                    {
                        ShowMessage("A должно быть меньше B. Выбраны значениения по умолчанию -10 ... 10", MessageBoxImage.Error);
                        return (-10, 10);
                    }
                }
                else
                {
                    ShowMessage("Невозможно прочитать интервал. Возможно не целочисленные единицы. Выбраны значениения по умолчанию -10 ... 10", MessageBoxImage.Error);
                    return (-10, 10);
                }
            }
        }

        public static (double, double) GetSafeIntervalAB(MainWindow window)
        {
            if (window.tba.Text == "Min" || window.tbb.Text == "Max")
            {
                return (-10, 10);
            }
            else
            {
                if (double.TryParse(window.tba.Text, out double a) && (double.TryParse(window.tbb.Text, out double b)))
                {
                    if (a < b)
                    {
                        return (double.Parse(window.tba.Text), double.Parse(window.tbb.Text));
                    }
                    else
                    {
                        ShowMessage("A должно быть меньше B. Выбраны значениения по умолчанию -10 ... 10", MessageBoxImage.Error);
                        return (-10, 10);
                    }
                }
                else
                {
                    ShowMessage("Невозможно прочитать интервал. Возможно не целочисленные единицы. Выбраны значениения по умолчанию -10 ... 10", MessageBoxImage.Error);
                    return (-10, 10);
                }
            }
        }

        public static void ShowMessage(string message, MessageBoxImage messageBoxImage)
        {
            MessageBox.Show(message, "Метод золотого сечения", MessageBoxButton.OK, messageBoxImage);
        }
    }
}
