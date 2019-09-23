using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Windows
{
    public class WindowTests : TestWithApplication
    {
        private static readonly CalculatorWindowWithRelativeElements calculatorWindow = new CalculatorWindowWithRelativeElements();

        [Test]
        public void Should_WorkWithCalculator_ViaRelativeElements()
        {
            calculatorWindow.OneButton.Click();
            calculatorWindow.PlusButton.Click();
            calculatorWindow.TwoButton.Click();
            calculatorWindow.EqualsButton.Click();
            StringAssert.Contains("3", calculatorWindow.ResultsLabel.Text);
        }
    }
}
