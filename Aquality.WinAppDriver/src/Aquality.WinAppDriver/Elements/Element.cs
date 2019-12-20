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

namespace Aquality.WinAppDriver.Elements
{
    public abstract class Element : CoreElement, IElement
    {
        protected Element(By locator, string name, ElementState elementState = ElementState.Displayed) : base(locator, name, elementState)
        {
        }

        protected override ElementActionRetrier ActionRetrier => AqualityServices.Get<ElementActionRetrier>();

        protected override IApplication Application => AqualityServices.Application;

        protected virtual WindowsDriver<WindowsElement> WindowsDriver => AqualityServices.Application.Driver;

        protected override ConditionalWait ConditionalWait => AqualityServices.Get<ConditionalWait>();

        protected override CoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => AqualityServices.Get<IElementFactory>();

        public virtual IKeyboardActions KeyboardActions => new KeyboardActions(this, ElementType, () => WindowsDriver, LocalizedLogger, ActionRetrier);

        public virtual IMouseActions MouseActions => new MouseActions(this, ElementType, () => WindowsDriver, LocalizedLogger, ActionRetrier);

        protected override CoreElementFinder Finder => AqualityServices.Get<CoreElementFinder>();

        protected override ILocalizedLogger LocalizedLogger => AqualityServices.LocalizedLogger;

        protected ILocalizationManager LocalizationManager => AqualityServices.Get<ILocalizationManager>();
    }
}
