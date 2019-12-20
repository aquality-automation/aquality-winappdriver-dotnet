using System;
using System.Collections.Generic;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
using IElement = Aquality.WinAppDriver.Elements.Interfaces.IElement;
using IElementFactory = Aquality.WinAppDriver.Elements.Interfaces.IElementFactory;
using System.Reflection;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
        private readonly Func<ISearchContext> searchContextSupplier;
        private readonly bool isRootSession;

        public ElementFactory(
            ConditionalWait conditionalWait, 
            IElementFinder elementFinder, 
            ILocalizationManager localizationManager, 
            Func<ISearchContext> searchContextSupplier = null, 
            bool isRootSession = false) 
            : base(conditionalWait, elementFinder, localizationManager)
        {
            this.searchContextSupplier = searchContextSupplier;
            this.isRootSession = isRootSession;
        }

        public IButton GetButton(By locator, string name, ElementState elementState = ElementState.Displayed)
        {
            return GetCustomElement(ResolveSupplier<IButton>(null), locator, name, elementState);
        }

        public ILabel GetLabel(By locator, string name, ElementState elementState = ElementState.Displayed)
        {
            return GetCustomElement(ResolveSupplier<ILabel>(null), locator, name, elementState);
        }

        public ITextBox GetTextBox(By locator, string name, ElementState elementState = ElementState.Displayed)
        {
            return GetCustomElement(ResolveSupplier<ITextBox>(null), locator, name, elementState);
        }

        public T FindChildElement<T>(Form parentForm, By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            return elementSupplier(new ByChained(parentForm.Locator, childLocator), $"{childName}' - {parentForm.GetElementType()} '{parentForm.Name}", elementState);
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

        /// <summary>
        /// Resolves element supplier or return itself if it is not null
        /// </summary>
        /// <typeparam name="T">type of target element</typeparam>
        /// <param name="supplier">target element supplier</param>
        /// <returns>non-null element supplier</returns>
        protected override ElementSupplier<T> ResolveSupplier<T>(ElementSupplier<T> supplier)
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
                        new[] { typeof(By), typeof(string), typeof(Func<ISearchContext>), typeof(bool), typeof(ElementState) },
                        null);
                if (elementCntr == null)
                {
                    return base.ResolveSupplier(supplier);
                }
                return (locator, name, state) => (T) elementCntr.Invoke(new object[] { locator, name, searchContextSupplier, isRootSession, state });
            }
        }
    }
}
