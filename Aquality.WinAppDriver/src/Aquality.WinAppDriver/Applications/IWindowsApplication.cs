using Aquality.Selenium.Core.Applications;
using Aquality.WinAppDriver.Actions;
using OpenQA.Selenium.Appium.Windows;

namespace Aquality.WinAppDriver.Applications
{
    /// <summary>
    /// Provides functionality to work with Windows application via WinAppDriver.  
    /// </summary>
    public interface IWindowsApplication : IApplication
    {
        /// <summary>
        /// Provides instance of Windows Driver for current application.
        /// </summary>
        new WindowsDriver<WindowsElement> Driver { get; }

        /// <summary>
        /// Provides instance of Windows Driver for desktop session.
        /// </summary>
        WindowsDriver<WindowsElement> RootSession { get; }

        /// <summary>
        /// Provides methods representing basic keyboard actions.
        /// </summary>
        IKeyboardActions KeyboardActions { get; }

        /// <summary>
        /// Provides methods representing basic mouse actions.
        /// </summary>
        IMouseActions MouseActions { get; }

        /// <summary>
        /// Launches an instance of the current application.
        /// </summary>
        /// <returns>Current application instance.</returns>
        IWindowsApplication Launch();

        /// <summary>
        /// Closes current application and desktop sessions.
        /// </summary>
        void Quit();
    }
}
