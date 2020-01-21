using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorFormWithCachedElements : Form, ICalculatorForm
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

        public CalculatorFormWithCachedElements() : base(CalculatorLocators.WindowLocator, "Calculator")
        {
            LeftArgumentTextBox = ElementFactory.GetTextBox(CalculatorLocators.LeftArgumentTextBox, "Left Argument");
            RightArgumentTextBox = ElementFactory.GetTextBox(CalculatorLocators.RightArgumentTextBox, "Right Argument");
            OneButton = ElementFactory.GetButton(CalculatorLocators.OneButton, "1");
            TwoButton = ElementFactory.GetButton(CalculatorLocators.TwoButton, "2");
            PlusButton = ElementFactory.GetButton(CalculatorLocators.PlusButton, "+");
            EqualsButton = ElementFactory.GetButton(CalculatorLocators.EqualsButton, "=");
            EmptyButton = ElementFactory.GetButton(CalculatorLocators.EmptyButton, "Empty Button");
            MaximizeButton = ElementFactory.GetButton(CalculatorLocators.MaximizeButton, "Maximize");
            ResultsLabel = ElementFactory.GetLabel(CalculatorLocators.ResultsLabel, "Results bar");
        }
    }
}