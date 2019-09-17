using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementFactoryTests : TestWithApplication
    {
        private static readonly CalculatorWindow сalculatorWindow = new CalculatorWindow();
        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        [Test]
        public void Should_WorkWithCalculator_ViaElementFactory()
        {
            сalculatorWindow.OneButton.Click();
            сalculatorWindow.PlusButton.Click();
            сalculatorWindow.TwoButton.Click();
            сalculatorWindow.EqualsButton.Click();
            StringAssert.Contains("3", сalculatorWindow.ResultsLabel.Text);
        }
        
        [Test]
        public void Should_FindChildElements_ViaElementFactory()
        {
            Assert.IsNotNull(Factory.FindChildElement<Button>(сalculatorWindow.NumberPad, сalculatorWindow.OneButton.Locator).GetElement(TimeSpan.Zero));
        }

        [Test]
        public void Should_FindElements_ViaElementFactory()
        {
            Assert.IsTrue(Factory.FindElements<Button>(By.XPath("//*")).Count > 1);
        }
    }
}
