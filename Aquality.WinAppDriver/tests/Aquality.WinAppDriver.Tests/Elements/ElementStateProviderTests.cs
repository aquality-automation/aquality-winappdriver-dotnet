using System;
using System.Diagnostics;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using IElementStateProvider = Aquality.Selenium.Core.Elements.Interfaces.IElementStateProvider;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class ElementStateProviderTests : TestWithApplication
    {
        private static readonly CalculatorForm CalculatorForm = new();
        private static ITextBox RightArgumentTextBox => CalculatorForm.RightArgumentTextBox;
        private static IButton EmptyButton => CalculatorForm.EmptyButton;
        private static readonly TimeSpan FromSeconds = TimeSpan.FromSeconds(5);

        private static IElement NotPresentLabel => AqualityServices.Get<IElementFactory>()
            .GetLabel(By.XPath("//*[@id='111111']"), "Not present element");

        private static Stopwatch StartedStopwatch
        {
            get
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                return stopwatch;
            }
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsDisplayed()
        {
            Assert.That(RightArgumentTextBox.State.IsDisplayed, Is.True);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotDisplayed()
        {
            Assert.That(NotPresentLabel.State.IsDisplayed, Is.False);
        }

        [Test]
        public void Should_ReturnFalse_IfElementDoesNotExist()
        {
            Assert.That(NotPresentLabel.State.IsExist, Is.False);
        }

        [Test]
        public void Should_ReturnTrue_IfElementExist()
        {
            Assert.That(RightArgumentTextBox.State.IsExist, Is.True);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotClickable()
        {
            Assert.That(EmptyButton.State.IsClickable, Is.False);
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsClickable()
        {
            Assert.That(RightArgumentTextBox.State.IsClickable, Is.True);
        }

        [Test]
        public void Should_ReturnFalse_IfElementIsNotEnabled()
        {
            Assert.That(EmptyButton.State.IsEnabled, Is.False);
        }

        [Test]
        public void Should_ReturnTrue_IfElementIsEnabled()
        {
            Assert.That(RightArgumentTextBox.State.IsEnabled, Is.True);
        }

        [Test]
        public void Should_ThrowException_IfWaitForClickableEnded()
        {
            Assert.Throws<WebDriverTimeoutException>(() => EmptyButton.State.WaitForClickable(TimeSpan.Zero));
        }

        [Test]
        public void Should_FinishWaitForClickable_AfterClickaleElementIsFound()
        {
            Assert.DoesNotThrow(() => RightArgumentTextBox.State.WaitForClickable(TimeSpan.Zero));
        }

        [Test]
        public void Should_ReturnFalse_InWaitForDisplayed_WhenElementIsNotDisplayed()
        {
            Assert.That(NotPresentLabel.State.WaitForDisplayed(TimeSpan.Zero), Is.False);
        }

        [Test]
        public void Should_ReturnTrue_InWaitForDisplayed_WhenElementIsDisplayed()
        {
            Assert.That(RightArgumentTextBox.State.WaitForDisplayed(TimeSpan.Zero), Is.True);
        }

        [Test]
        public void Should_ReturnFalse_InWaitForNotDisplayed_WhenElementIsDisplayed()
        {
            Assert.That(RightArgumentTextBox.State.WaitForNotDisplayed(TimeSpan.Zero), Is.False);
        }

        [Test]
        public void Should_ReturnTrue_InWaitForNotDisplayed_WhenElementIsNotDisplayed()
        {
            Assert.That(NotPresentLabel.State.WaitForNotDisplayed(TimeSpan.Zero), Is.True);
        }

        [Test]
        public void Should_ReturnFalse_InWaitForEnabled_WhenElementIsNotEnabled()
        {
            Assert.That(EmptyButton.State.WaitForEnabled(TimeSpan.Zero), Is.False);
        }

        [Test]
        public void Should_ReturnTrue_InWaitForEnabled_WhenElementIsEnabled()
        {
            Assert.That(RightArgumentTextBox.State.WaitForEnabled(TimeSpan.Zero), Is.True);
        }

        [Test]
        public void Should_ReturnFalse_InWaitForNotEnabled_WhenElementIsEnabled()
        {
            Assert.That(RightArgumentTextBox.State.WaitForNotEnabled(TimeSpan.Zero), Is.False);
        }

        [Test]
        public void Should_ReturnTrue_InWaitForNotEnabled_WhenElementIsNotEnabled()
        {
            Assert.That(EmptyButton.State.WaitForNotEnabled(TimeSpan.Zero), Is.True);
        }

        [Test]
        public void Should_ReturnFalse_InWaitForExist_WhenElementDoesNotExist()
        {
            Assert.That(NotPresentLabel.State.WaitForExist(TimeSpan.Zero), Is.False);
        }

        [Test]
        public void Should_ReturnTrue_InWaitForExist_WhenElementExists()
        {
            Assert.That(RightArgumentTextBox.State.WaitForExist(TimeSpan.Zero), Is.True);
        }

        [Test]
        public void Should_ReturnFalse_InWaitForNotExist_WhenElementExists()
        {
            Assert.That(RightArgumentTextBox.State.WaitForNotExist(TimeSpan.Zero), Is.False);
        }

        [Test]
        public void Should_ReturnTrue_InWaitForNotExist_WhenElementDoesNotExist()
        {
            Assert.That(NotPresentLabel.State.WaitForNotExist(TimeSpan.Zero), Is.True);
        }

        [Test]
        public void Should_WaitForClickable_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(EmptyButton.State, (state, time) =>
            {
                try
                {
                    EmptyButton.State.WaitForClickable(FromSeconds);
                }
                catch (WebDriverTimeoutException)
                {
                }
            });
        }

        [Test]
        public void Should_WaitForDisplayed_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(NotPresentLabel.State, (state, time) => state.WaitForDisplayed(time));
        }

        [Test]
        public void Should_WaitForEnabled_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(EmptyButton.State, (state, time) => state.WaitForEnabled(time));
        }

        [Test]
        public void Should_WaitForExist_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(NotPresentLabel.State, (state, time) => state.WaitForExist(time));
        }

        [Test]
        public void Should_WaitForNotExist_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(RightArgumentTextBox.State, (state, time) => state.WaitForNotExist(time));
        }

        [Test]
        public void Should_WaitForNotDisplayed_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(RightArgumentTextBox.State, (state, time) => state.WaitForNotDisplayed(time));
        }

        [Test]
        public void Should_WaitForNotEnabled_CorrectAmountOfTime()
        {
            AssertIfWaitTimeIsCorrect(RightArgumentTextBox.State, (state, time) => state.WaitForNotEnabled(time));
        }

        private static void AssertIfWaitTimeIsCorrect(IElementStateProvider stateProvider, Action<IElementStateProvider, TimeSpan> action)
        {
            var stopwatch = StartedStopwatch;
            action(stateProvider, FromSeconds);
            stopwatch.Stop();
            Assert.That(Math.Abs(stopwatch.Elapsed.Seconds - FromSeconds.Seconds), Is.LessThanOrEqualTo(1), "Wait Time is not correct");
        }
    }
}