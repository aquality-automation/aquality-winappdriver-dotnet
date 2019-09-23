using System;
using System.Collections.Generic;
using System.Reflection;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
using Aquality.WinAppDriver.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
using IElement = Aquality.WinAppDriver.Elements.Interfaces.IElement;
using IElementFactory = Aquality.WinAppDriver.Elements.Interfaces.IElementFactory;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
        public ElementFactory(ConditionalWait conditionalWait, IElementFinder elementFinder, LocalizationManager localizationManager) : base(conditionalWait, elementFinder, localizationManager)
        {
        }

        public IButton GetButton(By locator, string name)
        {
            return GetCustomElement(ResolveSupplier<IButton>(null), locator, name);
        }

        public ILabel GetLabel(By locator, string name)
        {
            return GetCustomElement(ResolveSupplier<ILabel>(null), locator, name);
        }

        public ITextBox GetTextBox(By locator, string name)
        {
            return GetCustomElement(ResolveSupplier<ITextBox>(null), locator, name);
        }

        public T FindChildElement<T>(Window parentWindow, By childLocator, string childName, ElementSupplier<T> supplier = null) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            return elementSupplier(new ByChained(parentWindow.Locator, childLocator), $"{childName}' - {parentWindow.GetElementType()} '{parentWindow.Name}", ElementState.Displayed);
        }

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
                        new[] { typeof(By), typeof(string) },
                        null);
                if(elementCntr == null)
                {
                    return base.ResolveSupplier(supplier);
                }
                return (locator, name, state) => (T)elementCntr.Invoke(new object[] { locator, name });
            }
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
    }
}
