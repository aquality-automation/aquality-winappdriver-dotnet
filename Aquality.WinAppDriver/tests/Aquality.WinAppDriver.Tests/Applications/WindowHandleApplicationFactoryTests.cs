using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Configurations;
using Aquality.WinAppDriver.Tests.Windows;
using Aquality.WinAppDriver.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class WindowHandleApplicationFactoryTests : TestWithApplication
    {
        private IProcessManager ProcessManager => ApplicationManager.GetRequiredService<IProcessManager>();
        private string ApplicationPath => ApplicationManager.GetRequiredService<IDriverSettings>().ApplicationPath;

        [Test]
        public void Should_BePossibleTo_SetWindowHandleApplicationFactory()
        {
            const string appName = "Day Maxi Calc  v.1.5 Freeware";
            ProcessManager.Start(ApplicationPath);
            ApplicationManager.SetWindowHandleApplicationFactory(
                rootSession => GetWindowHandle(rootSession.FindElementByName(appName)));
            Assert.IsTrue(new CalculatorWindow().IsDisplayed);
        }

        [TearDown]
        public new void CleanUp()
        {
            base.CleanUp();
            ApplicationManager.SetDefaultFactory();
            var executableName = ApplicationPath.Contains('\\') ? ApplicationPath.Substring(ApplicationPath.LastIndexOf('\\') + 1) : ApplicationPath;
            ProcessManager.TryToStopExecutables(executableName);
        }

        /// <summary>
        /// returns window handle attribute, converted to HEX format
        /// </summary>
        /// <returns></returns>
        public string GetWindowHandle(WindowsElement element)
        {
            var nativeWindowHandle = element.GetAttribute("NativeWindowHandle");
            return int.Parse(nativeWindowHandle).ToString("x");
        }
    }
}
