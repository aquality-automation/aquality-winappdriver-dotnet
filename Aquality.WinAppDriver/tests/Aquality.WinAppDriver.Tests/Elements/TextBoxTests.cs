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

        private readonly ITextBox TextBoxRightArgument = 
            GetTextBox(ApplicationManager.GetRequiredService<IElementFactory>(), CalculatorWindow.RightArgumentTextBox, "Right Argument");

        [Test]
        public void Should_EnterValues()
        {
            TextBoxRightArgument.Type(ExpectedValue);
            Assert.AreEqual(ExpectedValue, TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_EnterValues_WithoutCleaningTextbox()
        {
            TextBoxRightArgument.Type(ExpectedValue);
            const string additionalExpectedValue = "23";
            TextBoxRightArgument.Type(additionalExpectedValue);
            Assert.AreEqual(ExpectedValue + additionalExpectedValue, TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_PrefilledTextbox()
        {
            TextBoxRightArgument.Type("123");
            TextBoxRightArgument.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, TextBoxRightArgument.Value);
        }

        [Test]
        public void Should_ClearTextBeforeEnteringValues_EmptyTextbox()
        {
            TextBoxRightArgument.ClearAndType(ExpectedValue);
            Assert.AreEqual(ExpectedValue, TextBoxRightArgument.Value);
        }

        public static ITextBox GetTextBox(IElementFactory coreFactory, By loc, string nam) => coreFactory.GetTextBox(loc, nam);
    }
}