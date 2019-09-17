using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class CalculatorTest : TestWithApplication
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
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.PlusButton.Locator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.EqualsButton.Locator).Click();
            var result = ApplicationManager.GetRequiredService<IElementFinder>().FindElement(calculatorWindow.ResultsLabel.Locator).Text;
            StringAssert.Contains("3", result);
        }
    }
}
