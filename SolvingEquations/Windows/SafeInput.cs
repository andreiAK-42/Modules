using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SolvingEquations.Methods
{
    public class SafeInput
    {
        public static void ShowMessage(string message, MessageBoxImage messageBoxImage)
        {
            MessageBox.Show(message, "Решение уравнений", MessageBoxButton.OK, messageBoxImage);
        }
    }
}
