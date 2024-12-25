using Interface;
using System.Drawing;

namespace MLS
{
    class Module : IModule
    {
        public object Show()
        {
            var userControl = new MainWindow();

            return userControl;
        }

        public void Close()
        {
        }

        public string GetModuleName()
        {
            return "Метод наим. квадратов";
        }

        public Bitmap GetIcon()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            return (Bitmap)resources.GetObject("icon.Image");
        }
    }
}
