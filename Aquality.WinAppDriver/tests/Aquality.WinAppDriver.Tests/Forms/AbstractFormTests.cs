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

        protected abstract ICalculatorForm CalculatorFormWithRelativeElements { get; }

        protected abstract ITestForm TestForm { get; }

        [Test]
        public void Should_WorkWithCalculator_ViaRelativeElements()
        {
            CalculatorFormWithRelativeElements.OneButton.Click();
            CalculatorFormWithRelativeElements.PlusButton.Click();
            CalculatorFormWithRelativeElements.TwoButton.Click();
            CalculatorFormWithRelativeElements.EqualsButton.Click();
            StringAssert.Contains("3", CalculatorFormWithRelativeElements.ResultsLabel.Text);
        }

        [Test]
        public void Should_GetSizeCorrectly_WhenFormIsPresent()
        {
            var formSize = CalculatorFormWithRelativeElements.Size;
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
            Assert.IsTrue(CalculatorFormWithRelativeElements.IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfFormIsNotDisplayed()
        {
            Assert.IsFalse(TestForm.IsDisplayed);
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