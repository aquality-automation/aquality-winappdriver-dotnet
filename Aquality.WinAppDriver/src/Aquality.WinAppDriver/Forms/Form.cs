using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Drawing;
using Element = Aquality.WinAppDriver.Elements.Element;
using ElementFactory = Aquality.WinAppDriver.Elements.ElementFactory;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines base class for any form on any application's window.
    /// </summary>
    public abstract class Form : Element, IForm
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the window.</param>
        /// <param name="name">Name of the window.</param>
        /// <param name="parentForm">Parent form. If set to null, search context of <see cref="AqualityServices.Application"/> is used.</param>
        /// <param name="isRootSession">Determines whether the search of the current form would be performed from the <see cref="IWindowsApplication.RootSession"/> or not.
        /// If is set to false, search is performed from the application session <see cref="IWindowsApplication.Driver"/>.</param>
        /// <param name="elementState">Element presence state.</param>
        protected Form(By locator, string name, IForm parentForm = null, bool isRootSession = false, ElementState elementState = ElementState.Displayed)
            : base(locator, name, ResolveSearchContextSupplier(parentForm), parentForm == null ? isRootSession : parentForm.IsRootSession, elementState)
        {
            ParentForm = parentForm;
            var relativeFinderFromForm = new WindowsElementFinder(LocalizedLogger, ConditionalWait, () => GetElement());
            RelativeElementFactory = new ElementFactory(ConditionalWait, relativeFinderFromForm, LocalizationManager);
        }

        private static Func<ISearchContext> ResolveSearchContextSupplier(IForm parentForm)
        {
            return parentForm == null
                ? null
                : (Func<ISearchContext>)(() => parentForm.GetElement());
        }
        
        /// <summary>
        /// Return window state for form locator
        /// </summary>
        /// <value>True - form is opened,
        /// False - form is not opened.</value>
        public bool IsDisplayed => State.WaitForDisplayed();

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
        /// Finds element relative to current form.
        /// </summary>
        /// <typeparam name="T">Type of the target element.</typeparam>
        /// <param name="childLocator">Locator of the element relative to current form.</param>
        /// <param name="childName">Name of the element.</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom element.</param>
        /// <param name="elementState">Element existance state</param>
        /// <returns>Instance of element.</returns>
        public T FindChildElement<T>(By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed)
            where T : IElement
        {
            return RelativeElementFactory.FindChildElement(this, childLocator, childName, supplier, elementState);
        }

        /// <summary>
        /// Gets size of the form element defined by its locator.
        /// </summary>
        public Size Size => GetElement().Size;

        protected IForm ParentForm { get; }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.form");
    }
}
