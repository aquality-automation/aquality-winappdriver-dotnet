using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class TextBoxTests : TestWithApplication
    {
        private const string ExpectedValue = "1";
        private readonly ITextBox rightArgumentTextBox = new CalculatorForm().RightArgumentTextBox;

        [Test]
        public void Should_EnterValues()
        {
            rightArgumentTextBox.Type(ExpectedValue);
            Assert.AreEqual(ExpectedValue, rightArgumentTextBox.Value);
        }

        [Test]
        public void Should_EnterValues_WithoutCleaningTextbox()
        {
            rightArgumentTextBox.Type(ExpectedValue);
            const string additionalExpectedValue = "23";
            rightArgumentTextBox.Type(additionalExpectedValue);
            Assert.AreEqual(ExpectedValue + additionalExpectedValue, rightArgumentTextBox.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_PrefilledTextbox()
        {
            rightArgumentTextBox.Type("123");
            rightArgumentTextBox.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, rightArgumentTextBox.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_EmptyTextbox()
        {
            rightArgumentTextBox.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, rightArgumentTextBox.Value);
        }
    }
}