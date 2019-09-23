using Aquality.WinAppDriver.Tests.Applications.Locators;
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

        [Test]
        public void Should_GetSizeCorrectly()
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
        public void Should_ReturnTrue_IfWindowIsDisplayed()
        {
            var window = new CalculatorWindow();
            Assert.IsTrue(window.IsDisplayed);
        }

        [Test]
        public void Should_SetCorrectLocatorInConstructor()
        {
            var window = new TestWindow(Locator, PageName);
            Assert.AreEqual(Locator, window.Locator, "Locator");
        }

        [Test]
        public void Should_SetCorrectPageNameInConstructor()
        {
            var window = new TestWindow(Locator, PageName);
            Assert.AreEqual(PageName, window.Name, "Name");
        }
    }
}