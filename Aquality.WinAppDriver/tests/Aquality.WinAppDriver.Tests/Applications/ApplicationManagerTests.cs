using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Tests.Windows;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class ApplicationManagerTests : TestWithApplication
    {
        private readonly CalculatorWindow calculatorWindow = new CalculatorWindow();

        [Test]
        public void Should_WorkWithCalculator()
        {
            ApplicationManager.Application.Driver.FindElement(calculatorWindow.OneButton.Locator).Click();
            ApplicationManager.Application.Driver.FindElement(calculatorWindow.PlusButton.Locator).Click();
            ApplicationManager.Application.Driver.FindElement(calculatorWindow.TwoButton.Locator).Click();
            ApplicationManager.Application.Driver.FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = ApplicationManager.Application.Driver.FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_WorkWithCalculator_ViaElementFinder()
        {
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.OneButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.PlusButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.TwoButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_GetCurrentApplicationFactory_AfterSetDefaultFactory()
        {
            var firstFactory = ApplicationManager.GetRequiredService<IApplicationFactory>();
            ApplicationManager.SetDefaultFactory();
            var secondFactory = ApplicationManager.GetRequiredService<IApplicationFactory>();
            Assert.AreNotSame(firstFactory, secondFactory);
            ApplicationManager.ApplicationFactory = firstFactory;
            Assert.AreSame(firstFactory, ApplicationManager.GetRequiredService<IApplicationFactory>());
            Assert.AreNotSame(secondFactory, ApplicationManager.GetRequiredService<IApplicationFactory>());
        }

        [Test]
        public void Should_GetCurrentApplication_AfterSetApplication()
        {
            IApplication firstApplication;
            using(var scope = ApplicationManager.ServiceProvider.CreateScope())
            {
                firstApplication = scope.ServiceProvider.GetRequiredService<IApplication>();
            }

            // Creating a second instance of Application
            ApplicationManager.Application = ApplicationManager.ApplicationFactory.Application;

            using (var scope = ApplicationManager.ServiceProvider.CreateScope())
            {
                var secondApplication = scope.ServiceProvider.GetRequiredService<IApplication>();
                Assert.AreNotSame(firstApplication, secondApplication);
                secondApplication.Driver.Quit();
            }

            // Switching back to a first instance of Application
            ApplicationManager.Application = firstApplication as Application;

            using (var scope = ApplicationManager.ServiceProvider.CreateScope())
            {
                Assert.AreSame(firstApplication, scope.ServiceProvider.GetRequiredService<IApplication>());
            }
        }

        [Test]
        public void Should_GetCurrentApplication_AfterQuit()
        {
            var firstApplication = ApplicationManager.Application;
            firstApplication.Quit();
            var secondApplication = ApplicationManager.Application;
            Assert.AreNotSame(firstApplication, secondApplication);
            using (var scope = ApplicationManager.ServiceProvider.CreateScope())
            {
                var secondApplicationFromServiceProvider = scope.ServiceProvider.GetRequiredService<IApplication>();
                Assert.AreNotSame(firstApplication, secondApplicationFromServiceProvider);
                Assert.AreSame(secondApplication, secondApplicationFromServiceProvider);
            }
        }
    }
}
