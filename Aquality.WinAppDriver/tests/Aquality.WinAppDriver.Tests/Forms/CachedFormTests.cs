namespace Aquality.WinAppDriver.Tests.Forms
{
    public class CachedFormTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Форма";

        protected override ICalculatorForm CalculatorForm { get; }

        protected override ITestForm TestForm { get; }

        public CachedFormTests()
        {
            CalculatorForm = new CalculatorFormWithCachedElements();
            TestForm = new TestForm(Locator, PageName);
        }
    }
}