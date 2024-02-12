using Aquality.Selenium.Core.Localization;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Mouse actions for the whole application.
    /// </summary>
    public class MouseActions : ApplicationActions, IMouseActions
    {
        [DllImport("User32")]
        private static extern bool GetCursorPos(out Point lpPoint);

        public Point Coordinates
        {
            get
            {
                if (!GetCursorPos(out var point))
                {
                    throw new InvalidOperationException("Failed to get current mouse coordinates");
                }
                return point;
            }
        }

        public MouseActions(ILocalizedLogger localizedLogger, Func<WindowsDriver> windowsDriverSupplier)
            : base(localizedLogger, windowsDriverSupplier)
        {
        }

        /// <summary>
        /// Resolves nullable parameters for mouse action scripts.
        /// </summary>
        /// <param name="modifierKeys"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        protected virtual Dictionary<string, object> ResolveParameters(IList<ModifierKey> modifierKeys, TimeSpan? duration = null)
        {
            var parameters = new Dictionary<string, object>();
            if (modifierKeys != null && modifierKeys.Any())
            {
                parameters.Add("modifierKeys", modifierKeys.Select(key => key.ToString().ToLowerInvariant()).ToArray());
            }
            if (duration != null)
            {
                parameters.Add("durationMs", duration?.TotalMilliseconds);
            }
            return parameters;
        }

        /// <summary>
        /// Prepares action parameters for logging, excluding elementId from them.
        /// </summary>
        /// <param name="parameters">Parameters of the action.</param>
        /// <returns>Parameters formatted as string.</returns>
        protected virtual string PrepareParametersForLogging(Dictionary<string, object> parameters)
        {
            return string.Join(",", parameters.Where(param => "elementId" != param.Key).Select(param => $"{Environment.NewLine}{param.Key}: {JsonConvert.SerializeObject(param.Value)}"));
        }

        /// <summary>
        /// Logs mouse action.
        /// </summary>
        /// <param name="localizationKey">Key of the localized message.</param>
        /// <param name="parameters">Parameters of the action.</param>
        protected virtual void LogMouseAction(string localizationKey, Dictionary<string, object> parameters)
        {
            LogAction(localizationKey, PrepareParametersForLogging(parameters));
        }

        public void Click(int? x = null, int? y = null, MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null)
        {
            var parameters = ResolveParameters(modifierKeys);
            if (button != null)
            {
                parameters.Add("button", button.ToString().ToLowerInvariant());
            }
            if (times != null)
            {
                parameters.Add("times", times);
            }
            if (interClickDelay != null)
            {
                parameters.Add("interClickDelayMs", interClickDelay?.TotalMilliseconds);
            }
            Point cursor;
            if (x == null || y == null)
            {
                cursor = Coordinates;
            }
            parameters.Add("x", x ?? cursor.X);
            parameters.Add ("y", y ?? cursor.Y);
            if (parameters.Count > 1)
            {
                LogMouseAction("loc.mouse.click.withparameters", parameters);
            }
            else
            {
                LogAction("loc.mouse.click");
            }
            PerformAction("windows: click", parameters);            
        }
            

        public void ContextClick(int? x = null, int? y = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null)
        {
            LogAction("loc.mouse.contextclick");
            Click(x, y, MouseButton.Right, modifierKeys, duration, times, interClickDelay);
        }

        public void DoubleClick(int? x = null, int? y = null, MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null)
        {
            LogAction("loc.mouse.doubleclick");
            Click(x, y, button, modifierKeys, duration, times: 2, interClickDelay);
        }

        public void MoveByOffset(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            LogAction("loc.mouse.movebyoffset", offsetX, offsetY);
            var location = Coordinates;
            Hover(location.X, location.Y, location.X + offsetX, location.Y + offsetY, modifierKeys, duration);
        }

        public void Hover(int startX, int startY, int? endX = null, int? endY = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            var parameters = ResolveParameters(modifierKeys, duration);
            LogAction("loc.mouse.hover", $"({startX}, {startY}) ->", $"({endX ?? startX}, {endY ?? startY})");
            parameters.Add("startX", startX);
            parameters.Add("startY", startY);
            parameters.Add("endX", endX ?? startX);
            parameters.Add("endY", endY ?? startY);
            PerformAction("windows: hover", parameters);
        }

        public void Scroll(int delta, int? x = null, int? y = null, ScrollDirection direction = ScrollDirection.Vertical, IList<ModifierKey> modifierKeys = null)
        {
            var parameters = ResolveParameters(modifierKeys);
            Point cursor;
            if (x == null || y == null)
            {
                cursor = Coordinates;
            }
            parameters.Add("x", x ?? cursor.X);
            parameters.Add("y", y ?? cursor.Y);
            if (direction == ScrollDirection.Vertical)
            {
                parameters.Add("deltaY", delta);
            }
            else
            {
                parameters.Add("deltaX", delta);
            }
            LogMouseAction("loc.mouse.scroll", parameters);
            PerformAction("windows: scroll", parameters);
        }
    }
}
