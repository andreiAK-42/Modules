using Interface;

namespace Dichotomy
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
            return "Метод дихотомии";
        }

        public string GetIcon(string applicationFolder)
        {
            return applicationFolder + @"\Img\DichotomyIcon.png";
        }
    }
}

