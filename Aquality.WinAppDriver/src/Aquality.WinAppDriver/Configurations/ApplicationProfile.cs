using Aquality.Selenium.Core.Utilities;
using System;

namespace Aquality.WinAppDriver.Configurations
{
    /// <summary>
    /// Provides application profile.
    /// </summary>
    public class ApplicationProfile : IApplicationProfile
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public ApplicationProfile(JsonFile settingsFile, IDriverSettings driverSettings)
        {
            SettingsFile = settingsFile;
            DriverSettings = driverSettings;
        }

        protected JsonFile SettingsFile { get; }

        public IDriverSettings DriverSettings { get; }

        public bool IsRemote => SettingsFile.GetValue<bool>(".isRemote");

        public Uri RemoteConnectionUrl => new Uri(SettingsFile.GetValue<string>(".remoteConnectionUrl"));
    }
}
