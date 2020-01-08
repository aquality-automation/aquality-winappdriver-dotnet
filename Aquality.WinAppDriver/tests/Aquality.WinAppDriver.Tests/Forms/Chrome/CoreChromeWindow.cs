using System;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class CoreChromeWindow : Window
    {
        public CoreChromeWindow(WindowsDriver<WindowsElement> rootSession) : base(By.ClassName("Chrome_WidgetWin_1"), nameof(CoreChromeWindow))
        {
            WindowsDriverSupplier = () => rootSession;
        }

        public string NativeWindowHandle => int.Parse(GetElement().GetAttribute("NativeWindowHandle")).ToString("x");

        protected override Func<WindowsDriver<WindowsElement>> WindowsDriverSupplier { get; }
    }
}
