using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Implements Mouse actions for a specific element.
    /// </summary>
    public class MouseActions : ElementActions, IMouseActions
    {
        private readonly IElement element;
        private readonly Func<RemoteTouchScreen> remoteTouchScreenSupplier;

        /// <summary>
        /// Instantiates Mouse actions for a specific element.
        /// </summary>
        /// <param name="element">Target element.</param>
        /// <param name="elementType">Target element's type.</param>
        /// <param name="applicationSupplier">Method to get current application session.</param>
        /// <param name="localizationLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        public MouseActions(IElement element, string elementType, Func<IWindowsApplication> applicationSupplier, ILocalizedLogger localizationLogger, ElementActionRetrier elementActionsRetrier)
            : base(element, elementType, applicationSupplier, localizationLogger, elementActionsRetrier)
        {
            this.element = element;
            remoteTouchScreenSupplier = () => new RemoteTouchScreen(applicationSupplier().Driver);
        }

        public void Click()
        {
            LogAction("loc.mouse.click");
            PerformAction((actions, element) => actions.Click(element));
        }

        public void ClickAndHold()
        {
            LogAction("loc.mouse.clickandhold");
            PerformAction((actions, element) => actions.ClickAndHold(element));
        }

        public void Release()
        {
            LogAction("loc.mouse.release");
            PerformAction((actions, element) => actions.Release(element));
        }

        public void ContextClick()
        {
            LogAction("loc.mouse.contextclick");
            PerformAction((actions, element) => actions.ContextClick(element));
        }

        public void DoubleClick()
        {
            LogAction("loc.mouse.doubleclick");
            PerformAction((actions, element) => actions.DoubleClick(element));
        }

        public void MoveByOffset(int offsetX, int offsetY)
        {
            DragAndDropToOffset(offsetX, offsetY);
        }

        public void DragAndDrop(IElement target)
        {
            LogAction("loc.mouse.draganddrop", target.GetElementType(), target.Name);
            PerformAction((actions, element) => actions.DragAndDrop(element, target.GetElement()));
        }

        public void DragAndDropToOffset(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.draganddrop.tooffset", offsetX, offsetY);
            PerformAction((actions, element) => actions.DragAndDropToOffset(element, offsetX, offsetY));
        }

        public void MoveFromElement()
        {
            var offsetX = - element.GetElement().Size.Width / 2;
            var offsetY = - element.GetElement().Size.Height / 2;
            LogAction("loc.mouse.movefromelement", offsetX, offsetY);
            PerformAction((actions, element) => actions.MoveToElement(element, offsetX, offsetY));
        }

        public void MoveToElement()
        {
            LogAction("loc.mouse.movetoelement");
            PerformAction((actions, element) => actions.MoveToElement(element));
        }

        public void MoveToElement(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.movetoelement.byoffset", offsetX, offsetY);
            PerformAction((actions, element) => actions.MoveToElement(element, offsetX, offsetY));
        }

        public void MoveToElement(int offsetX, int offsetY, MoveToElementOffsetOrigin offsetOrigin)
        {
            LogAction("loc.mouse.movetoelement.byoffset.withorigin", offsetX, offsetY, offsetOrigin);
            PerformAction((actions, element) => actions.MoveToElement(element, offsetX, offsetY, offsetOrigin));
        }

        public void Scroll(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.scrollbyoffset", offsetX, offsetY);
            remoteTouchScreenSupplier().Scroll(element.GetElement().Coordinates, offsetX, offsetY);
        }
    }
}
