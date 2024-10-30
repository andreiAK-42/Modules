using Interface;

namespace CoordinateDescentMethod
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
            return "Метод покоординатного спуска";
        }

        public string GetIcon(string applicationFolder)
        {
            return applicationFolder + @"\Img\Newton.png";
        }
    }
}
