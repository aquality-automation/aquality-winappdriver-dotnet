using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Tests.Applications.Locators
{
    public class CalculatorWindow : Window
    {
        private static By WindowLocator => By.TagName("Window");

        public ITextBox RightArgumentTextBox => ElementFactory.GetTextBox(By.XPath("//*[@AutomationId='49']"), "Right Argument");

        public IButton OneButton => ElementFactory.GetButton(By.Name("1"), "1");

        public IButton TwoButton => ElementFactory.GetButton(By.Name("2"), "2");

        public IButton PlusButton => ElementFactory.GetButton(By.Name("+"), "+");

        public IButton EqualsButton => ElementFactory.GetButton(By.Name("="), "=");

        public ILabel ResultsLabel => ElementFactory.GetLabel(MobileBy.AccessibilityId("48"), "Results bar");

        public IElement NumberPad => ElementFactory.GetButton(WindowLocator, "Number pad");

        public CalculatorWindow() : base(WindowLocator, "Main Calculator Window")
        {
        }
    }
}