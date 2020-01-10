using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class WindowTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Акно";

        protected override ICalculatorForm CalculatorFormWithRelativeElements => new CalculatorWindow();

        protected override ITestForm TestForm => new TestWindow(Locator, PageName);

        private IElementFactory RootElementFactory => new ElementFactory(
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
            var childElement = parentElement.FindChildElement<IButton>(CalculatorLocators.OneButton);
            Assert.IsTrue(childElement.State.IsDisplayed);
            Assert.DoesNotThrow(() => childElement.MouseActions.Click());
        }
    }
}