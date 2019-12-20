using OpenQA.Selenium.Appium.Service;

namespace Aquality.WinAppDriver.Applications
{
    public class LocalApplicationFactory : ApplicationFactory
    {
        private readonly AppiumLocalService driverService;

        public LocalApplicationFactory(AppiumLocalService driverService) : base()
        {
            this.driverService = driverService;
        }

        public override Application Application
        {
            get
            {
                driverService.Start();
                var serviceUrl = driverService.ServiceUrl;
                LocalizedLogger.Info("loc.application.driver.service.local.start", serviceUrl);
                return new Application(() => GetApplicationSession(serviceUrl), () => GetRootSession(serviceUrl));
            }
        }
    }
}
