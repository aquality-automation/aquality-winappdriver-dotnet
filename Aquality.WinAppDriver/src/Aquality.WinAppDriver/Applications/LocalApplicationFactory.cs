using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Configurations;
using OpenQA.Selenium.Appium.Service;

namespace Aquality.WinAppDriver.Applications
{
    public class LocalApplicationFactory : ApplicationFactory
    {
        private readonly AppiumLocalService driverService;
        private readonly IDriverSettings driverSettings;
        private readonly ITimeoutConfiguration timeoutConfiguration;
        private readonly LocalizationLogger localizationLogger;

        public LocalApplicationFactory(AppiumLocalService driverService, IDriverSettings driverSettings, ITimeoutConfiguration timeoutConfiguration, LocalizationLogger localizationLogger) : base(localizationLogger)
        {
            this.driverService = driverService;
            this.driverSettings = driverSettings;
            this.timeoutConfiguration = timeoutConfiguration;
            this.localizationLogger = localizationLogger;
        }

        public override Application Application
        {
            get
            {
                driverService.Start();
                var serviceUrl = driverService.ServiceUrl;
                localizationLogger.Info("loc.application.driver.service.local", serviceUrl);
                var driver = GetDriver(serviceUrl, driverSettings.AppiumOptions, timeoutConfiguration.Command);
                return new Application(driver, timeoutConfiguration, localizationLogger);
            }
        }
    }
}
