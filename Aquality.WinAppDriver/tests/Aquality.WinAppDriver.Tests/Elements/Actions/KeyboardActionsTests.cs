using Aquality.WinAppDriver.Actions;

namespace Aquality.WinAppDriver.Tests.Elements.Actions
{
    public class KeyboardActionsTests : Tests.Actions.KeyboardActionsTests
    {
        protected override IKeyboardActions KeyboardActions => RightArgumentTextBox.KeyboardActions;
    }
}
