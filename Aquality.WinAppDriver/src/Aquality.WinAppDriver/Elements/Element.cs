using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Core.Waitings;
using Aquality.WinAppDriver.Applications;
using OpenQA.Selenium;
using CoreElement = Aquality.Selenium.Core.Elements.Element;

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

        protected override IElementFactory Factory => ApplicationManager.GetRequiredService<IElementFactory>();

        protected override IElementFinder Finder => ApplicationManager.GetRequiredService<IElementFinder>();

        protected override LocalizationLogger LocalizationLogger => ApplicationManager.GetRequiredService<LocalizationLogger>();

        protected LocalizationManager LocalizationManager => ApplicationManager.GetRequiredService<LocalizationManager>();
    }
}
