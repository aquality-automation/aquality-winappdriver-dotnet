using System;
using OpenQA.Selenium.Appium.Service;

namespace Aquality.WinAppDriver.Configurations
{
    /// <summary>
    /// Describes application settings.
    /// </summary>
    public interface IApplicationProfile
    {
        /// <summary>
        /// Is remote WinAppDriver service or not: true to use <see cref="RemoteConnectionUrl"/> and false to create default <see cref="AppiumLocalService"/>.
        /// </summary>
        bool IsRemote { get; }
        
        /// <summary>
        /// Gets remote connection URI is case of remote browser.
        /// </summary>
        Uri RemoteConnectionUrl { get; }

        /// <summary>
        /// Gets WinAppDriver settings for application.
        /// </summary>
        IDriverSettings DriverSettings { get; }
    }
}
