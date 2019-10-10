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
        private static readonly string[] SupportedLanguages = { "be", EnglishLanguageCode, "ru" };
        private static readonly Assembly LibraryAssembly = Assembly.GetAssembly(typeof(ApplicationManager));
        private static readonly IList<KeyValuePair<string, string>> LocalizationFileEnglishDictionary = GetLocalizationDictionaryAsList(EnglishLanguageCode);
        private static readonly IEnumerable<string> KeysWithoutParams = LocalizationFileEnglishDictionary.Where(pair => !pair.Value.Contains("{0}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithOneParameter = LocalizationFileEnglishDictionary.Where(pair => pair.Value.Contains("{0}") && !pair.Value.Contains("{1}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithTwoAndMoreParameters = LocalizationFileEnglishDictionary.Where(pair => pair.Value.Contains("{1}")).Select(pair => pair.Key);
        private static readonly IEnumerable<string> KeysWithParameters = LocalizationFileEnglishDictionary.Where(pair => pair.Value.Contains("{0}")).Select(pair => pair.Key);

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
            var paramsArray = new[] { "a", "b" , "c", "d" };
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

        [Test]
        public void Should_HaveSameAmountOfValues([ValueSource(nameof(SupportedLanguages))] string language)
        {
            Assert.AreEqual(LocalizationFileEnglishDictionary.Count, GetLocalizationDictionaryAsList(language).Count);
        }

        [Test]
        public void Should_HaveNotAllValuesTheSame_InDifferentLanguages([ValueSource(nameof(SupportedLanguages))] string language)
        {
            var currentLanguageDictionary = GetLocalizationDictionaryAsList(language);

            foreach (var dictionary in SupportedLanguages.Except(new[] { language }).Select(lang => GetLocalizationDictionaryAsList(lang)))
            {
                CollectionAssert.AreNotEquivalent(currentLanguageDictionary, dictionary);
            }
        }

        private static IList<KeyValuePair<string, string>> GetLocalizationDictionaryAsList(string language)
        {
            return new JsonSettingsFile($"Resources.Localization.{language}.json", LibraryAssembly).GetValue<Dictionary<string, string>>("$").ToList();
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
