namespace Aquality.WinAppDriver.Tests.Forms
{
    public class FormTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Форма";

        protected override ICalculatorForm CalculatorFormWithRelativeElements =>
            new CalculatorFormWithRelativeElements();

        protected override ITestForm TestForm => new TestForm(Locator, PageName);
    }
}