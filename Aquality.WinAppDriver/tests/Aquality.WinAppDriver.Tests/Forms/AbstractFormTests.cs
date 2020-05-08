using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public abstract class AbstractFormTests : TestWithApplication
    {
        private const int ExpectedHeight = 500;
        private const int ExpectedWidth = 709;

        protected By Locator => By.XPath("//*[@id='111111']");
        protected string PageName => "Not present page";

        protected abstract string ExpectedElementType { get;}

        protected abstract ICalculatorForm CalculatorForm { get; }

        protected abstract ITestForm TestForm { get; }

        [Test]
        public void Should_WorkWithCalculator_ViaRelativeElements()
        {
            CalculatorForm.OneButton.Click();
            CalculatorForm.PlusButton.Click();
            CalculatorForm.TwoButton.Click();
            CalculatorForm.EqualsButton.Click();
            StringAssert.Contains("3", CalculatorForm.ResultsLabel.Text);
        }

        [Test]
        public void Should_WorkWithCalculator_WithCachedElement()
        {
            var oneButton = CalculatorForm.OneButton;
            oneButton.Click();
            CalculatorForm.PlusButton.Click();
            oneButton.Click();
            CalculatorForm.EqualsButton.Click();
            var result = CalculatorForm.ResultsLabel.Text;
            StringAssert.Contains("2", result);
        }

        [Test]
        public void Should_GetSizeCorrectly_WhenFormIsPresent()
        {
            var formSize = CalculatorForm.Size;
            Assert.Multiple(() =>
            {
                Assert.IsFalse(formSize.IsEmpty, "Form is not empty");
                Assert.AreEqual(ExpectedHeight, formSize.Height, "Height");
                Assert.AreEqual(ExpectedWidth, formSize.Width, "Width");
            });
        }

        [Test]
        public void Should_ThrowException_InGetSize_WhenFormIsNotPresent()
        {
            Assert.Throws<NoSuchElementException>(() =>
            {
                var testFormSize = TestForm.Size;
            });
        }

        [Test]
        public void Should_ReturnTrue_IfFormIsDisplayed()
        {
            Assert.IsTrue(CalculatorForm.State.IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfFormIsNotDisplayed()
        {
            Assert.IsFalse(TestForm.State.IsDisplayed);
        }

        [Test]
        public void Should_SetCorrectLocatorInConstructor()
        {
            Assert.AreEqual(Locator, TestForm.Locator, "Locator");
        }

        [Test]
        public void Should_SetCorrectPageNameInConstructor()
        {
            Assert.AreEqual(PageName, TestForm.Name, "Name");
        }

        [Test]
        public void Should_SetCorrectSession_WhenConstructorParameterIsNull()
        {
            var expectedSession = TestForm is Window ? AqualityServices.Application.RootSession : AqualityServices.Application.Driver;
            Assert.AreEqual(expectedSession, TestForm.WindowsDriverSupplier(), "WindowsDriverSupplier");
        }

        [Test]
        public void Should_ReturnCorrectElementType()
        {
            Assert.AreEqual(ExpectedElementType, TestForm.ElementType);
        }
    }
}