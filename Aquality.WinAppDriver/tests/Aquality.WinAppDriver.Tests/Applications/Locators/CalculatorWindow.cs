using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Tests.Applications.Locators
{
    public class CalculatorWindow : Window
    {
        public static By WindowLocator => By.TagName("Window");

        public static By OneButtonLocator => By.Name("1");

        public static By TwoButtonLocator => By.Name("2");

        public static By ThreeButtonLocator => By.Name("3");

        public static By PlusButtonLocator => By.Name("+");

        public static By EqualsButtonLocator => By.Name("=");

        public static By RightArgumentTextBoxLocator => By.XPath("//*[@AutomationId='49']");

        public static By ResultsLabelLocator => MobileBy.AccessibilityId("48");

        public ITextBox TextBoxRightArgument => ElementFactory.GetTextBox(RightArgumentTextBoxLocator, "Right Argument");

        public CalculatorWindow() : base(WindowLocator, "Main Calculator Window")
        {
        }
    }
}