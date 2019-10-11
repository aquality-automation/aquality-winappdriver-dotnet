using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using ElementState = Aquality.Selenium.Core.Elements.ElementState;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Defines Label UI element.
    /// </summary>
    public class Label : Element, ILabel
    {
        protected internal Label(By locator, string name, ElementState elementState = ElementState.Displayed) : base(locator, name, elementState)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.label");
    }
}
