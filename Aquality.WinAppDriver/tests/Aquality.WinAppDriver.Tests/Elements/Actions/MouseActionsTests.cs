using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using IMouseActions = Aquality.WinAppDriver.Elements.Actions.IMouseActions;

namespace Aquality.WinAppDriver.Tests.Elements.Actions
{
    public class MouseActionsTests : TestWithApplication
    {
        protected IMouseActions MouseActions => LeftArgumentTextBox.MouseActions;
        private ITextBox LeftArgumentTextBox => new CalculatorForm().LeftArgumentTextBox;

        [Test]
        public void Should_PerformElementSpecificMouseActions()
        {
            LeftArgumentTextBox.Click();
            Assert.DoesNotThrow(() =>
            {
                LeftArgumentTextBox.MouseActions.DragAndDrop(LeftArgumentTextBox);
                LeftArgumentTextBox.MouseActions.DragAndDropToOffset(10, 10);
                LeftArgumentTextBox.MouseActions.MoveByOffset(10, 10);
                LeftArgumentTextBox.MouseActions.MoveFromElement();
                LeftArgumentTextBox.MouseActions.MoveToElement();
                LeftArgumentTextBox.MouseActions.MoveToElement(10, 10); 
                LeftArgumentTextBox.MouseActions.Scroll(5, ScrollDirection.Horizontal);
            });
        }

        [Test]
        public void Should_PerformMouseActions()
        {
            LeftArgumentTextBox.Click();
            Assert.DoesNotThrow(() =>
            {
                MouseActions.Click();
                MouseActions.DoubleClick();
                MouseActions.MoveByOffset(10, 10);
                MouseActions.Scroll(10, modifierKeys: [ModifierKey.Ctrl]);
            });
        }
    }
}
