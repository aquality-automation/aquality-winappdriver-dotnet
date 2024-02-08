using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Elements;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Mouse actions for the whole application.
    /// </summary>
    public class MouseActions : ApplicationActions, IMouseActions
    {
        private readonly Func<WindowsDriver> windowsDriverSupplier;

        public MouseActions(ILocalizedLogger localizedLogger, Func<WindowsDriver> windowsDriverSupplier)
            : base(localizedLogger, windowsDriverSupplier)
        {
            this.windowsDriverSupplier = windowsDriverSupplier;
        }

        public void Click()
        {
            LogAction("loc.mouse.click");
            windowsDriverSupplier().ExecuteScript("windows:click", new Dictionary<string, object>() { { "x", 0 }, {"y", 0 } } );
        }

        public void ContextClick()
        {
            LogAction("loc.mouse.contextclick");
            windowsDriverSupplier().ExecuteScript("windows:click", new Dictionary<string, object>() { { "x", 0 }, { "y", 0 }, { "button", MouseButton.Right.ToString().ToLowerInvariant() } });
        }

        public void DoubleClick()
        {
            LogAction("loc.mouse.doubleclick");
            windowsDriverSupplier().ExecuteScript("windows:click", new Dictionary<string, object>() { { "x", 0 }, { "y", 0 }, { "times", 2 } });
        }

        public void MoveByOffset(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.movebyoffset", offsetX, offsetY);
            PerformAction(actions => actions.MoveByOffset(offsetX, offsetY));
        }

        public void Scroll(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.scrollbyoffset", offsetX, offsetY);
            var windowSize = windowsDriverSupplier().Manage().Window.Size;
            windowsDriverSupplier().ExecuteScript("windows: scroll", new Dictionary<string, object>() {
                {"x", windowSize.Width},
                {"y", windowSize.Height / 2},
                {"deltaX", offsetX }
            });
        }
    }
}
