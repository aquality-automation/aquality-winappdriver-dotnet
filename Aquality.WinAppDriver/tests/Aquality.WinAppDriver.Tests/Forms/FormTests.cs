using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public class FormTests : AbstractFormTests
    {
        protected override string ExpectedElementType => "Форма";

        protected override ICalculatorForm CalculatorForm { get; }

        protected override ITestForm TestForm { get; }

        public FormTests()
        {
            CalculatorForm = new CalculatorForm();
            TestForm = new TestForm(Locator, PageName);
        }

        [Test]
        public void Should_SaveAndCompareDump()
        {
            CalculatorForm.State.WaitForDisplayed();
            Assert.DoesNotThrow(() => CalculatorForm.Dump.Save(), "Dump should be saved without errors");
            Assert.That(() => CalculatorForm.Dump.Compare(), Is.EqualTo(0), "Dump should have no difference right after the saving");
            CalculatorForm.OneButton.Click();
            CalculatorForm.PlusButton.Click();
            CalculatorForm.TwoButton.Click();
            CalculatorForm.EqualsButton.Click();
            StringAssert.Contains("3", CalculatorForm.ResultsLabel.Text);
            Assert.That(() => CalculatorForm.Dump.Compare(), Is.GreaterThan(0), "Dump should have differences after some calculations");
        }
    }
}