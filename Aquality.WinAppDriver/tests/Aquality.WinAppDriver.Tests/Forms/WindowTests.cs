using Aquality.WinAppDriver.Applications;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class WindowTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Акно";

        protected override ICalculatorForm CalculatorFormWithRelativeElements => new CalculatorWindow();

        protected override ITestForm TestForm => new TestWindow(Locator, PageName);

        [SetUp]
        public void SetUp()
        {
            AqualityServices.Logger.Debug($"Starting {AqualityServices.Application.Driver.Title}");
        }
    }
}