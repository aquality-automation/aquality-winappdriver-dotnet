using OpenQA.Selenium.Appium.Service;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public class LocalApplicationFactory : ApplicationFactory
    {
        private readonly AppiumLocalService driverService;

        public LocalApplicationFactory(AppiumLocalService driverService, IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
            this.driverService = driverService;
        }

        public override Application Application
        {
            get
            {
                driverService.Start();
                var serviceUrl = driverService.ServiceUrl;
                LocalizationLogger.Info("loc.application.driver.service.local.start", serviceUrl);
                var driver = GetDriver(serviceUrl);
                return new Application(driver, ServiceProvider);
            }
        }
    }
}
