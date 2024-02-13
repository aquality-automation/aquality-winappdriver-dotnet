using Aquality.Selenium.Core.Applications;
using Aquality.WinAppDriver.Actions;
using OpenQA.Selenium.Appium.Windows;
using System.Collections.Generic;

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
        new WindowsDriver Driver { get; }

        /// <summary>
        /// Provides instance of Windows Driver for desktop session.
        /// </summary>
        WindowsDriver RootSession { get; }

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
        /// Executes driver scripts e.g. PowerShell, platform-specific extensions.
        /// <see href="https://github.com/appium/appium-windows-driver?tab=readme-ov-file#power-shell-commands-execution"/>
        /// </summary>
        /// <param name="script">Script for execution or script type, e.g. "powerShell" or "windows: click".</param>
        /// <param name="parameters">Parameters for the script execution. E.g. for PowerShell a "script" or "command" key must be defined.</param>
        /// <param name="inRootSession">Whether to execute the script against the <see cref="RootSession"/> or the application session <see cref="Driver"/></param>
        /// <returns>Script result, if any, or null otherwise.</returns>
        object ExecuteScript(string script, IDictionary<string, object> parameters, bool inRootSession = false);

        /// <summary>
        /// Closes current application and desktop sessions.
        /// </summary>
        void Quit();
    }
}
