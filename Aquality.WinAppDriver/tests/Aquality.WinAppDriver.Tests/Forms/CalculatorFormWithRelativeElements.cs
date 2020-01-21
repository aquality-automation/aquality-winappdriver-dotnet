using Aquality.WinAppDriver.Elements.Interfaces;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorFormWithRelativeElements : CalculatorForm, ICalculatorForm
    {
        public new IButton OneButton => FindChildElement<IButton>(CalculatorLocators.OneButton, base.OneButton.Name);

        public new IButton TwoButton => FindChildElement<IButton>(CalculatorLocators.TwoButton, base.TwoButton.Name);

        public new IButton PlusButton => FindChildElement<IButton>(CalculatorLocators.PlusButton, base.PlusButton.Name);

        public new IButton EqualsButton => FindChildElement<IButton>(CalculatorLocators.EqualsButton, base.EqualsButton.Name);

        public new ILabel ResultsLabel => FindChildElement<ILabel>(CalculatorLocators.ResultsLabelByXPath);
    }
}