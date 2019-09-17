using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class CalculatorTest : TestWithApplication
    {
        private readonly CalculatorWindow CalculatorWindow = new CalculatorWindow();

        [Test]
        public void Should_WorkWithCalculator()
        {
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.OneButton.Locator).Click();
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.PlusButton.Locator).Click();
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.TwoButton.Locator).Click();
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.EqualsButton.Locator).Click();
            var result = ApplicationManager.Application.Driver.FindElement(CalculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_WorkWithCalculator_ViaElementFinder()
        {
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.OneButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.PlusButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.PlusButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.EqualsButton.Locator).Click();
            var result = ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }
    }
}
