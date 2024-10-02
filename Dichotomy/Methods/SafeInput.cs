using Dichotomy;
using OxyPlot;
using System.Windows;

namespace DichotomyMethod.Methods
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

        public static (int, int) GetSafeIntervalAB(MainWindow window)
        {
            if (window.tba.Text == "Min" || window.tba.Text == "Max")
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

        public static int CheckNumberIntersections(List<DataPoint> Graphic)
        {
            int crossings = 0;

            for (int counterI = 1; counterI < Graphic.Count; ++counterI)
            {
                if ((Graphic[counterI - 1].Y < 0 && Graphic[counterI].Y > 0) || (Graphic[counterI - 1].Y > 0 && Graphic[counterI].Y < 0) || (Graphic[counterI - 1].Y == 0 && Graphic[counterI].Y < 0) || (Graphic[counterI - 1].Y < 0 && Graphic[counterI].Y == 0))
                {
                    crossings++;
                }
            }
            return crossings;
        }

        public static void ShowMessage(string message, MessageBoxImage messageBoxImage)
        {
            MessageBox.Show(message, "Метод дихотомии", MessageBoxButton.OK, messageBoxImage);
        }
    }
}
