using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class TextBoxTests : TestWithApplication
    {
        private const string ExpectedValue = "1";
        private readonly CalculatorWindow calculatorWindow = new CalculatorWindow();

        [Test]
        public void Should_EnterValues()
        {
            calculatorWindow.TextBoxRightArgument.Type(ExpectedValue);
            Assert.AreEqual(ExpectedValue, calculatorWindow.TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_EnterValues_WithoutCleaningTextbox()
        {
            calculatorWindow.TextBoxRightArgument.Type(ExpectedValue);
            const string additionalExpectedValue = "23";
            calculatorWindow.TextBoxRightArgument.Type(additionalExpectedValue);
            Assert.AreEqual(ExpectedValue + additionalExpectedValue, calculatorWindow.TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_PrefilledTextbox()
        {
            calculatorWindow.TextBoxRightArgument.Type("123");
            calculatorWindow.TextBoxRightArgument.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, calculatorWindow.TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_EmptyTextbox()
        {
            calculatorWindow.TextBoxRightArgument.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, calculatorWindow.TextBoxRightArgument.Value);
        }
    }
}