using Aquality.WinAppDriver.Configurations;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public class WindowHandleApplicationFactory : ApplicationFactory
    {
        private const string WindowHandleCapability = "appTopLevelWindow";
        private const string AppNameCapability = "app";
        private readonly Uri driverServerUri;
        private readonly Func<WindowsDriver, string> getWindowHandleFunction;
        private readonly bool isRemote;

        public WindowHandleApplicationFactory(Uri driverServerUri, Func<WindowsDriver, string> getWindowHandleFunction)
            : base()
        {
            this.driverServerUri = driverServerUri;
            this.getWindowHandleFunction = getWindowHandleFunction;
            isRemote = AqualityServices.Get<IApplicationProfile>().IsRemote;
        }

        public override IWindowsApplication Application
        {
            get
            {
                var messageKey = isRemote ? "loc.application.driver.service.remote" : "loc.application.driver.service.local.start";
                LocalizedLogger.Info(messageKey, driverServerUri);
                return GetApplication(driverServerUri);
            }
        }

        protected override WindowsDriver GetApplicationSession(Uri driverServerUri)
        {
            var options = DriverSettings.AppiumOptions;
            options.AddAdditionalAppiumOption(AppNameCapability, null);
            options.AddAdditionalAppiumOption(WindowHandleCapability, getWindowHandleFunction(GetRootSession(driverServerUri)));
            return CreateSession(driverServerUri, options);
        }

        protected override WindowsDriver CreateSession(Uri driverServerUri, AppiumOptions appliumOptions)
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
