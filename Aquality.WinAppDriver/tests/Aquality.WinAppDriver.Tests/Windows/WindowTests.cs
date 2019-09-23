using Aquality.WinAppDriver.Tests.Windows.Models;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Windows
{
    public class WindowTests : TestWithApplication
    {
        private const int ExpectedHeight = 500;
        private const int ExpectedWidth = 709;
        private static readonly By Locator = By.XPath("//*[@id='111111']");
        private const string PageName = "Not present page";
        private static readonly CalculatorWindowWithRelativeElements calculatorWindow = new CalculatorWindowWithRelativeElements();

        [Test]
        public void Should_WorkWithCalculator_ViaRelativeElements()
        {
            calculatorWindow.OneButton.Click();
            calculatorWindow.PlusButton.Click();
            calculatorWindow.TwoButton.Click();
            calculatorWindow.EqualsButton.Click();
            StringAssert.Contains("3", calculatorWindow.ResultsLabel.Text);
        }

        [Test]
        public void Should_GetSizeCorrectly_WhenindowIsPresent()
        {
            var windowSize = new CalculatorWindow().Size;
            Assert.Multiple(() =>
            {
                Assert.IsFalse(windowSize.IsEmpty, "Window is not empty");
                Assert.AreEqual(ExpectedHeight, windowSize.Height, "Height");
                Assert.AreEqual(ExpectedWidth, windowSize.Width, "Width");
            });
        }

        [Test]
        public void Should_GetSizeCorrectly_WhenWindowIsNotPresent()
        {
            var windowSize = TestWindow.Size;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(windowSize.IsEmpty, "Window is not empty");
                Assert.AreEqual(0, windowSize.Height, "Height");
                Assert.AreEqual(0, windowSize.Width, "Width");
            });
        }

        [Test]
        public void Should_ReturnTrue_IfWindowIsDisplayed()
        {
            Assert.IsTrue(new CalculatorWindow().IsDisplayed);
        }

        [Test]
        public void Should_ReturnFalse_IfWindowIsNotDisplayed()
        {
            Assert.IsFalse(TestWindow.IsDisplayed);
        }

        [Test]
        public void Should_SetCorrectLocatorInConstructor()
        {
            Assert.AreEqual(Locator, TestWindow.Locator, "Locator");
        }

        [Test]
        public void Should_SetCorrectPageNameInConstructor()
        {
            Assert.AreEqual(PageName, TestWindow.Name, "Name");
        }

        private static TestWindow TestWindow => new TestWindow(Locator, PageName);
    }
}
