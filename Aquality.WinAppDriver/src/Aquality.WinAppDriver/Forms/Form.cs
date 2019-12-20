using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using Element = Aquality.WinAppDriver.Elements.Element;
using ElementFactory = Aquality.WinAppDriver.Elements.ElementFactory;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines base class for any form on any application's window.
    /// </summary>
    public abstract class Form : Element
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the window.</param>
        /// <param name="name">Name of the window.</param>
        /// <param name="parentForm">Parent form. If set to null, search context of <see cref="AqualityServices.Application"/> is used.</param>
        /// <param name="elementState">Element presence state.</param>
        protected Form(By locator, string name, Form parentForm = null, ElementState elementState = ElementState.Displayed) 
            : base(locator, name, () => parentForm?.GetElement(), parentForm?.IsRootSession == true, elementState)
        {
            ParentForm = parentForm;
            var relativeFinderFromForm = new RelativeElementFinder(LocalizedLogger, ConditionalWait, () => GetElement());
            RelativeElementFactory = new ElementFactory(ConditionalWait, relativeFinderFromForm, LocalizationManager);
        }

        public virtual bool IsRootSession => false;

        /// <summary>
        /// Element factory <see cref="IElementFactory"/>
        /// </summary>
        /// <value>Element factory.</value>
        protected virtual IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();
        
        /// <summary>
        /// Element factory <see cref="IElementFactory"/> to search from the context of the current form.
        /// </summary>
        /// <value>Relative element factory.</value>
        protected virtual IElementFactory RelativeElementFactory { get; }

        /// <summary>
        /// Finds element relative to current window.
        /// </summary>
        /// <typeparam name="T">Type of the target element.</typeparam>
        /// <param name="childLocator">Locator of the element relative to current window.</param>
        /// <param name="childName">Name of the element.</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom element.</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element.</returns>
        public T FindChildElement<T>(By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed) 
            where T : IElement
        {
            return RelativeElementFactory.FindChildElement(this, childLocator, childName, supplier, elementState);
        }

        protected Form ParentForm { get; }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.form");
    }
}
