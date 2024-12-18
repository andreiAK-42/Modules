using System.Windows;

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
