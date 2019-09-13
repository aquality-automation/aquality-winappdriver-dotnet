using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class TextBoxTests : TestWithApplication
    {
        private const string ExpectedValue = "1";
        private IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        [Test]
        public void Should_EnterValues()
        {
            var textBox = GetTextBox(Factory, CalculatorWindow.RightArgumentTextBox, "Right Argument");
            textBox.Type(ExpectedValue);
            Assert.AreEqual(ExpectedValue, textBox.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_PrefilledTextbox()
        {
            var textBox = GetTextBox(Factory, CalculatorWindow.RightArgumentTextBox, "Right Argument");
            textBox.Type("123");
            textBox.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, textBox.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_EmptyTextbox()
        {
            var textBox = GetTextBox(Factory, CalculatorWindow.RightArgumentTextBox, "Right Argument");
            textBox.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, textBox.Value);
        }

        public static ITextBox GetTextBox(IElementFactory coreFactory, By loc, string nam) => coreFactory.GetTextBox(loc, nam);
    }
}
