using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorFormWithRelativeElements : CalculatorForm
    {
        public new IButton OneButton => FindChildElement<IButton>(CalculatorLocators.OneButton, base.OneButton.Name);

        public new IButton TwoButton => FindChildElement<IButton>(CalculatorLocators.TwoButton, base.TwoButton.Name);

        public new IButton PlusButton => FindChildElement<IButton>(CalculatorLocators.PlusButton, base.PlusButton.Name);

        public new IButton EqualsButton => FindChildElement<IButton>(CalculatorLocators.EqualsButton, base.EqualsButton.Name);

        public new ILabel ResultsLabel => FindChildElement<ILabel>(CalculatorLocators.ResultsLabelByXPath);
    }
}