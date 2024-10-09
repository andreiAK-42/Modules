using Interface;

namespace Newton
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
            return "Метод Ньютона";
        }

        public string GetIcon(string applicationFolder)
        {
            return applicationFolder + @"\Img\Newton.png";
        }
    }
}
