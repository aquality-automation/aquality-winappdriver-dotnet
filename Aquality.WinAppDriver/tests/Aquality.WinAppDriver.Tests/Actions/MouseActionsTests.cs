using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Actions
{
    public class MouseActionsTests : TestWithApplication
    {
        protected virtual IMouseActions MouseActions => AqualityServices.MouseActions;

        protected static ITextBox RightArgumentTextBox => new CalculatorForm().RightArgumentTextBox;

        [Test]
        public void Should_PerformMouseActions()
        {
            RightArgumentTextBox.Click();
            Assert.DoesNotThrow(() =>
            {
                MouseActions.Click();
                MouseActions.ContextClick();
                MouseActions.DoubleClick();
                MouseActions.MoveByOffset(10, 10);
                MouseActions.Hover(15, 20);
                MouseActions.Scroll(10, direction: ScrollDirection.Horizontal);
            });
        }
    }
}
