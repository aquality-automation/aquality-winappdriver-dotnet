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
    /// </summary>
    public class MouseActions : ElementActions, IMouseActions
    {
        private readonly IElement element;
        private readonly Func<WindowsDriver> windowsDriverSupplier;

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
            this.windowsDriverSupplier = windowsDriverSupplier;
        }

        public void Click(MouseButton? button = null, IList<ModifierKey> modifierKeys = null, TimeSpan? duration = null, int? times = null, TimeSpan? interClickDelay = null)
        {
            var parameters = new Dictionary<string, object>() { { "elementId", element.GetElement().Id } };
            if (button != null)
            {
                parameters.Add("button", button.ToString().ToLowerInvariant());
            }
            if (modifierKeys != null && modifierKeys.Any())
            {
                parameters.Add("modifierKeys", modifierKeys.Select(key => key.ToString().ToLowerInvariant()).ToArray());
            }
            if (duration != null)
            {
                parameters.Add("durationMs", duration?.TotalMilliseconds);
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
                LogAction("loc.mouse.click.withParameters", PrepareParametersForLogging(parameters));
            }
            else
            {
                LogAction("loc.mouse.click");
            }
            windowsDriverSupplier().ExecuteScript("windows: click", parameters);
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

        public void Scroll(int offsetX, int offsetY)
        {
            LogAction("loc.mouse.scrollbyoffset", offsetX, offsetY);
            
            windowsDriverSupplier().ExecuteScript("windows: scroll", new Dictionary<string, object>() {
                {"elementId", element.GetElement().Id},
                {"deltaX", offsetX }
            });
            //remoteTouchScreenSupplier().Scroll(element.GetElement().Coordinates, offsetX, offsetY);
        }

        private string PrepareParametersForLogging(Dictionary<string, object> parameters)
        {
            return string.Join(",", parameters.Where(param => "elementId" != param.Key).Select(param => $"{Environment.NewLine}{param.Key}: {JsonConvert.SerializeObject(param.Value)}"));
        }
    }
}
