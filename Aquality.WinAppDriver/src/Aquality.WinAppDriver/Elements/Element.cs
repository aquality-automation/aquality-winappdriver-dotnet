using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using IKeyboardActions = Aquality.WinAppDriver.Actions.IKeyboardActions;
using CoreElement = Aquality.Selenium.Core.Elements.Element;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using CoreElementFinder = Aquality.Selenium.Core.Elements.Interfaces.IElementFinder;
using OpenQA.Selenium.Appium.Windows;
using System;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Visualization;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.WinAppDriver.Elements
{
    public abstract class Element : CoreElement, IElement
    {
        private static readonly IDictionary<string, Func<string, By>> LocatorConversionMap = new Dictionary<string, Func<string, By>>
        {
            { "By.Name", name => MobileBy.Name(name) },
            { "By.ClassName[Contains]", name => MobileBy.ClassName(name) }
        };
        private readonly Func<ISearchContext> searchContextSupplier;
        internal readonly ElementState elementState;

        protected Element(
            By locator, 
            string name, 
            Func<ISearchContext> searchContextSupplier = null,
            Func<WindowsDriver> customSessionSupplier = null,
            ElementState elementState = ElementState.ExistsInAnyState) 
            : base(ConvertCssSelector(locator), name, elementState)
        {
            this.searchContextSupplier = searchContextSupplier;
            WindowsDriverSupplier = customSessionSupplier ?? (() => AqualityServices.Application.Driver);
            this.elementState = elementState;
        }

        /// <summary>
        /// Workaround to support By.Name and By.ClassName locator strategies.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        internal static By ConvertCssSelector(By locator)
        {
            if ("css selector" == locator.Mechanism)
            {
                var locatorString = locator.ToString();
                var supportedLocatorType = LocatorConversionMap.Keys.FirstOrDefault(locType => locatorString.StartsWith(locType));
                if (supportedLocatorType == null)
                {
                    return locator;
                }
                var name = locatorString.Substring(locatorString.IndexOf(':') + 1).Trim();
                return LocatorConversionMap[supportedLocatorType](name);
            }
            return locator;
        }

        protected override IElementActionRetrier ActionRetrier => AqualityServices.Get<IElementActionRetrier>();

        protected override IApplication Application => AqualityServices.Application;

        protected override IElementCacheConfiguration CacheConfiguration => AqualityServices.Get<IElementCacheConfiguration>();

        protected override IConditionalWait ConditionalWait => AqualityServices.ConditionalWait;

        protected override CoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => new ElementFactory(ConditionalWait, Finder, LocalizationManager, driverSessionSupplier: WindowsDriverSupplier);

        public virtual Func<WindowsDriver> WindowsDriverSupplier { get; }

        public virtual IKeyboardActions KeyboardActions => new KeyboardActions(this, ElementType, WindowsDriverSupplier, LocalizedLogger, ActionRetrier);

        public virtual IMouseActions MouseActions => new MouseActions(this, ElementType, WindowsDriverSupplier, LocalizedLogger, ActionRetrier);

        protected override CoreElementFinder Finder => new WindowsElementFinder(LocalizedLogger, ConditionalWait, searchContextSupplier ?? WindowsDriverSupplier);

        protected override ILocalizedLogger LocalizedLogger => AqualityServices.LocalizedLogger;

        protected override ILocalizationManager LocalizationManager => AqualityServices.Get<ILocalizationManager>();

        protected override IImageComparator ImageComparator => AqualityServices.Get<IImageComparator>();

        public new AppiumElement GetElement(TimeSpan? timeout = null)
        {
            return (AppiumElement) base.GetElement(timeout);
        }
    }
}
