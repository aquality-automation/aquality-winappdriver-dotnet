using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Actions;
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
        private readonly IKeyboardActions keyboardActions;
        private readonly IMouseActions mouseActions;

        public LocalApplicationFactory(AppiumLocalService driverService, IDriverSettings driverSettings, ITimeoutConfiguration timeoutConfiguration, LocalizationLogger localizationLogger, IKeyboardActions keyboardActions, IMouseActions mouseActions) 
            : base(localizationLogger)
        {
            this.driverService = driverService;
            this.driverSettings = driverSettings;
            this.timeoutConfiguration = timeoutConfiguration;
            this.localizationLogger = localizationLogger;
            this.keyboardActions = keyboardActions;
            this.mouseActions = mouseActions;
        }

        public override Application Application
        {
            get
            {
                driverService.Start();
                var serviceUrl = driverService.ServiceUrl;
                localizationLogger.Info("loc.application.driver.service.local.start", serviceUrl);
                var driver = GetDriver(serviceUrl, driverSettings.AppiumOptions, timeoutConfiguration.Command);
                return new Application(driver, timeoutConfiguration, localizationLogger, keyboardActions, mouseActions);
            }
        }
    }
}
