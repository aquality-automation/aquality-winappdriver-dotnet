namespace Aquality.WinAppDriver.Tests.Forms
{
    public class RelativeFormTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Форма";

        protected override ICalculatorForm CalculatorForm => new CalculatorFormWithRelativeElements();

        protected override ITestForm TestForm => new TestForm(Locator, PageName);
    }
}