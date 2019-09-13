using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Configurations
{
    /// <summary>
    /// Describes WinAppDriver settings.
    /// </summary>
    public interface IDriverSettings
    {
        /// <summary>
        /// Gets desired WinAppDriver options
        /// </summary>
        AppiumOptions AppiumOptions { get; }

        /// <summary>
        /// Provides a path to the application
        /// </summary>
        string ApplicationPath { get; }
    }
}
