using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aquality.Selenium.Core.Configurations;

namespace Aquality.WinAppDriver.Configurations
{
    /// <summary>
    /// Provides target application profile.
    /// </summary>
    public class DriverSettings : IDriverSettings
    {
        private const string DriverSettingsPath = ".driverSettings";
        private const string ApplicationPathJPath = DriverSettingsPath + ".applicationPath";
        private const string AppCapabilityKey = "app";

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public DriverSettings(ISettingsFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        protected ISettingsFile SettingsFile { get; }

        protected IDictionary<string, object> Capabilities => SettingsFile.GetValueOrNew<Dictionary<string, object>>($"{DriverSettingsPath}.capabilities");

        /// <summary>
        /// Defines does the current settings have the application path defined
        /// </summary>
        protected bool HasApplicationPath => SettingsFile.IsValuePresent(ApplicationPathJPath);

        public AppiumOptions AppiumOptions
        {
            get
            {
                var options = new AppiumOptions();
                Capabilities.ToList().ForEach(capability => options.AddAdditionalCapability(capability.Key, capability.Value));
                if (HasApplicationPath)
                {
                    options.AddAdditionalCapability(AppCapabilityKey, ApplicationPath);
                }
                return options;
            }
        }

        public string ApplicationPath => Path.GetFullPath(SettingsFile.GetValue<string>(ApplicationPathJPath));
    }
}
