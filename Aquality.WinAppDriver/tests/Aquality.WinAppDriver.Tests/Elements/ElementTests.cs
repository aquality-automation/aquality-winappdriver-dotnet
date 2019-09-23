using System;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Windows;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementTests : TestWithApplication
    {
        private readonly ITextBox rightArgumentTextBox = new CalculatorWindow().RightArgumentTextBox;
        private const string ExpectedValue = "2";
        private static readonly By ElementLocator = By.XPath("//*[@id='111111']");
        private const string ElementDescription = "Not present element";

        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();
        private IElement Label => Factory.GetLabel(ElementLocator, ElementDescription);

        [Test]
        public void Should_SendKeys()
        {
            rightArgumentTextBox.SendKeys(ExpectedValue);
            Assert.AreEqual(ExpectedValue, rightArgumentTextBox.Value);
        }

        [Test]
        public void Should_ThrowException_InSendKeys_WhenNullIsSend()
        {
            Assert.Throws<ArgumentNullException>(() => rightArgumentTextBox.SendKeys(null));
        }

        [Test]
        public void Should_GetNullValue_InSendKeys_WhenEmptyStringIsEntered()
        {
            rightArgumentTextBox.SendKeys(string.Empty);
            Assert.IsNull(rightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SetCorrectLocatorInConstructor()
        {
            Assert.AreEqual(ElementLocator, Label.Locator);
        }

        [Test]
        public void Should_SetCorrectNameInConstructor()
        {
            Assert.AreEqual(ElementDescription, Label.Name);
        }

        [Test]
        public void Should_GetCorrectText_FromEmptyField()
        {
            Assert.AreEqual(new CalculatorWindow().ResultsLabel.Text, string.Empty);
        }

        [Test]
        public void Should_GetElement_WhenElementIsPresent()
        {
            Assert.NotNull(rightArgumentTextBox.GetElement());
        }

        [Test]
        public void Should_ThrowNoSuchElementException_InGetElement_WhenElementIsNotPresent()
        {
            Assert.Throws<NoSuchElementException>(() => Label.GetElement(TimeSpan.Zero));
        }
    }
}