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
        private const string EnglishLanguageCode = "en";
        private static readonly string[] SupportedLanguages = ["be", EnglishLanguageCode, "ru"];
        private static readonly Assembly LibraryAssembly = Assembly.GetAssembly(typeof(AqualityServices));
        private static readonly List<KeyValuePair<string, string>> LocalizationFileEnglishDictionary = GetLocalizationDictionaryAsList(EnglishLanguageCode);
        private static readonly IEnumerable<string> KeysWithoutParams = LocalizationFileEnglishDictionary.Where(pair => !pair.Value.Contains("{0}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithOneParameter = LocalizationFileEnglishDictionary.Where(pair => pair.Value.Contains("{0}") && !pair.Value.Contains("{1}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithTwoAndMoreParameters = LocalizationFileEnglishDictionary.Where(pair => pair.Value.Contains("{1}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithParameters = LocalizationFileEnglishDictionary.Where(pair => pair.Value.Contains("{0}")).Select(pair => pair.Key);

        private static ILocalizationManager LocalizationManager => AqualityServices.Get<ILocalizationManager>();

        [Test]
        public void Should_BePossibleTo_UseLocalizationManager_OnCustomLanguage_ForClicking()
        {
            Assert.That(LocalizationManager.GetLocalizedMessage("loc.clicking"), Is.EqualTo("Націскаем"));
        }

        [Test]
        public void Should_BePossibleTo_UseLocalizationManager_ForUnknownKey()
        {
            var unknownKey = "loc.unknown.fake.key";
            Assert.That(LocalizationManager.GetLocalizedMessage(unknownKey), Is.EqualTo(unknownKey));
        }

        [Test]
        public void Should_ReturnNonKeyValues_AndNotEmptyValues_ForKeysWithoutParams([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithoutParams))] string key)
        {
            var localizedValue = GetLocalizationManager(language).GetLocalizedMessage(key);
            Assert.That(localizedValue, Is.Not.EqualTo(key), "Value should be defined in resource files");
            Assert.That(localizedValue, Is.Not.Empty, "Value should not be empty");
        }

        [Test]
        public void Should_ReturnNonKeyValues_AndNotEmptyValues_ForKeysWithOneParameter([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithOneParameter))] string key)
        {
            var paramsArray = new[] { "a" };
            var localizedValue = GetLocalizationManager(language).GetLocalizedMessage(key, paramsArray);
            Assert.That(localizedValue, Is.Not.EqualTo(key), "Value should be defined in resource files");
            Assert.That(localizedValue, Is.Not.Empty, "Value should not be empty");
            Assert.That(localizedValue.Contains(paramsArray[0]), Is.True, "Value should contain at least first parameter");
        }

        [Test]
        public void Should_ReturnNonKeyValues_AndNotEmptyValues_ForKeysWithTwoAndMoreParameters([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithTwoAndMoreParameters))] string key)
        {
            var paramsArray = new[] { "a", "b" , "c", "d" };
            var localizedValue = GetLocalizationManager(language).GetLocalizedMessage(key, paramsArray);
            Assert.That(localizedValue, Is.Not.EqualTo(key), "Value should be defined in resource files");
            Assert.That(localizedValue, Is.Not.Empty, "Value should not be empty");
            Assert.That(localizedValue.Contains(paramsArray[0]), Is.True, "Value should contain at least first parameter");
            Assert.That(localizedValue.Contains(paramsArray[1]), Is.True, "Value should contain at least first parameter");
        }

        [Test]
        public void Should_ThrowsFormatException_WhenKeysRequireParams([ValueSource(nameof(SupportedLanguages))] string language, [ValueSource(nameof(KeysWithParameters))] string key)
        {
            Assert.Throws<FormatException>(() => GetLocalizationManager(language).GetLocalizedMessage(key));
        }

        [Test]
        public void Should_HaveSameAmountOfValues([ValueSource(nameof(SupportedLanguages))] string language)
        {
            Assert.That(GetLocalizationDictionaryAsList(language).Count, Is.EqualTo(LocalizationFileEnglishDictionary.Count));
        }

        [Test]
        public void Should_HaveNotAllValuesTheSame_InDifferentLanguages([ValueSource(nameof(SupportedLanguages))] string language)
        {
            var currentLanguageDictionary = GetLocalizationDictionaryAsList(language);

            foreach (var dictionary in SupportedLanguages.Except([language]).Select(GetLocalizationDictionaryAsList))
            {
                Assert.That(currentLanguageDictionary, Is.Not.EquivalentTo(dictionary));
            }
        }

        private static List<KeyValuePair<string, string>> GetLocalizationDictionaryAsList(string language)
        {
            return [.. new JsonSettingsFile($"Resources.Localization.{language}.json", LibraryAssembly).GetValue<Dictionary<string, string>>("$")];
        }

        private static LocalizationManager GetLocalizationManager(string customLanguage)
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

            public bool LogPageSource => false;
        }
    }
}
