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
        private static readonly CalculatorWindow CalculatorWindow = new CalculatorWindow();
        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        [Test]
        public void Should_WorkWithCalculator_ViaElementFactory()
        {
            CalculatorWindow.ButtonOne.Click();
            CalculatorWindow.ButtonPlus.Click();
            CalculatorWindow.ButtonTwo.Click();
            CalculatorWindow.ButtonEquals.Click();
            StringAssert.Contains("3", CalculatorWindow.LabelResults.Text);
        }
        
        [Test]
        public void Should_FindChildElements_ViaElementFactory()
        {
            Assert.IsNotNull(Factory.FindChildElement<Button>(CalculatorWindow.NumberPad, CalculatorWindow.OneButtonLocator).GetElement(TimeSpan.Zero));
        }

        [Test]
        public void Should_FindElements_ViaElementFactory()
        {
            Assert.IsTrue(Factory.FindElements<Button>(By.XPath("//*")).Count > 1);
        }
    }
}
