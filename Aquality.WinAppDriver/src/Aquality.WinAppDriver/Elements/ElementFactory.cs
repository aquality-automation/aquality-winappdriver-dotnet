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

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
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
                return (locator, name, state) => (T)elementCntr.Invoke(new object[] { locator, name, customSearchContextSupplier, driverSessionSupplier, state });
            }
        }
    }
}
