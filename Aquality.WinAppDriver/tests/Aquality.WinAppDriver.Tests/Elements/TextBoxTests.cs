using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class TextBoxTests : TestWithApplication
    {
        private const string ExpectedValue = "1";
        private readonly CalculatorWindow CalculatorWindow = new CalculatorWindow();

        [Test]
        public void Should_EnterValues()
        {
            CalculatorWindow.TextBoxRightArgument.Type(ExpectedValue);
            Assert.AreEqual(ExpectedValue, CalculatorWindow.TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_EnterValues_WithoutCleaningTextbox()
        {
            CalculatorWindow.TextBoxRightArgument.Type(ExpectedValue);
            const string additionalExpectedValue = "23";
            CalculatorWindow.TextBoxRightArgument.Type(additionalExpectedValue);
            Assert.AreEqual(ExpectedValue + additionalExpectedValue, CalculatorWindow.TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_PrefilledTextbox()
        {
            CalculatorWindow.TextBoxRightArgument.Type("123");
            CalculatorWindow.TextBoxRightArgument.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, CalculatorWindow.TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_EmptyTextbox()
        {
            CalculatorWindow.TextBoxRightArgument.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, CalculatorWindow.TextBoxRightArgument.Value);
        }
    }
}