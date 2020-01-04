using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorFormWithRelativeElements : CalculatorForm
    {
        public new IButton OneButton => FindChildElement<IButton>(base.OneButton.Locator, base.OneButton.Name);

        public new IButton TwoButton => FindChildElement<IButton>(base.TwoButton.Locator, base.TwoButton.Name);

        public new IButton PlusButton => FindChildElement<IButton>(base.PlusButton.Locator, base.PlusButton.Name);

        public new IButton EqualsButton => FindChildElement<IButton>(base.EqualsButton.Locator, base.EqualsButton.Name);

        public new ILabel ResultsLabel => FindChildElement<ILabel>(By.XPath(".//*[@AutomationId='48']"));
    }
}