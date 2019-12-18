using System;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Aquality.WinAppDriver.Applications;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Unit.Configuration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class CustomConfigurationTests
    {
        private const string SpecialLoggerLanguage = "SpecialLoggerLanguage";
        private const string SpecialCustomValue = "SpecialCustomValue";
        private static readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

        [SetUp]
        public static void SetUp()
        {
            AqualityServices.SetStartup(new CustomStartup());
        }

        [Test]
        public void Should_BeAbleOverrideDependencies_AndGetCustomService()
        {
            Assert.AreEqual(SpecialLoggerLanguage, AqualityServices.Get<ILoggerConfiguration>().Language, "Configuration value should be overriden.");
        }

        [Test]
        public void Should_BeAbleAdd_CustomService()
        {
            Assert.AreEqual(SpecialCustomValue, AqualityServices.Get<ICustomService>().CustomValue, "Custom service should have value");
        }

        [Test]
        public void Should_BeAbleGet_DefaultService()
        {
            Assert.AreEqual(DefaultCommandTimeout, AqualityServices.Get<ITimeoutConfiguration>().Command, "Default service value should have default value");
        }

        [TearDown]
        public static void TearDown()
        {
            AqualityServices.SetStartup(new ApplicationStartup());
        }

        private class CustomLoggerConfiguration : ILoggerConfiguration
        {
            public string Language { get; } = SpecialLoggerLanguage;
        }

        private interface ICustomService
        {
            string CustomValue { get; }
        }

        private class CustomService : ICustomService
        {
            public string CustomValue { get; } = SpecialCustomValue;
        }

        private class CustomStartup : ApplicationStartup
        {
            public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider, ISettingsFile settings = null)
            {
                settings = GetSettings();
                base.ConfigureServices(services, applicationProvider, settings);
                services.AddSingleton<ILoggerConfiguration>(new CustomLoggerConfiguration());
                services.AddTransient<ICustomService, CustomService>();
                return services;
            }
        }
    }
}