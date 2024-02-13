using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using NUnit.Framework;
using System;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class WindowTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Акно";

        protected override ICalculatorForm CalculatorForm => new CalculatorWindow();

        protected override ITestForm TestForm => new TestWindow(Locator, PageName);

        private static ElementFactory RootElementFactory => new(
            AqualityServices.ConditionalWait,
            new WindowsElementFinder(
                AqualityServices.LocalizedLogger,
                AqualityServices.ConditionalWait,
                () => AqualityServices.Application.RootSession),
            AqualityServices.Get<ILocalizationManager>(), 
            driverSessionSupplier: () => AqualityServices.Application.RootSession
            );

        [SetUp]
        public void SetUp()
        {
            AqualityServices.Logger.Debug($"Starting {AqualityServices.Application.Driver.Title}");
        }

        [Test]
        public void Should_FindChildElements_FromTheRootSessionElement()
        {
            var parentElement = RootElementFactory.GetLabel(CalculatorLocators.WindowLocator, "Calc window");
            var childElement = parentElement.FindChildElement<IButton>(CalculatorLocators.LeftArgumentTextBox);
            Assert.IsTrue(childElement.State.WaitForDisplayed());
            Assert.DoesNotThrow(() => childElement.MouseActions.Click());
            Assert.DoesNotThrow(() => childElement.MouseActions.DoubleClick());
            Assert.DoesNotThrow(() => childElement.MouseActions.ContextClick(modifierKeys: [ModifierKey.Ctrl, ModifierKey.Shift], interClickDelay: TimeSpan.FromSeconds(0.2)));
            Assert.DoesNotThrow(() => childElement.MouseActions.Scroll(10));
        }
    }
}