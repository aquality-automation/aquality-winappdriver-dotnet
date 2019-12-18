using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class KeyboardActionsTests : Actions.KeyboardActionsTests
    {
        protected override IKeyboardActions KeyboardActions => AqualityServices.Application.KeyboardActions;
    }
}
