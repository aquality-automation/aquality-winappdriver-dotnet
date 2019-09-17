using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class CalculatorTest : TestWithApplication
    {
        [Test]
        public void Should_WorkWithCalculator()
        {
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.OneButtonLocator).Click();
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.PlusButtonLocator).Click();
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.TwoButtonLocator).Click();
            ApplicationManager.Application.Driver.FindElement(CalculatorWindow.EqualsButtonLocator).Click();
            var result = ApplicationManager.Application.Driver.FindElement(CalculatorWindow.ResultsLabelLocator).Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_WorkWithCalculator_ViaElementFinder()
        {
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.OneButtonLocator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.PlusButtonLocator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.TwoButtonLocator).Click();
            ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.EqualsButtonLocator).Click();
            var result = ApplicationManager.GetRequiredService<IElementFinder>().FindElement(CalculatorWindow.ResultsLabelLocator).Text;
            StringAssert.Contains("3", result);
        }
    }
}
