using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aquality.Selenium.Core.Configurations;
using OpenQA.Selenium;
using System;

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
        private const string AutomationName = "Windows";

        private readonly ISettingsFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public DriverSettings(ISettingsFile settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        protected virtual IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>();

        protected virtual IDictionary<string, object> Capabilities => settingsFile.GetValueOrNew<Dictionary<string, object>>($"{DriverSettingsPath}.capabilities");
        
        /// <summary>
        /// Defines does the current settings have the application path defined
        /// </summary>
        protected virtual bool HasApplicationPath => settingsFile.IsValuePresent(ApplicationPathJPath) || Capabilities.ContainsKey(AppCapabilityKey);


        public virtual AppiumOptions AppiumOptions
        {
            get
            {
                var options = new AppiumOptions
                {
                    AutomationName = AutomationName
                };
                Capabilities.ToList().ForEach(
                    capability =>
                    {
                        try
                        {
                            options.AddAdditionalAppiumOption(capability.Key, capability.Value);
                        }
                        catch (ArgumentException exception)
                        {
                            if (exception.Message.StartsWith("There is already an option"))
                            {
                                SetKnownProperty(options, capability, exception);
                            }
                            else
                            {
                                if (exception.Message.StartsWith("There is already an option"))
                                {
                                    SetKnownProperty(options, capability, exception);
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                    });
                if (HasApplicationPath && ApplicationPath != null)
                {
                    options.App = ApplicationPath;
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

        private void SetKnownProperty(DriverOptions options, KeyValuePair<string, object> capability, ArgumentException exception)
        {
            if (KnownCapabilitySetters.ContainsKey(capability.Key))
            {
                KnownCapabilitySetters[capability.Key](options, capability.Value);
            }
            else
            {
                SetOptionByPropertyName(options, capability, exception);
            }
        }

        private void SetOptionByPropertyName(DriverOptions options, KeyValuePair<string, object> option, Exception exception)
        {
            var optionProperty = options
                            .GetType()
                            .GetProperties()
                            .FirstOrDefault(property => IsPropertyNameMatchOption(property.Name, option.Key) && property.CanWrite)
                            ?? throw exception;
            var propertyType = optionProperty.PropertyType;
            var valueToSet = IsEnumValue(propertyType, option.Value)
                ? ParseEnumValue(propertyType, option.Value)
                : option.Value;
            optionProperty.SetValue(options, valueToSet);
        }

        private object ParseEnumValue(Type propertyType, object optionValue)
        {
            return optionValue is string
                ? Enum.Parse(propertyType, optionValue.ToString(), ignoreCase: true)
                : Enum.ToObject(propertyType, Convert.ChangeType(optionValue, Enum.GetUnderlyingType(propertyType)));
        }

        private bool IsEnumValue(Type propertyType, object optionValue)
        {
            var valueAsString = optionValue.ToString();
            if (!propertyType.IsEnum || string.IsNullOrEmpty(valueAsString))
            {
                return false;
            }
            var normalizedValue = char.ToUpper(valueAsString[0]) +
                (valueAsString.Length > 1 ? valueAsString.Substring(1) : string.Empty);
            return propertyType.IsEnumDefined(normalizedValue)
                || propertyType.IsEnumDefined(valueAsString)
                || (IsValueOfIntegralNumericType(optionValue)
                    && propertyType.IsEnumDefined(Convert.ChangeType(optionValue, Enum.GetUnderlyingType(propertyType))));
        }

        private bool IsValueOfIntegralNumericType(object value)
        {
            return value is byte || value is sbyte
                || value is ushort || value is short
                || value is uint || value is int
                || value is ulong || value is long;
        }

        private bool IsPropertyNameMatchOption(string propertyName, string optionKey)
        {
            return propertyName.Equals(optionKey, StringComparison.InvariantCultureIgnoreCase)
                || optionKey.ToLowerInvariant().Contains(propertyName.ToLowerInvariant());
        }
    }
}
