using Interface;
using System.Drawing;

namespace DefiniteIntegral
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
            return "Определённый интеграл";
        }

        public Bitmap GetIcon()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            return (Bitmap)resources.GetObject("icon.Image");
        }
    }
}
