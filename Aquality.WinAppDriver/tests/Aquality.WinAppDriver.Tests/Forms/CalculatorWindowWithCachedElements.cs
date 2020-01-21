using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorWindowWithCachedElements : Window, ICalculatorForm
    {
        public ITextBox LeftArgumentTextBox { get; }

        public ITextBox RightArgumentTextBox { get; }

        public IButton OneButton { get; }

        public IButton TwoButton { get; }

        public IButton PlusButton { get; }

        public IButton EqualsButton { get; }

        public IButton EmptyButton { get; }

        public IButton MaximizeButton { get; }

        public ILabel ResultsLabel { get; }

        public CalculatorWindowWithCachedElements() : base(CalculatorLocators.WindowLocator, "Calculator")
        {
            LeftArgumentTextBox = RelativeElementFactory.GetTextBox(CalculatorLocators.LeftArgumentTextBox, "Left Argument");
            RightArgumentTextBox = RelativeElementFactory.GetTextBox(CalculatorLocators.RightArgumentTextBox, "Right Argument");
            OneButton = RelativeElementFactory.GetButton(CalculatorLocators.OneButton, "1");
            TwoButton = RelativeElementFactory.GetButton(CalculatorLocators.TwoButton, "2");
            PlusButton = RelativeElementFactory.GetButton(CalculatorLocators.PlusButton, "+");
            EqualsButton = RelativeElementFactory.GetButton(CalculatorLocators.EqualsButton, "=");
            EmptyButton = RelativeElementFactory.GetButton(CalculatorLocators.EmptyButton, "Empty Button");
            MaximizeButton = RelativeElementFactory.GetButton(CalculatorLocators.MaximizeButton, "Maximize");
            ResultsLabel = RelativeElementFactory.GetLabel(CalculatorLocators.ResultsLabel, "Results bar");
        }
    }
}