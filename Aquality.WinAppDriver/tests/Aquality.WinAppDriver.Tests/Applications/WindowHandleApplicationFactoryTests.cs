using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Configurations;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class WindowHandleApplicationFactoryTests : TestWithCustomApplication
    {
        protected override string ApplicationPath => AqualityServices.Get<IDriverSettings>().ApplicationPath;

        [Test]
        public void Should_BePossibleTo_SetWindowHandleApplicationFactory()
        {
            const string appName = "Day Maxi Calc  v.1.5 Freeware";
            ProcessManager.Start(ApplicationPath);

            AqualityServices.SetWindowHandleApplicationFactory(rootSession => GetWindowHandle(rootSession, appName));
            Assert.IsTrue(new CalculatorForm().IsDisplayed);
        }

        /// <summary>
        /// returns window handle attribute, converted to HEX format
        /// </summary>
        /// <returns></returns>
        private string GetWindowHandle(WindowsDriver<WindowsElement> webDriver, string appName)
        {
            AqualityServices.ConditionalWait.WaitForTrue(() =>
            {
                try
                {
                    return webDriver.FindElementByName(appName) != null;
                }
                catch
                {
                    return false;
                }
            });

            var nativeWindowHandle = webDriver.FindElementByName(appName).GetAttribute("NativeWindowHandle");
            return int.Parse(nativeWindowHandle).ToString("x");
        }
    }
}
