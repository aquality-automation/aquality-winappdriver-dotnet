using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class TestWindow(By locator, string name) : Window(locator, name), ITestForm
    {
        public new string ElementType => base.ElementType;
    }
}
