using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Mouse actions for the whole application.
    /// </summary>
    public class MouseActions : ApplicationActions, IMouseActions
    {
        public MouseActions(LocalizationLogger localizationLogger, Func<WindowsDriver<WindowsElement>> windowsDriverSupplier)
            : base(localizationLogger, windowsDriverSupplier)
        {
        }

        public void Click()
        {
            LogAction("loc.mouse.click");
            PerformAction(actions => actions.Click());
        }

        public void ClickAndHold()
        {
            LogAction("loc.mouse.clickandhold");
            PerformAction(actions => actions.ClickAndHold());
        }

        public void Release()
        {
            LogAction("loc.mouse.release");
            PerformAction(actions => actions.Release());
        }

        public void ContextClick()
        {
            LogAction("loc.mouse.contextclick");
            PerformAction(actions => actions.ContextClick());
        }

        public void DoubleClick()
        {
            LogAction("loc.mouse.doubleclick");
            PerformAction(actions => actions.DoubleClick());
        }

        public void MoveByOffset(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.movebyoffset");
            PerformAction(actions => actions.MoveByOffset(offsetX, offsetY));
        }
    }
}
