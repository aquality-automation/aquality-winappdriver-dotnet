using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Drawing;
using Element = Aquality.WinAppDriver.Elements.Element;
using ElementFactory = Aquality.WinAppDriver.Elements.ElementFactory;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;
using Aquality.Selenium.Core.Visualization;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Aquality.Selenium.Core.Configurations;

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
        /// <param name="customSessionSupplier">Custom WinAppDriver session supplier.</param>
        /// <param name="elementState">Element presence state.</param>
        protected Form(By locator, string name, IForm parentForm = null, Func<WindowsDriver> customSessionSupplier = null, ElementState elementState = ElementState.Displayed)
            : base(locator, name, ResolveSearchContextSupplier(parentForm), customSessionSupplier ?? parentForm?.WindowsDriverSupplier, elementState)
        {
            var relativeFinderFromForm = new WindowsElementFinder(LocalizedLogger, ConditionalWait, () => GetElement());
            RelativeElementFactory = new ElementFactory(ConditionalWait, relativeFinderFromForm, LocalizationManager);
        }

        public Process Process => Process.GetProcessById(Convert.ToInt32(GetAttribute("ProcessId")));

        private static Func<ISearchContext> ResolveSearchContextSupplier(IForm parentForm)
        {
            return parentForm == null
                ? null
                : (Func<ISearchContext>)(() => parentForm.GetElement());
        }

        /// <summary>
        /// Element factory <see cref="IElementFactory"/>
        /// </summary>
        /// <value>Element factory.</value>
        protected virtual IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();

        /// <summary>
        /// Windows Application <see cref="IWindowsApplication"/>.  
        /// </summary>
        protected virtual new IWindowsApplication Application => AqualityServices.Application;

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
        /// <param name="elementState">Element existence state</param>
        /// <returns>Instance of element.</returns>
        protected virtual new T FindChildElement<T>(By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed)
            where T : IElement
        {
            return RelativeElementFactory.FindChildElement(this, childLocator, GetChildElementName(childName), supplier, elementState);
        }

        protected virtual string GetChildElementName(string pureName) => $"{pureName}' - {ElementType} '{Name}";

        /// <summary>
        /// Gets size of the form element defined by its locator.
        /// </summary>
        public virtual Size Size => GetElement().Size;

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.form");

        protected virtual IVisualizationConfiguration VisualizationConfiguration => AqualityServices.Get<IVisualizationConfiguration>();
                
        /// <summary>
        /// Gets dump manager for the current form that could be used for visualization purposes, such as saving and comparing dumps.
        /// Uses <see cref="ElementsForVisualization"/> as basis for dump creation and comparison.
        /// </summary>
        public virtual IDumpManager Dump => new DumpManager<IElement>(ElementsForVisualization, Name, VisualizationConfiguration, LocalizedLogger);

        /// <summary>
        /// List of pairs uniqueName-element to be used for dump saving and comparing.
        /// By default, only currently displayed elements to be used (<see cref="ElementsInitializedAsDisplayed"/>).
        /// You can override this property with defined <see cref="AllElements"/>, <see cref="DisplayedElements"/> or your own element set.
        /// </summary>
        protected virtual IDictionary<string, IElement> ElementsForVisualization => DisplayedElements;

        /// <summary>
        /// List of pairs uniqueName-element from all fields and properties of type <see cref="IElement"/>.
        /// </summary>
        protected IDictionary<string, IElement> AllElements
        {
            get
            {
                const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                var elementProperties = GetType().GetProperties(bindingFlags).Where(property => typeof(IElement).IsAssignableFrom(property.PropertyType))
                    .ToDictionary(property => property.Name, property => (IElement)property.GetValue(this));
                var elementFields = GetType().GetFields(bindingFlags).Where(field => typeof(IElement).IsAssignableFrom(field.FieldType))
                    .ToDictionary(field => elementProperties.Keys.Any(
                        key => key.Equals(field.Name, StringComparison.InvariantCultureIgnoreCase)) ? $"_{field.Name}" : field.Name,
                    field => (IElement)field.GetValue(this));
                return elementFields.Concat(elementProperties)
                    .ToDictionary(el => el.Key, el => el.Value);
            }
        }

        /// <summary>
        /// List of pairs uniqueName-element from all fields and properties of type <see cref="IElement"/>,
        /// which were initialized as <see cref="ElementState.Displayed"/>.
        /// </summary>
        protected IDictionary<string, IElement> ElementsInitializedAsDisplayed => AllElements
            .Where(element => element.Value is Element && (element.Value as Element).elementState == ElementState.Displayed)
            .ToDictionary(el => el.Key, el => el.Value);

        /// <summary>
        /// List of pairs uniqueName-element from all fields and properties of type <see cref="IElement"/>,
        /// which are currently displayed (using <see cref="Selenium.Core.Elements.Interfaces.IElementStateProvider.IsDisplayed"/>).
        /// </summary>
        protected IDictionary<string, IElement> DisplayedElements => AllElements.Where(element => element.Value.State.IsDisplayed)
            .ToDictionary(el => el.Key, el => el.Value);
    }
}
