using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace Sorting
{
    public class VisualManager
    {
        public static SolidColorBrush EnterBrush = new SolidColorBrush(Color.FromArgb(100, 210, 208, 208));
        public static SolidColorBrush LeaveBrush = new SolidColorBrush(Color.FromArgb(100, 234, 234, 234));

        public static void MouseEnter(object sender, MouseEventArgs e) 
        {
            ((Border) sender).Background = EnterBrush;
        }

        public static void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border) sender).Background = LeaveBrush;
        }
    }
}
