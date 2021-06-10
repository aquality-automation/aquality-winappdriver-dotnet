using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CalculatorForm : Form, ICalculatorForm
    {
        public ITextBox LeftArgumentTextBox => ElementFactory.GetTextBox(CalculatorLocators.LeftArgumentTextBox, "Left Argument");

        public ITextBox RightArgumentTextBox => ElementFactory.GetTextBox(CalculatorLocators.RightArgumentTextBox, "Right Argument");

        public IButton OneButton => ElementFactory.GetButton(CalculatorLocators.OneButton, "1");

        public IButton TwoButton => ElementFactory.GetButton(CalculatorLocators.TwoButton, "2");

        public IButton PlusButton => ElementFactory.GetButton(CalculatorLocators.PlusButton, "+");

        public IButton EqualsButton => ElementFactory.GetButton(CalculatorLocators.EqualsButton, "=");

        public IButton EmptyButton => ElementFactory.GetButton(CalculatorLocators.EmptyButton, "Empty Button");

        public IButton MaximizeButton => ElementFactory.GetButton(CalculatorLocators.MaximizeButton, "Maximize");

        public ILabel ResultsLabel => ElementFactory.GetLabel(CalculatorLocators.ResultsLabel, "Results bar");

        public IElement NumberPad => ElementFactory.GetButton(CalculatorLocators.WindowLocator, "Number pad");

        public CalculatorForm() : base(CalculatorLocators.WindowLocator, "Calculator")
        {
        }

        protected override IDictionary<string, IElement> ElementsForVisualization => new Dictionary<string, IElement>
        {
            { NumberPad.Name, NumberPad },
            { MaximizeButton.Name, MaximizeButton }
        };
    }
}