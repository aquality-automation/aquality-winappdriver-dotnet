using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Logging;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aquality.WinAppDriver.Tests.Localization
{
    public class LocalizationFilesTest
    {
        private static readonly string[] SupportedLanguages = { "be", "en", "ru" };
        private static readonly Assembly LibraryAssembly = Assembly.GetAssembly(typeof(ApplicationManager));
        private static readonly IList<KeyValuePair<string, string>> LocalizationFileDictionary 
            = new JsonFile("Resources.Localization.en.json", LibraryAssembly).GetValue<Dictionary<string, string>>("$").ToList();
        private static readonly IEnumerable<string> KeysWithoutParams = LocalizationFileDictionary.Where(pair => !pair.Value.Contains("{0}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithOneParameter = LocalizationFileDictionary.Where(pair => pair.Value.Contains("{0}") && !pair.Value.Contains("{1}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithTwoAndMoreParameters = LocalizationFileDictionary.Where(pair => pair.Value.Contains("{1}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithParameters = LocalizationFileDictionary.Where(pair => pair.Value.Contains("{0}")).Select(pair => pair.Key);

        private LocalizationManager LocalizationManager => ApplicationManager.GetRequiredService<LocalizationManager>();

        [Test]
        public void Should_BePossibleTo_UseLocalizationManager_OnCustomLanguage_ForClicking()
        {
            Assert.AreEqual("Націскаем", LocalizationManager.GetLocalizedMessage("loc.clicking"));
        }

        [Test]
        public void Should_BePossibleTo_UseLocalizationManager_ForUnknownKey()
        {
            var unknownKey = "loc.unknown.fake.key";
            Assert.AreEqual(unknownKey, LocalizationManager.GetLocalizedMessage(unknownKey));
        }

        [Test]
        public void Should_ReturnNonKeyValues_AndNotEmptyValues_ForKeysWithoutParams([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithoutParams))] string key)
        {
            var localizedValue = GetLocalizationManager(language).GetLocalizedMessage(key);
            Assert.AreNotEqual(key, localizedValue, "Value should be defined in resource files");
            Assert.IsNotEmpty(localizedValue, "Value should not be empty");
        }

        [Test]
        public void Should_ReturnNonKeyValues_AndNotEmptyValues_ForKeysWithOneParameter([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithOneParameter))] string key)
        {
            var paramsArray = new[] { "a" };
            var localizedValue = GetLocalizationManager(language).GetLocalizedMessage(key, paramsArray);
            Assert.AreNotEqual(key, localizedValue, "Value should be defined in resource files");
            Assert.IsNotEmpty(localizedValue, "Value should not be empty");
            Assert.IsTrue(localizedValue.Contains(paramsArray[0]), "Value should contain at least first parameter");
        }

        [Test]
        public void Should_ReturnNonKeyValues_AndNotEmptyValues_ForKeysWithTwoAndMoreParameters([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithTwoAndMoreParameters))] string key)
        {
            var paramsArray = new[] { "a", "b" };
            var localizedValue = GetLocalizationManager(language).GetLocalizedMessage(key, paramsArray);
            Assert.AreNotEqual(key, localizedValue, "Value should be defined in resource files");
            Assert.IsNotEmpty(localizedValue, "Value should not be empty");
            Assert.IsTrue(localizedValue.Contains(paramsArray[0]), "Value should contain at least first parameter");
            Assert.IsTrue(localizedValue.Contains(paramsArray[1]), "Value should contain at least first parameter");
        }

        [Test]
        public void Should_ThrowsFormatException_WhenKeysRequireParams([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithParameters))] string key)
        {
            Assert.Throws<FormatException>(() => GetLocalizationManager(language).GetLocalizedMessage(key));
        }

        private LocalizationManager GetLocalizationManager(string customLanguage)
        {
            var configuration = new DynamicConfiguration
            {
                Language = customLanguage
            };

            return new LocalizationManager(configuration, Logger.Instance, LibraryAssembly);
        }

        private class DynamicConfiguration : ILoggerConfiguration
        {
            public string Language { get; set; }
        }
    }
}
