using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Tests.Forms;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class AqualityServicesTests : TestWithApplication
    {
        private readonly CalculatorForm calculatorWindow = new CalculatorForm();

        [Test]
        public void Should_WorkWithCalculator()
        {
            AqualityServices.Application.Driver.FindElement(calculatorWindow.OneButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorWindow.PlusButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorWindow.TwoButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = AqualityServices.Application.Driver.FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_WorkWithCalculator_ViaElementFinder()
        {
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.OneButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.PlusButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.TwoButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = AqualityServices.Get<IElementFinder>().FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_GetCurrentApplicationFactory_AfterSetDefaultFactory()
        {
            var firstFactory = AqualityServices.Get<IApplicationFactory>();
            AqualityServices.SetDefaultFactory();
            var secondFactory = AqualityServices.Get<IApplicationFactory>();
            Assert.AreNotSame(firstFactory, secondFactory);
            AqualityServices.ApplicationFactory = firstFactory;
            Assert.AreSame(firstFactory, AqualityServices.Get<IApplicationFactory>());
            Assert.AreNotSame(secondFactory, AqualityServices.Get<IApplicationFactory>());
        }

        [Test]
        public void Should_GetCurrentApplication_AfterSetApplication()
        {
            IApplication firstApplication;
            using (var scope = AqualityServices.Get<IServiceProvider>().CreateScope())
            {
                firstApplication = scope.ServiceProvider.GetRequiredService<IWindowsApplication>().Launch();
            }

            // Creating a second instance of Application
            AqualityServices.Application = AqualityServices.ApplicationFactory.Application;

            using (var scope = AqualityServices.Get<IServiceProvider>().CreateScope())
            {
                var secondApplication = scope.ServiceProvider.GetRequiredService<IWindowsApplication>().Launch();
                Assert.AreNotSame(firstApplication, secondApplication);
                secondApplication.Driver.Quit();
            }

            // Switching back to a first instance of Application
            AqualityServices.Application = firstApplication as IWindowsApplication;

            using (var scope = AqualityServices.Get<IServiceProvider>().CreateScope())
            {
                Assert.AreSame(firstApplication, scope.ServiceProvider.GetRequiredService<IWindowsApplication>().Launch());
            }
        }

        [Test]
        public void Should_GetCurrentApplication_AfterQuit()
        {
            var firstApplication = AqualityServices.Application;
            firstApplication.Launch().Quit();
            var secondApplication = AqualityServices.Application.Launch();
            Assert.AreNotSame(firstApplication, secondApplication);
            using (var scope = AqualityServices.Get<IServiceProvider>().CreateScope())
            {
                var secondApplicationFromServiceProvider = scope.ServiceProvider.GetRequiredService<IApplication>();
                Assert.AreNotSame(firstApplication, secondApplicationFromServiceProvider);
                Assert.AreSame(secondApplication, secondApplicationFromServiceProvider);
            }
        }

        [Test]
        public void Should_BeAbleGetApplication()
        {
            Assert.DoesNotThrow(() => AqualityServices.Application.Driver.Manage());
        }

        [Test]
        [Parallelizable]
        public void Should_BeAbleCheck_IsApplicationNotStarted()
        {
            Assert.IsFalse(AqualityServices.IsApplicationStarted, "Application is not started");
        }

        [Test]
        public void Should_BeAbleCheck_IsApplicationStarted()
        {
            AqualityServices.Application.Driver.Manage();
            Assert.IsTrue(AqualityServices.IsApplicationStarted, "Application is started");
        }

        [Test]
        [Parallelizable]
        public void Should_BeAbleToGet_Logger()
        {
            Assert.DoesNotThrow(() => AqualityServices.Logger.Info("message"), "Logger should not be null");
        }

        [Test]
        [Parallelizable]
        public void Should_BeAbleToGet_ConditionalWait()
        {
            Assert.DoesNotThrow(() => AqualityServices.ConditionalWait.WaitForTrue(() => true), "ConditionalWait should not be null");
        }

        [Test]
        public void Should_BeAbleToGet_KeyboardActions()
        {
            Assert.DoesNotThrow(() => AqualityServices.KeyboardActions.SendKeys(WinAppDriver.Actions.ActionKey.Space), "KeyboardActions should not be null");
        }

        [Test]
        public void Should_BeAbleToGet_MouseActions()
        {
            Assert.DoesNotThrow(() => AqualityServices.MouseActions.MoveByOffset(1,1), "MouseActions should not be null");
        }

        [Test]
        [Parallelizable]
        public void Should_BeAbleToGet_WinAppDriverLauncher()
        {
            Assert.IsNotNull(AqualityServices.WinAppDriverLauncher, "WinAppDriverLauncher should not be null");
        }

        [Test]
        [Parallelizable]
        public void Should_BeAbleToGet_ProcessManager()
        {
            Assert.DoesNotThrow(() => AqualityServices.ProcessManager.IsProcessRunning(string.Empty), "ProcessManager should not be null");
        }
    }
}
