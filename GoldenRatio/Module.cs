using Interface;

namespace GoldenRatio
{
    class Module : IModule
    {
        public object Show()
        {
            var userControl = new MainWindow();
            var content = userControl.Content;
            userControl.Content = null;

            return content;
        }

        public void Close()
        {
        }

        public string GetModuleName()
        {
            return "Метод золотого сечения";
        }

        public string GetIcon(string applicationFolder)
        {
            return applicationFolder + @"\Img\GoldenRatioIcon.jpg";
        }
    }
}
