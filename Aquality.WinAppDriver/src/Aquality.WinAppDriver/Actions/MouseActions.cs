using Aquality.Selenium.Core.Localization;
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
            windowsDriverSupplier().ExecuteScript("windows: keys", new Dictionary<string, object>() { { "actions", new[]
            {
                new KeyAction{ VirtualKeyCode = 0x01, Down = true}.ToDictionary(),
                //new Dictionary<string, object>() { { "virtualKeyCode", 0x01 }, { "down", true } },
                new Dictionary<string, object>() { { "pause", 500 } },
                new Dictionary<string, object>() { { "text", "Important text" } },
                new Dictionary<string, object>() { { "virtualKeyCode", 0x01 }, { "down", false } }
            }
            }});
        }

        public void ContextClick()
        {
            LogAction("loc.mouse.contextclick");
            windowsDriverSupplier().ExecuteScript("windows: keys",
                new Dictionary<string, object>() { { "virtualKeyCode", 0x02 }, { "down", true } },
                new Dictionary<string, object>() { { "virtualKeyCode", 0x02 }, { "down", false } });
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
