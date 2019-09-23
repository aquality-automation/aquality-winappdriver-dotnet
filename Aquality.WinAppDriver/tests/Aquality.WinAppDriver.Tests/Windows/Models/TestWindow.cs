using Aquality.WinAppDriver.Windows;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Windows.Models
{
    public class TestWindow : Window
    {
        public TestWindow(By locator, string name) : base(locator, name)
        {
        }
    }
}
