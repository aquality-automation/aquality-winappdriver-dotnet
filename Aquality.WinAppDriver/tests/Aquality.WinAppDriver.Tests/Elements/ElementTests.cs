using System;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementTests : TestWithApplication
    {
        private readonly ITextBox rightArgumentTextBox = new CalculatorForm().RightArgumentTextBox;
        private const string ExpectedValue = "2";
        private static readonly By ElementLocator = By.XPath("//*[@id='111111']");
        private const string ElementDescription = "Not present element";

        private static IElementFactory Factory => AqualityServices.Get<IElementFactory>();
        private static IElement Label => Factory.GetLabel(ElementLocator, ElementDescription);

        [Test]
        public void Should_SendKeys()
        {
            rightArgumentTextBox.SendKeys(ExpectedValue);
            Assert.That(rightArgumentTextBox.Value, Is.EqualTo(ExpectedValue));
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
            Assert.That(rightArgumentTextBox.Value, Is.Null);
        }

        [Test]
        public void Should_SetCorrectLocatorInConstructor()
        {
            Assert.That(Label.Locator, Is.EqualTo(ElementLocator));
        }

        [Test]
        public void Should_SetCorrectNameInConstructor()
        {
            Assert.That(Label.Name, Is.EqualTo(ElementDescription));
        }

        [Test]
        public void Should_GetCorrectText_FromEmptyField()
        {
            Assert.That(new CalculatorForm().ResultsLabel.Text, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Should_GetElement_WhenElementIsPresent()
        {
            Assert.That(rightArgumentTextBox.GetElement(), Is.Not.Null);
        }

        [Test]
        public void Should_ThrowNoSuchElementException_InGetElement_WhenElementIsNotPresent()
        {
            Assert.Throws<NoSuchElementException>(() => Label.GetElement(TimeSpan.Zero));
        }
    }
}