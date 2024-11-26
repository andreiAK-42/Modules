using System.Windows;

namespace DefiniteIntegral.SafeInput
{
    public class SafeInput
    {
        public static (int, int) GetSafeInterval(MainWindow window)
        {
            if (window.tba.Text == "a" || window.tbb.Text == "b")
            {
                return (-10, 10);
            }
            else
            {
                if (int.TryParse(window.tba.Text, out int a) && (int.TryParse(window.tbb.Text, out int b)))
                {
                    if (a < b)
                    {
                        return (int.Parse(window.tba.Text), int.Parse(window.tbb.Text));
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
            MessageBox.Show(message, "Метод дихотомии", MessageBoxButton.OK, messageBoxImage);
        }
    }
}
