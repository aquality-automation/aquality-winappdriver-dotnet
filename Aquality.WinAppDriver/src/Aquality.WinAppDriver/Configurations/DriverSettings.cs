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

        private readonly ISettingsFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public DriverSettings(ISettingsFile settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        protected virtual IDictionary<string, object> Capabilities => settingsFile.GetValueOrNew<Dictionary<string, object>>($"{DriverSettingsPath}.capabilities");
        
        /// <summary>
        /// Defines does the current settings have the application path defined
        /// </summary>
        protected virtual bool HasApplicationPath => settingsFile.IsValuePresent(ApplicationPathJPath) || Capabilities.ContainsKey(AppCapabilityKey);


        public virtual AppiumOptions AppiumOptions
        {
            get
            {
                var options = new AppiumOptions();
                Capabilities.ToList().ForEach(capability => options.AddAdditionalCapability(capability.Key, capability.Value));
                if (HasApplicationPath && ApplicationPath != null)
                {
                    options.AddAdditionalCapability(AppCapabilityKey, ApplicationPath);
                }
                return options;
            }
        }

        public virtual string ApplicationPath
        {
            get
            {
                var appValue = settingsFile.GetValueOrDefault(ApplicationPathJPath,
                    defaultValue: (Capabilities.ContainsKey(AppCapabilityKey) ? Capabilities[AppCapabilityKey] : null)?.ToString());
                return appValue?.StartsWith(".") == true ? Path.GetFullPath(appValue) : appValue;
            }
        }
    }
}
