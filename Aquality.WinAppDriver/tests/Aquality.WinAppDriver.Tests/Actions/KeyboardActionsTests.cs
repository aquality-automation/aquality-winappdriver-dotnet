using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Tests.Windows;
using NUnit.Framework;
using System;

namespace Aquality.WinAppDriver.Tests.Actions
{
    public class KeyboardActionsTests : TestWithApplication
    {
        private const string ValueToSend = "abc";

        protected virtual IKeyboardActions KeyboardActions => ApplicationManager.GetRequiredService<IKeyboardActions>();

        protected ITextBox RightArgumentTextBox => new CalculatorWindow().RightArgumentTextBox;

        protected static readonly ModifierKey[] modifierKeys = Enum.GetValues(typeof(ModifierKey)) as ModifierKey[];

        protected static readonly ActionKey[] actionKeys = Enum.GetValues(typeof(ActionKey)) as ActionKey[];

        [Test]
        public void Should_SendKeys_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual(ValueToSend, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKey_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ActionKey.Equal);
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual("=" + ValueToSend, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKey_AfterSequence_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ValueToSend, ActionKey.Equal);
            Assert.AreEqual(ValueToSend + "=", RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKey_SeveralTimes_ViaKeyboardActions()
        {
            const int numberOfCharsToDelete = 2;
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ValueToSend);
            KeyboardActions.SendKeys(ActionKey.Backspace, times: numberOfCharsToDelete);
            Assert.AreEqual(ValueToSend.Substring(0, ValueToSend.Length - numberOfCharsToDelete), RightArgumentTextBox.Value);
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
        public void Should_NotThrow_WhenHoldModifierKeys_ViaKeyboardActions([ValueSource(nameof(modifierKeys))] ModifierKey modifierKey)
        {
            RightArgumentTextBox.Click();
            Assert.DoesNotThrow(() => KeyboardActions.SendKeysWithKeyHold(ValueToSend, modifierKey));
        }

        [Test]
        public void Should_NotThrow_WhenSendKeyTwice_ViaKeyboardActions([ValueSource(nameof(actionKeys))] ActionKey actionKey)
        {
            RightArgumentTextBox.Click();
            Assert.DoesNotThrow(() => KeyboardActions.SendKeys(actionKey, times: 2));
        }
    }
}
