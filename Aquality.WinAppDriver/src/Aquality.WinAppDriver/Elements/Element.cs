using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;
using CoreElement = Aquality.Selenium.Core.Elements.Element;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using CoreElementFinder = Aquality.Selenium.Core.Elements.Interfaces.IElementFinder;

namespace Aquality.WinAppDriver.Elements
{
    public abstract class Element : CoreElement
    {
        protected Element(By locator, string name) : base(locator, name, ElementState.Displayed)
        {
        }

        protected override ElementActionRetrier ActionRetrier => ApplicationManager.GetRequiredService<ElementActionRetrier>();

        protected override IApplication Application => ApplicationManager.Application;

        protected override ConditionalWait ConditionalWait => ApplicationManager.GetRequiredService<ConditionalWait>();

        protected override CoreElementFactory Factory => CustomFactory;

        protected IElementFactory CustomFactory => ApplicationManager.GetRequiredService<IElementFactory>();

        public T FindChildElement<T>(By childLocator, ElementSupplier<T> supplier = null) where T : IElement
        {
            return FindChildElement(childLocator, supplier, ElementState.Displayed);
        }

        protected override CoreElementFinder Finder => ApplicationManager.GetRequiredService<CoreElementFinder>();

        protected override LocalizationLogger LocalizationLogger => ApplicationManager.GetRequiredService<LocalizationLogger>();

        protected LocalizationManager LocalizationManager => ApplicationManager.GetRequiredService<LocalizationManager>();
    }
}
