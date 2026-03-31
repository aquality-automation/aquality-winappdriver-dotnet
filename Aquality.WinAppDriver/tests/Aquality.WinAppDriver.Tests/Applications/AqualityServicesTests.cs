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
        private readonly CalculatorForm calculatorForm = new();

        [Test]
        public void Should_WorkWithCalculator()
        {
            AqualityServices.Application.Driver.FindElement(calculatorForm.OneButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorForm.PlusButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorForm.TwoButton.Locator).Click();
            AqualityServices.Application.Driver.FindElement(calculatorForm.EqualsButton.Locator).Click();
            var result = AqualityServices.Application.Driver.FindElement(calculatorForm.ResultsLabel.Locator).Text;
            Assert.That(result, Does.Contain("3"));
        }

        [Test]
        public void Should_WorkWithCalculator_ViaElementFinder()
        {
            AqualityServices.Get<IElementFinder>().FindElement(calculatorForm.OneButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorForm.PlusButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorForm.TwoButton.Locator).Click();
            AqualityServices.Get<IElementFinder>().FindElement(calculatorForm.EqualsButton.Locator).Click();
            var result = AqualityServices.Get<IElementFinder>().FindElement(calculatorForm.ResultsLabel.Locator).Text;
            Assert.That(result, Does.Contain("3"));
        }

        [Test]
        public void Should_GetCurrentApplicationFactory_AfterSetDefaultFactory()
        {
            var firstFactory = AqualityServices.Get<IApplicationFactory>();
            AqualityServices.SetDefaultFactory();
            var secondFactory = AqualityServices.Get<IApplicationFactory>();
            Assert.That(firstFactory, Is.Not.SameAs(secondFactory));
            AqualityServices.ApplicationFactory = firstFactory;
            Assert.That(firstFactory, Is.SameAs(AqualityServices.Get<IApplicationFactory>()));
            Assert.That(secondFactory, Is.Not.SameAs(AqualityServices.Get<IApplicationFactory>()));
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
                Assert.That(firstApplication, Is.Not.SameAs(secondApplication));
                secondApplication.Driver.Quit();
            }

            // Switching back to a first instance of Application
            AqualityServices.Application = firstApplication as IWindowsApplication;

            using (var scope = AqualityServices.Get<IServiceProvider>().CreateScope())
            {
                Assert.That(firstApplication, Is.SameAs(scope.ServiceProvider.GetRequiredService<IWindowsApplication>().Launch()));
            }
        }

        [Test]
        public void Should_GetCurrentApplication_AfterQuit()
        {
            var firstApplication = AqualityServices.Application;
            firstApplication.Launch().Quit();
            var secondApplication = AqualityServices.Application.Launch();
            Assert.That(firstApplication, Is.Not.SameAs(secondApplication));
            using var scope = AqualityServices.Get<IServiceProvider>().CreateScope();
            var secondApplicationFromServiceProvider = scope.ServiceProvider.GetRequiredService<IApplication>();
            Assert.That(firstApplication, Is.Not.SameAs(secondApplicationFromServiceProvider));
            Assert.That(secondApplication, Is.SameAs(secondApplicationFromServiceProvider));
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
            Assert.That(AqualityServices.IsApplicationStarted, Is.False, "Application is not started");
        }

        [Test]
        public void Should_BeAbleCheck_IsApplicationStarted()
        {
            AqualityServices.Application.Driver.Manage();
            Assert.That(AqualityServices.IsApplicationStarted, Is.True, "Application is started");
        }
    }
}
