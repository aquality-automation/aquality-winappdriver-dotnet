using OpenQA.Selenium.Remote;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public class RemoteApplicationFactory : ApplicationFactory
    {
        private readonly Uri driverServerUri;

        public RemoteApplicationFactory(Uri driverServerUri, IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
            this.driverServerUri = driverServerUri;
        }

        public override Application Application
        {
            get
            {
                LocalizationLogger.Info("loc.application.driver.service.remote", driverServerUri);
                var driver = GetDriver(driverServerUri);
                driver.FileDetector = new LocalFileDetector();
                return new Application(driver, ServiceProvider);
            }
        }
    }
}
