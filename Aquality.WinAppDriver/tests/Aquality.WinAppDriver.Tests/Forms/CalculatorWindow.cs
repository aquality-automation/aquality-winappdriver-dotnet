using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorWindow(Func<WindowsDriver> customSessionSupplier = null) : Window(CalculatorLocators.WindowLocator, "Calculator", customSessionSupplier), ICalculatorForm
    {
        public ITextBox LeftArgumentTextBox => RelativeElementFactory.GetTextBox(CalculatorLocators.LeftArgumentTextBox, "Left Argument");

        public ITextBox RightArgumentTextBox => RelativeElementFactory.GetTextBox(CalculatorLocators.RightArgumentTextBox, "Right Argument");

        public IButton OneButton => RelativeElementFactory.GetButton(CalculatorLocators.OneButton, "1");

        public IButton TwoButton => RelativeElementFactory.GetButton(CalculatorLocators.TwoButton, "2");

        public IButton PlusButton => RelativeElementFactory.GetButton(CalculatorLocators.PlusButton, "+");

        public IButton EqualsButton => RelativeElementFactory.GetButton(CalculatorLocators.EqualsButton, "=");

        public IButton EmptyButton => RelativeElementFactory.GetButton(CalculatorLocators.EmptyButton, "Empty Button");

        public IButton MaximizeButton => RelativeElementFactory.GetButton(CalculatorLocators.MaximizeButton, "Maximize");

        public ILabel ResultsLabel => RelativeElementFactory.GetLabel(CalculatorLocators.ResultsLabel, "Results bar");
    }
}