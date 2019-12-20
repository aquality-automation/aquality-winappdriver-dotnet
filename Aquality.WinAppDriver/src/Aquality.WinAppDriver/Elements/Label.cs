using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using ElementState = Aquality.Selenium.Core.Elements.ElementState;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Defines Label UI element.
    /// </summary>
    public class Label : Element, ILabel
    {
        protected internal Label(
            By locator,
            string name,
            Func<ISearchContext> searchContextSupplier = null,
            bool isRootSession = false,
            ElementState elementState = ElementState.ExistsInAnyState)
            : base(locator, name, searchContextSupplier, isRootSession, elementState)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.label");
    }
}
