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
        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        private IElement NumberPad => GetButton(Factory, CalculatorWindow.WindowLocator, "Number pad");

        [Test]
        public void Should_WorkWithCalculator_ViaElementFactory()
        {
            GetButton(Factory, CalculatorWindow.OneButton, "1").Click();
            GetButton(Factory, CalculatorWindow.PlusButton, "+").Click();
            GetButton(Factory, CalculatorWindow.TwoButton, "2").Click();
            GetButton(Factory, CalculatorWindow.EqualsButton, "=").Click();
            var result = GetButton(Factory, CalculatorWindow.ResultsLabel, "Results bar").Text;
            StringAssert.Contains("3", result);
        }

        [Test]
        public void Should_FindChildElements_ViaElementFactory()
        {
            Assert.IsNotNull(Factory.FindChildElement<Button>(NumberPad, CalculatorWindow.OneButton).GetElement(TimeSpan.Zero));
        }

        [Test]
        public void Should_FindElements_ViaElementFactory()
        {
            Assert.IsTrue(Factory.FindElements<Button>(By.XPath("//*")).Count > 1);
        }

        public static IButton GetButton(IElementFactory coreFactory, By loc, string nam)
        {
            return coreFactory.GetButton(loc, nam);
        }
    }
}
