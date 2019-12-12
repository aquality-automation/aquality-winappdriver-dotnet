using Aquality.Selenium.Core.Applications;
using OpenQA.Selenium.Appium.Windows;

namespace Aquality.WinAppDriver.Applications
{
    public interface IWindowsApplication : IApplication
    {
        /// <summary>
        /// Provides instance of Windows Driver for current application
        /// </summary>
        new WindowsDriver<WindowsElement> Driver { get; }

        /// <summary>
        /// Provides instance of Windows Driver for desktop session
        /// </summary>
        WindowsDriver<WindowsElement> RootSession { get; }
    }
}
