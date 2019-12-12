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
        private const string AppNameCapability = "app";
        private readonly Uri driverServerUri;
        private readonly Func<WindowsDriver<WindowsElement>, string> getWindowHandleFunction;
        private readonly bool isRemote;

        public WindowHandleApplicationFactory(Uri driverServerUri, IServiceProvider serviceProvider, Func<WindowsDriver<WindowsElement>, string> getWindowHandleFunction)
            : base(serviceProvider)
        {
            this.driverServerUri = driverServerUri;
            this.getWindowHandleFunction = getWindowHandleFunction;
            isRemote = serviceProvider.GetRequiredService<IApplicationProfile>().IsRemote;
        }

        public override Application Application
        {
            get
            {
                var messageKey = isRemote ? "loc.application.driver.service.remote" : "loc.application.driver.service.local.start";
                LocalizedLogger.Info(messageKey, driverServerUri);
                return GetApplication(driverServerUri);
            }
        }

        protected override WindowsDriver<WindowsElement> GetApplicationSession(Uri driverServerUri)
        {
            var options = DriverSettings.AppiumOptions;
            options.AddAdditionalCapability(AppNameCapability, null);
            options.AddAdditionalCapability(WindowHandleCapability, getWindowHandleFunction(GetRootSession(driverServerUri)));
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
