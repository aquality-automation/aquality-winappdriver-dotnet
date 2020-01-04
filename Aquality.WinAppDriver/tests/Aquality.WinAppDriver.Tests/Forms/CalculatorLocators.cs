using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public static class CalculatorLocators
    {
        public static By WindowLocator => By.Name("Day Maxi Calc  v.1.5 Freeware");

        public static By LeftArgumentTextBox => MobileBy.AccessibilityId("50");

        public static By RightArgumentTextBox => By.XPath("//*[@AutomationId='49']");

        public static By OneButton => By.Name("1");

        public static By TwoButton => By.Name("2");

        public static By PlusButton => By.Name("+");

        public static By EqualsButton => By.Name("=");

        public static By EmptyButton => By.XPath("//*[@AutomationId='7']");

        public static By MaximizeButton => By.Name("Maximize");

        public static By ResultsLabel => MobileBy.AccessibilityId("48");

        public static By ResultsLabelByXPath => By.XPath(".//*[@AutomationId='48']");
    }
}
