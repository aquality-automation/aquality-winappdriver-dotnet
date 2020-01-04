using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorWindow : Window, ICalculatorForm
    {
        private static By WindowLocator => By.Name("Day Maxi Calc  v.1.5 Freeware");

        public ITextBox LeftArgumentTextBox => RelativeElementFactory.GetTextBox(MobileBy.AccessibilityId("50"), "Left Argument");

        public ITextBox RightArgumentTextBox => RelativeElementFactory.GetTextBox(By.XPath("//*[@AutomationId='49']"), "Right Argument");

        public IButton OneButton => RelativeElementFactory.GetButton(By.Name("1"), "1");

        public IButton TwoButton => RelativeElementFactory.GetButton(By.Name("2"), "2");

        public IButton PlusButton => RelativeElementFactory.GetButton(By.Name("+"), "+");

        public IButton EqualsButton => RelativeElementFactory.GetButton(By.Name("="), "=");

        public IButton EmptyButton => RelativeElementFactory.GetButton(By.XPath("//*[@AutomationId='7']"), "Empty Button");

        public IButton MaximizeButton => RelativeElementFactory.GetButton(By.Name("Maximize"), "Maximize");

        public ILabel ResultsLabel => RelativeElementFactory.GetLabel(MobileBy.AccessibilityId("48"), "Results bar");

        public CalculatorWindow() : base(WindowLocator, "Calculator")
        {
        }
    }
}