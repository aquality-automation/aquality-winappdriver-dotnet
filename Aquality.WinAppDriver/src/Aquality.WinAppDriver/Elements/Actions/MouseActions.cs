using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.WinAppDriver.Elements.Actions
{
    /// <summary>
    /// Implements Mouse actions for a specific element.
    /// There is an issue with absolute coordinates calculation on Application session, so better to use the root session here if possible.
    /// Alternatively, you can move application to top left corner before doing mouse action.
    /// This might be addressed in future when fixed in appium-windows-driver 
    /// </summary>
    public class MouseActions : ElementActions, IMouseActions
    {
        private readonly IElement element;

        /// <summary>
        /// Instantiates Mouse actions for a specific element.
        /// </summary>
        /// <param name="element">Target element.</param>
        /// <param name="elementType">Target element's type.</param>
        /// <param name="windowsDriverSupplier">Method to get current application session.</param>
        /// <param name="localizationLogger">Logger for localized values.</param>
        /// <param name="elementActionsRetrier">Retrier for element actions.</param>
        public MouseActions(IElement element, string elementType, Func<WindowsDriver> windowsDriverSupplier, ILocalizedLogger localizationLogger, IElementActionRetrier elementActionsRetrier)
            : base(element, elementType, windowsDriverSupplier, localizationLogger, elementActionsRetrier)
        {
            this.element = element;
        }

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

        public void Click(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null)
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

        public void ContextClick(IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null)
        {
            LogAction("loc.mouse.contextclick");
            Click(MouseButton.Right, modifierKeys, duration, times, interClickDelay);
        }

        public void DoubleClick(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, TimeSpan? interClickDelay = null)
        {
            LogAction("loc.mouse.doubleclick");
            Click(button, modifierKeys, duration, times: 2, interClickDelay);
        }

        public void DragAndDrop(IElement target, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            var parameters = ResolveParameters(modifierKeys, duration);
            LogAction("loc.mouse.draganddrop", target.GetElementType(), target.Name);
            parameters.Add("endElementId", target.GetElement().Id);
            PerformAction("windows: clickAndDrag", parameters, "startElementId");
        }

        public void DragAndDrop(int endX, int endY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            var parameters = ResolveParameters(modifierKeys, duration);
            LogAction("loc.mouse.draganddrop.tocoordinates", endX, endY);
            parameters.Add("endX", endX);
            parameters.Add("endY", endY);
            PerformAction("windows: clickAndDrag", parameters, "startElementId");
        }

        public void DragAndDropToOffset(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            LogAction("loc.mouse.draganddrop.tooffset", offsetX, offsetY);
            var location = element.Visual.Location;
            DragAndDrop(location.X + offsetX, location.Y + offsetY, modifierKeys, duration);
        }

        public void MoveByOffset(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            DragAndDropToOffset(offsetX, offsetY);
        }

        public void Hover(IElement endElement, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            var parameters = ResolveParameters(modifierKeys, duration);
            LogAction("loc.mouse.hover", endElement.GetElementType(), endElement.Name);
            parameters.Add("endElementId", endElement.GetElement().Id);
            PerformAction("windows: hover", parameters, "startElementId");
        }

        public void Hover(int endX, int endY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            var parameters = ResolveParameters(modifierKeys, duration);
            parameters.Add("endX", endX);
            parameters.Add("endY", endY);
            LogMouseAction("loc.mouse.hover.withparameters", parameters);
            PerformAction("windows: hover", parameters, "startElementId");
        }

        public void MoveFromElement(IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            var offsetX = - element.Visual.Size.Width / 2;
            var offsetY = - element.Visual.Size.Height / 2;
            LogAction("loc.mouse.movefromelement", offsetX, offsetY);
            var location = element.Visual.Location;
            Hover(location.X + offsetX, location.Y + offsetY, modifierKeys, duration);
        }

        public void MoveToElement(IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            LogAction("loc.mouse.movetoelement");
            Hover(element, modifierKeys, duration);
        }

        public void MoveToElement(int offsetX, int offsetY, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null)
        {
            LogAction("loc.mouse.movetoelement.byoffset", offsetX, offsetY);
            var location = element.Visual.Location;
            Hover(location.X + offsetX, location.Y + offsetY, modifierKeys, duration);
        }

        public void Scroll(int delta, ScrollDirection direction = ScrollDirection.Vertical, IList<ModifierKey> modifierKeys = null)
        {
            var parameters = ResolveParameters(modifierKeys);
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

        protected virtual void LogMouseAction(string localizationKey, Dictionary<string, object> parameters)
        {
            LogAction(localizationKey, PrepareParametersForLogging(parameters));
        }

        private string PrepareParametersForLogging(Dictionary<string, object> parameters)
        {
            return string.Join(",", parameters.Where(param => "elementId" != param.Key).Select(param => $"{Environment.NewLine}{param.Key}: {JsonConvert.SerializeObject(param.Value)}"));
        }
    }
}
