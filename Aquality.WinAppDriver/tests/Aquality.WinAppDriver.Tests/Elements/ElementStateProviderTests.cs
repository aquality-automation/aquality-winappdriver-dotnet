using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementStateProviderTests : TestWithApplication
    {
        private static readonly By ElementLocator = By.XPath("//*[@id='111111']");
        private static CalculatorWindow calculatorWindow = new CalculatorWindow();
        private static readonly ITextBox RightArgumentTextBox = calculatorWindow.RightArgumentTextBox;

        const string ElementDescription = "Not present element";

        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        private IElement GetLabel() => Factory.GetLabel(ElementLocator, ElementDescription);

        [Test]
        public void Should_ReturnTrue_IfElementIsDisplayed()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotDisplayed()
        {
            //todo find solution for present, but not displayed element
            Assert.IsFalse(GetLabel().State.IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotPresent()
        {
            Assert.IsFalse(GetLabel().State.IsExist);
        }

        [Test]
        public void Should_ReturnTrue_IfElementExists()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsExist);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotClickable()
        {
            //todo find solution for present, but not clickable element
            Assert.IsFalse(GetLabel().State.IsClickable);
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsClickable()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsClickable);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotEnabled()
        {
            Assert.IsFalse(calculatorWindow.EmptyButton.State.IsEnabled);
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsEnabled()
        {
            Assert.IsTrue(RightArgumentTextBox.State.IsEnabled);
        }
    }
}