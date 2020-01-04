using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public interface ICalculatorForm : IForm
    {
        ITextBox LeftArgumentTextBox { get; }

        ITextBox RightArgumentTextBox { get; }

        IButton OneButton { get; }

        IButton TwoButton { get; }

        IButton PlusButton { get; }

        IButton EqualsButton { get; }

        IButton EmptyButton { get; }

        IButton MaximizeButton { get; }

        ILabel ResultsLabel { get; }
    }
}
