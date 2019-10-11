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

namespace Aquality.WinAppDriver.Elements
{
    public abstract class Element : CoreElement, IElement
    {
        protected Element(By locator, string name, ElementState elementState = ElementState.Displayed) : base(locator, name, elementState)
        {
        }

        protected override ElementActionRetrier ActionRetrier => ApplicationManager.GetRequiredService<ElementActionRetrier>();

        protected override IApplication Application => ApplicationManager.Application;

        protected override ConditionalWait ConditionalWait => ApplicationManager.GetRequiredService<ConditionalWait>();

        protected override CoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => ApplicationManager.GetRequiredService<IElementFactory>();

        public virtual IKeyboardActions KeyboardActions => new KeyboardActions(this, ElementType, () => Application, LocalizationLogger, ActionRetrier);

        public virtual IMouseActions MouseActions => new MouseActions(this, ElementType, () => Application, LocalizationLogger, ActionRetrier);

        protected override CoreElementFinder Finder => ApplicationManager.GetRequiredService<CoreElementFinder>();

        protected override LocalizationLogger LocalizationLogger => ApplicationManager.GetRequiredService<LocalizationLogger>();

        protected LocalizationManager LocalizationManager => ApplicationManager.GetRequiredService<LocalizationManager>();
    }
}
