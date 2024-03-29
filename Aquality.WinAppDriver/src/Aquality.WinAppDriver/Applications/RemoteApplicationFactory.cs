﻿using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.WinAppDriver.Applications
{
    public class RemoteApplicationFactory : ApplicationFactory
    {
        private readonly Uri driverServerUri;

        public RemoteApplicationFactory(Uri driverServerUri) : base()
        {
            this.driverServerUri = driverServerUri;
        }

        public override IWindowsApplication Application
        {
            get
            {
                LocalizedLogger.Info("loc.application.driver.service.remote", driverServerUri);
                return GetApplication(driverServerUri);
            }
        }

        protected override WindowsDriver CreateSession(Uri driverServerUri, AppiumOptions appliumOptions)
        {
            var session = base.CreateSession(driverServerUri, appliumOptions);
            session.FileDetector = new LocalFileDetector();
            return session;
        }
    }
}
