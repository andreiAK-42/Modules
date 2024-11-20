using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Sorting
{
    public class VisualManager
    {
        Window Window { get; set; }

        public VisualManager() { }
        public VisualManager(Window window)
        {
            this.Window = window;
        }

        public static SolidColorBrush EnterBrush = new SolidColorBrush(Color.FromArgb(100, 210, 208, 208));
        public static SolidColorBrush LeaveBrush = new SolidColorBrush(Color.FromArgb(100, 28, 29, 49));

        public void MoveWindow()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (Window != null)
                {
                    Window.DragMove();
                }
            }
        }

        public void CloseWindow(bool dialogResult)
        {
            if (Window != null)
            {
                Window.DialogResult = dialogResult;
                Window.Close();
            }
        }
    }
}
