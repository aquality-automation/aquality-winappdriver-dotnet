using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Extensions;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.WinAppDriver.Tests.Actions
{
    public class KeyboardActionsTests : TestWithApplication
    {
        private const string ValueToSend = "abc";
        private const string Semicolon = ";";
        private const string Colon = ":";

        protected virtual IKeyboardActions KeyboardActions => AqualityServices.KeyboardActions;

        protected ITextBox RightArgumentTextBox => new CalculatorForm().RightArgumentTextBox;

        protected static readonly ModifierKey[] modifierKeys = Enum.GetValues(typeof(ModifierKey)) as ModifierKey[];

        protected static readonly ActionKey[] actionKeys = Enum.GetValues(typeof(ActionKey)) as ActionKey[];

        protected static readonly Action<IKeyboardActions>[] actionToMinimizeWindow = new Action<IKeyboardActions>[] 
        { 
            keyboardActions => keyboardActions.SendKeyWithWindowsKeyHold('d'),
            keyboardActions => keyboardActions.SendKeyWithWindowsKeyHold(ActionKey.Down)
        };

        [Test]
        public void Should_SendKeys_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual(ValueToSend, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendClosingKeys_ViaKeyboardActions()
        {
            Assert.DoesNotThrow(() => KeyboardActions.SendKeysWithKeyHold(Keys.F4, ModifierKey.Alt, mayDisappear: false));
        }

        [Test]
        public void Should_SendKey_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ActionKey.Semicolon);
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual(Semicolon + ValueToSend, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKey_AfterSequence_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeys(ValueToSend, ActionKey.Semicolon);
            Assert.AreEqual(ValueToSend + Semicolon, RightArgumentTextBox.Value);
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
            KeyboardActions.SendKeys(ActionKey.Semicolon);
            KeyboardActions.ReleaseKey(ModifierKey.Shift);
            Assert.AreEqual(Colon, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKeysWithKeyHold_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeysWithKeyHold(Semicolon, ModifierKey.Shift);
            Assert.AreEqual(Colon, RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKeyWithWindowsKeyHold_ToMinimizeWindows([ValueSource(nameof(actionToMinimizeWindow))] Action<IKeyboardActions> minimizeAction)
        {
            var window = new CalculatorForm();
            Assume.That(window.State.WaitForDisplayed(), "App window must be opened before interactions");
            minimizeAction(KeyboardActions);
            Assert.IsTrue(window.State.WaitForNotDisplayed(), "Window should be minimized after the minimize action");
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
