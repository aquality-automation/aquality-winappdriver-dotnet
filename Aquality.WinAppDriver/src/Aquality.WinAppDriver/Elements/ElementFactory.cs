using System;
using System.Collections.Generic;
using System.Reflection;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
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
