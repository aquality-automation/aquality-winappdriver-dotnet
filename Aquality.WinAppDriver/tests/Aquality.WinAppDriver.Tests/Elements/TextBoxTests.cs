using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class TextBoxTests : TestWithApplication
    {
        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        [Test]
        public void Should_EnterValues()
        {
            var textBox = GetTextBox(Factory, CalculatorWindow.RightArgumentTextBox, "Right Argument");
            const string expectedValue = "1";
            textBox.Type(expectedValue);
            Assert.AreEqual(expectedValue, textBox.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_PrefilledTextbox()
        {
            var textBox = GetTextBox(Factory, CalculatorWindow.RightArgumentTextBox, "Right Argument");
            const string expectedValue = "1";
            textBox.Type("123");
            textBox.ClearAndType(expectedValue);
            Assert.AreEqual(expectedValue, textBox.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_EmptyTextbox()
        {
            var textBox = GetTextBox(Factory, CalculatorWindow.RightArgumentTextBox, "Right Argument");
            const string expectedValue = "1";
            textBox.ClearAndType(expectedValue);
            Assert.AreEqual(expectedValue, textBox.Value);
        }

        public static ITextBox GetTextBox(IElementFactory coreFactory, By loc, string nam) => coreFactory.GetTextBox(loc, nam);
    }
}
