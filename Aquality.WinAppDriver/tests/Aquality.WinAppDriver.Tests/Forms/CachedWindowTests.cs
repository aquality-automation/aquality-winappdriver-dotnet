using Aquality.WinAppDriver.Applications;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CachedWindowTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Акно";

        protected override ICalculatorForm CalculatorForm { get; }

        protected override ITestForm TestForm { get; }

        public CachedWindowTests()
        {
            CalculatorForm = new CalculatorWindowWithCachedElements();
            TestForm = new TestWindow(Locator, PageName);
        }

        [SetUp]
        public void SetUp()
        {
            AqualityServices.Application.Launch();
        }
    }
}