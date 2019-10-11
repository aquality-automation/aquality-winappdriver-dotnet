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

        public T FindChildElement<T>(Window parentWindow, By childLocator, string childName, ElementSupplier<T> supplier = null, ElementState elementState = ElementState.Displayed) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            return elementSupplier(new ByChained(parentWindow.Locator, childLocator), $"{childName}' - {parentWindow.GetElementType()} '{parentWindow.Name}", elementState);
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
