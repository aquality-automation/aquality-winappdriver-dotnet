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

        protected static By Locator => By.XPath("//*[@id='111111']");
        protected static string PageName => "Not present page";

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
            Assert.That(CalculatorForm.ResultsLabel.Text, Does.Contain("3"));
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
            Assert.That(result, Does.Contain("2"));
        }

        [Test]
        public void Should_GetSizeCorrectly_WhenFormIsPresent()
        {
            var formSize = CalculatorForm.Size;
            Assert.Multiple(() =>
            {
                Assert.That(formSize.IsEmpty, Is.False, "Form is not empty");
                Assert.That(formSize.Height, Is.EqualTo(ExpectedHeight), "Height");
                Assert.That(formSize.Width, Is.EqualTo(ExpectedWidth), "Width");
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
            CalculatorForm.State.WaitForDisplayed();
            Assert.That(CalculatorForm.State.IsDisplayed, Is.True);
        }

        [Test]
        public void Should_ReturnFalse_IfFormIsNotDisplayed()
        {
            Assert.That(TestForm.State.IsDisplayed, Is.False);
        }

        [Test]
        public void Should_SetCorrectLocatorInConstructor()
        {
            Assert.That(TestForm.Locator, Is.EqualTo(Locator), "Locator");
        }

        [Test]
        public void Should_SetCorrectPageNameInConstructor()
        {
            Assert.That(TestForm.Name, Is.EqualTo(PageName), "Name");
        }

        [Test]
        public void Should_SetCorrectSession_WhenConstructorParameterIsNull()
        {
            var expectedSession = TestForm is Window ? AqualityServices.Application.RootSession : AqualityServices.Application.Driver;
            Assert.That(TestForm.WindowsDriverSupplier(), Is.EqualTo(expectedSession), "WindowsDriverSupplier");
        }

        [Test]
        public void Should_ReturnCorrectElementType()
        {
            Assert.That(TestForm.ElementType, Is.EqualTo(ExpectedElementType));
        }
    }
}