using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Applications.Locators;
using NUnit.Framework;
using System;

namespace Aquality.WinAppDriver.Tests.Actions
{
    public class KeyboardActionsTests : TestWithApplication
    {
        private const string ValueToSend = "abc";

        protected virtual IKeyboardActions KeyboardActions => ApplicationManager.GetRequiredService<IKeyboardActions>();

        protected ITextBox RightArgumentTextBox => new CalculatorWindow().RightArgumentTextBox;

        private static readonly ModifierKey[] modifierKeys = Enum.GetValues(typeof(ModifierKey)) as ModifierKey[];

        [Test]
        public void Should_SendKeys_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual(ValueToSend, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_PressKey_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.PressKey(ModifierKey.Shift);
            KeyboardActions.SendKeys(ValueToSend);
            KeyboardActions.ReleaseKey(ModifierKey.Shift);
            Assert.AreEqual(ValueToSend.ToUpper(), RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKeysWithKeyHold_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeysWithKeyHold(ValueToSend, ModifierKey.Shift);
            Assert.AreEqual(ValueToSend.ToUpper(), RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_ReleaseKey_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.PressKey(ModifierKey.Shift);
            KeyboardActions.SendKeys(ValueToSend);
            KeyboardActions.ReleaseKey(ModifierKey.Shift);
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual($"{ValueToSend.ToUpper()}{ValueToSend}", RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_NotThrow_WhenHoldModifierKeys_ViaKeyboardActions([ValueSource(nameof(modifierKeys))]ModifierKey modifierKey)
        {
            RightArgumentTextBox.Click();
            Assert.DoesNotThrow(() => KeyboardActions.SendKeysWithKeyHold(ValueToSend, modifierKey));
        }
    }
}
