using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementFactoryTests : TestWithApplication
    {
        private static readonly CalculatorForm calculatorForm = new CalculatorForm();
        private IElementFactory Factory => AqualityServices.Get<IElementFactory>();

        [Test]
        public void Should_WorkWithCalculator_ViaElementFactory()
        {
            calculatorForm.OneButton.Click();
            calculatorForm.PlusButton.Click();
            calculatorForm.TwoButton.Click();
            calculatorForm.EqualsButton.Click();
            StringAssert.Contains("3", calculatorForm.ResultsLabel.Text);
        }
        
        [Test]
        public void Should_FindChildElements_ViaElementFactory()
        {
            Assert.IsNotNull(Factory.FindChildElement<Button>(calculatorForm.NumberPad, calculatorForm.OneButton.Locator).GetElement(TimeSpan.Zero));
        }

        [Test]
        public void Should_FindElements_ViaElementFactory()
        {
            Assert.IsTrue(Factory.FindElements<Button>(By.XPath("//*")).Count > 1);
        }
    }
}
