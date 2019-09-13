using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Defines Label UI element.
    /// </summary>
    public class Label : Element, ILabel
    {
        protected internal Label(By locator, string name) : base(locator, name)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.label");
    }
}
