using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using IKeyboardActions = Aquality.WinAppDriver.Actions.IKeyboardActions;
using CoreElement = Aquality.Selenium.Core.Elements.Element;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using CoreElementFinder = Aquality.Selenium.Core.Elements.Interfaces.IElementFinder;
using OpenQA.Selenium.Appium.Windows;
using System;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Visualization;

namespace Aquality.WinAppDriver.Elements
{
    public abstract class Element : CoreElement, IElement
    {
        private readonly Func<ISearchContext> searchContextSupplier;
        internal readonly ElementState elementState;

        protected Element(
            By locator, 
            string name, 
            Func<ISearchContext> searchContextSupplier = null,
            Func<WindowsDriver<WindowsElement>> customSessionSupplier = null,
            ElementState elementState = ElementState.ExistsInAnyState) 
            : base(locator, name, elementState)
        {
            this.searchContextSupplier = searchContextSupplier;
            WindowsDriverSupplier = customSessionSupplier ?? (() => AqualityServices.Application.Driver);
            this.elementState = elementState;
        }

        protected override IElementActionRetrier ActionRetrier => AqualityServices.Get<IElementActionRetrier>();

        protected override IApplication Application => AqualityServices.Application;

        protected override IElementCacheConfiguration CacheConfiguration => AqualityServices.Get<IElementCacheConfiguration>();

        protected override IConditionalWait ConditionalWait => AqualityServices.ConditionalWait;

        protected override CoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => new ElementFactory(ConditionalWait, Finder, LocalizationManager, driverSessionSupplier: WindowsDriverSupplier);

        public virtual Func<WindowsDriver<WindowsElement>> WindowsDriverSupplier { get; }

        public virtual IKeyboardActions KeyboardActions => new KeyboardActions(this, ElementType, WindowsDriverSupplier, LocalizedLogger, ActionRetrier);

        public virtual IMouseActions MouseActions => new MouseActions(this, ElementType, WindowsDriverSupplier, LocalizedLogger, ActionRetrier);

        protected override CoreElementFinder Finder => new WindowsElementFinder(LocalizedLogger, ConditionalWait, searchContextSupplier ?? WindowsDriverSupplier);

        protected override ILocalizedLogger LocalizedLogger => AqualityServices.LocalizedLogger;

        protected override ILocalizationManager LocalizationManager => AqualityServices.Get<ILocalizationManager>();

        protected override IImageComparator ImageComparator => AqualityServices.Get<IImageComparator>();
    }
}
