using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;

namespace Aquality.WinAppDriver.Tests.Elements.Actions
{
    public class MouseActionsTests : Tests.Actions.MouseActionsTests
    {
        protected override IMouseActions MouseActions => RightArgumentTextBox.MouseActions;
        private ITextBox LeftArgumentTextBox => new CalculatorForm().LeftArgumentTextBox;

        [Test]
        public void Should_PerformElementSpecificMouseActions()
        {
            RightArgumentTextBox.Click();
            Assert.DoesNotThrow(() =>
            {
                RightArgumentTextBox.MouseActions.DragAndDrop(LeftArgumentTextBox);
                RightArgumentTextBox.MouseActions.DragAndDropToOffset(10, 10);
                RightArgumentTextBox.MouseActions.MoveByOffset(10, 10);
                RightArgumentTextBox.MouseActions.MoveFromElement();
                RightArgumentTextBox.MouseActions.MoveToElement();
                RightArgumentTextBox.MouseActions.MoveToElement(10, 10);
                RightArgumentTextBox.MouseActions.MoveToElement(10, 10, MoveToElementOffsetOrigin.Center);
                RightArgumentTextBox.MouseActions.MoveToElement(10, 10, MoveToElementOffsetOrigin.TopLeft);
                RightArgumentTextBox.MouseActions.Scroll(10, 10);
            });
        }
    }
}
