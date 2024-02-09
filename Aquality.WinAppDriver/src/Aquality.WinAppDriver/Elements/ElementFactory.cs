using System;
using System.Collections.Generic;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
using CoreElement = Aquality.Selenium.Core.Elements.Interfaces.IElement;
using IElementFactory = Aquality.WinAppDriver.Elements.Interfaces.IElementFactory;
using System.Reflection;
using OpenQA.Selenium.Appium.Windows;
using System.Linq;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
        private static readonly IDictionary<string, string> LocatorToXPathTemplateMap = new Dictionary<string, string>
        {
            { "By.Name", "//*[@Name='{0}']" }
        };
        private readonly Func<ISearchContext> searchContextSupplier;
        private readonly Func<WindowsDriver> driverSessionSupplier;

        public ElementFactory(
            IConditionalWait conditionalWait, 
            IElementFinder elementFinder, 
            ILocalizationManager localizationManager, 
            Func<ISearchContext> searchContextSupplier = null,
            Func<WindowsDriver> driverSessionSupplier = null) 
            : base(conditionalWait, elementFinder, localizationManager)
        {
            this.searchContextSupplier = searchContextSupplier;
            this.driverSessionSupplier = driverSessionSupplier;
        }

        public virtual IButton GetButton(By locator, string name, ElementState elementState = ElementState.Displayed)
        {
            return GetCustomElement(ResolveSupplier<IButton>(null), locator, name, elementState);
        }

        public virtual ILabel GetLabel(By locator, string name, ElementState elementState = ElementState.Displayed)
        {
            return GetCustomElement(ResolveSupplier<ILabel>(null), locator, name, elementState);
        }

        public virtual ITextBox GetTextBox(By locator, string name, ElementState elementState = ElementState.Displayed)
        {
            return GetCustomElement(ResolveSupplier<ITextBox>(null), locator, name, elementState);
        }

        public override T FindChildElement<T>(CoreElement parentElement, By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed)
        {
            var elementSupplier = ResolveSupplier(supplier, () => parentElement.GetElement());
            return elementSupplier(childLocator, name ?? $"Child element of {parentElement.Name}", state);
        }

        protected override IDictionary<Type, Type> ElementTypesMap 
        {
            get
            {
                return new Dictionary<Type, Type>
                {
                    { typeof(IButton), typeof(Button) },
                    { typeof(ILabel), typeof(Label) },
                    { typeof(ITextBox), typeof(TextBox) }
                };
            }
        }

        protected override ElementSupplier<T> ResolveSupplier<T>(ElementSupplier<T> supplier)
        {
            return ResolveSupplier(supplier, searchContextSupplier);
        }

        /// <summary>
        /// Defines is the locator can be transformed to xpath or not.
        /// Base implementation works only with ByXPath.class and ByTagName locator types,
        /// for WinAppDriver By.Name support was added.
        /// </summary>
        /// <param name="locator">locator to transform</param>
        /// <returns>true if the locator can be transformed to xpath, false otherwise.</returns>
        protected override bool IsLocatorSupportedForXPathExtraction(By locator)
        {
            return LocatorToXPathTemplateMap.Keys.Any(locType => locator.ToString().StartsWith(locType))
                || base.IsLocatorSupportedForXPathExtraction(locator);
        }

        /// <summary>
        /// Extracts XPath from passed locator.
        /// Current implementation works only with ByXPath.class and ByTagName locator types,
        /// but you can implement your own for the specific WebDriver type.
        /// </summary>
        /// <param name="locator">locator to get xpath from.</param>
        /// <returns>extracted XPath.</returns>
        protected override string ExtractXPathFromLocator(By locator)
        {
            var locatorString = locator.ToString();
            var supportedLocatorType = LocatorToXPathTemplateMap.Keys.FirstOrDefault(locType => locatorString.StartsWith(locType));
            return supportedLocatorType == null
                ? base.ExtractXPathFromLocator(locator)
                : string.Format(LocatorToXPathTemplateMap[supportedLocatorType], locatorString.Substring(locatorString.IndexOf(':') + 1).Trim());
        }

        /// <summary>
        /// Workaround to support By.Name locator strategy.
        /// </summary>
        /// <param name="locator">Locator for element</param>
        /// <returns>In case of By.Name: returns formatted XPath; otherwise, returns not changed locator.</returns>
        protected virtual By ResolveLocator(By locator)
        {
            if ("css selector" == locator.Mechanism)
            {
                return By.XPath(ExtractXPathFromLocator(locator));
            }
            return locator;
        }

        /// <summary>
        /// Resolves element supplier or return itself if it is not null.
        /// </summary>
        /// <typeparam name="T">type of target element.</typeparam>
        /// <param name="supplier">target element supplier.</param>
        /// <param name="customSearchContextSupplier">Custom search context supplier to perform relative search for element.</param>
        /// <returns>non-null element supplier</returns>
        protected virtual ElementSupplier<T> ResolveSupplier<T>(ElementSupplier<T> supplier, Func<ISearchContext> customSearchContextSupplier)
            where T : CoreElement
        {
            if (supplier != null)
            {
                return supplier;
            }
            else
            {
                var type = typeof(T);
                var elementType = type.IsInterface ? ElementTypesMap[type] : type;
                var elementCntr = elementType.GetConstructor(
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance,
                        null,
                        new[] { typeof(By), typeof(string), typeof(Func<ISearchContext>), typeof(Func<WindowsDriver>), typeof(ElementState) },
                        null);
                if (elementCntr == null)
                {
                    return base.ResolveSupplier(supplier);
                }
                return (locator, name, state) => (T)elementCntr.Invoke(new object[] { ResolveLocator(locator), name, customSearchContextSupplier, driverSessionSupplier, state });
            }
        }
    }
}
