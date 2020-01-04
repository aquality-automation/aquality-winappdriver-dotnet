using System;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Elements
{
    public class WindowsElementFinder : RelativeElementFinder
    {
        public WindowsElementFinder(ILocalizedLogger logger, ConditionalWait conditionalWait, Func<ISearchContext> searchContextSupplier) 
            : base(logger, conditionalWait, searchContextSupplier)
        {
            Logger = logger;
            ConditionalWait = conditionalWait;
            SearchContextSupplier = searchContextSupplier;
        }

        private ILocalizedLogger Logger { get; }

        private ConditionalWait ConditionalWait { get; }

        private Func<ISearchContext> SearchContextSupplier { get; }

        public override IWebElement FindElement(By locator, Func<IWebElement, bool> elementStateCondition, string stateName, TimeSpan? timeout = null)
        {
            IWebElement element = null;
            if (!ConditionalWait.WaitFor(() =>
            {
                try
                {
                    element = SearchContextSupplier().FindElement(locator);
                    return elementStateCondition(element);
                }
                catch (WebDriverException)
                {
                    return false;
                }
            }, timeout))
            {
                if (element != null)
                {
                    Logger.Debug("loc.elements.were.found.but.not.in.state", null, locator.ToString(), stateName);
                }
                else
                {
                    Logger.Debug("loc.no.elements.found.by.locator", null, locator.ToString());
                }
                throw new NoSuchElementException($"No elements with locator '{locator.ToString()}' were found in {stateName} state");
            }

            return element;
        }
    }
}
