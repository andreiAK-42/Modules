﻿using Interface;
using System.Drawing;

namespace Newton
{
    class Module : IModule
    {
        public object Show()
        {
            var userControl = new MWindow();

            return userControl;
        }

        public void Close()
        {
        }

        public string GetModuleName()
        {
            return "Метод Ньютона";
        }

        public Bitmap GetIcon()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));

            return (Bitmap)resources.GetObject("icon.Image");
        }
    }
}
