using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class MouseActionsTests : Actions.MouseActionsTests
    {
        protected override IMouseActions MouseActions => ApplicationManager.Application.MouseActions;
    }
}
