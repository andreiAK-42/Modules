using Interface;
using System.Drawing;

namespace GoldenRatio
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
            return "Метод золотого сечения";
        }

        public Bitmap GetIcon()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            return (Bitmap)resources.GetObject("icon.Image");
        }
    }
}
