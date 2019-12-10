using Aquality.WinAppDriver.Configurations;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public class WindowHandleApplicationFactory : ApplicationFactory
    {
        private const string WindowHandleCapability = "appTopLevelWindow";
        private readonly Uri driverServerUri;
        private readonly Func<string> getWindowHandleFunction;
        private readonly string appName;
        private readonly bool isRemote;

        public WindowHandleApplicationFactory(Uri driverServerUri, IServiceProvider serviceProvider, Func<string> getWindowHandleFunction, string appName)
            : base(serviceProvider)
        {
            this.driverServerUri = driverServerUri;
            this.getWindowHandleFunction = getWindowHandleFunction;
            this.appName = appName;
            isRemote = serviceProvider.GetRequiredService<IApplicationProfile>().IsRemote;
        }

        public override Application Application
        {
            get
            {
                LocalizedLogger.Info(isRemote ? "loc.application.driver.service.remote" : "loc.application.driver.service.local.start", driverServerUri);
                return GetApplication(driverServerUri);
            }
        }

        protected override WindowsDriver<WindowsElement> GetApplicationSession(Uri driverServerUri)
        {
            var options = DriverSettings.AppiumOptions;
            LocalizedLogger.Info("loc.application.start", appName);
            options.AddAdditionalCapability(WindowHandleCapability, getWindowHandleFunction());
            return CreateSession(driverServerUri, options);
        }

        protected override WindowsDriver<WindowsElement> CreateSession(Uri driverServerUri, AppiumOptions appliumOptions)
        {
            var session = base.CreateSession(driverServerUri, appliumOptions);
            if (isRemote)
            {
                session.FileDetector = new LocalFileDetector();
            }
            return session;
        }
    }
}
