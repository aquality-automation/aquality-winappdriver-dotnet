using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;

namespace Aquality.WinAppDriver.Tests.Elements
{
    public class KeyboardActionsTests : TestWithApplication
    {
        private const string ValueToSend = "abc";
        private IKeyboardActions KeyboardActions => ApplicationManager.Application.KeyboardActions;

        private ITextBox RightArgumentTextBox => ApplicationManager.GetRequiredService<IElementFactory>().GetTextBox(MobileBy.AccessibilityId("49"), "Right Argument");

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
            KeyboardActions.PressKey(Keys.Shift);
            KeyboardActions.SendKeys(ValueToSend);
            KeyboardActions.ReleaseKey(Keys.Shift);
            Assert.AreEqual(ValueToSend.ToUpper(), RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_SendKeysWithKeyHold_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.SendKeysWithKeyHold(ValueToSend, Keys.Shift);
            Assert.AreEqual(ValueToSend.ToUpper(), RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_ReleaseKey_ViaKeyboardActions()
        {
            RightArgumentTextBox.Click();
            KeyboardActions.PressKey(Keys.Shift);
            KeyboardActions.SendKeys(ValueToSend);
            KeyboardActions.ReleaseKey(Keys.Shift);
            KeyboardActions.SendKeys(ValueToSend);
            Assert.AreEqual($"{ValueToSend.ToUpper()}{ValueToSend}", RightArgumentTextBox.Value);
        }

        [Test]
        public void Should_ThrowArgumentException_ForInvalidPressedKey_ViaKeyboardActions()
        {
            Assert.Throws<ArgumentException>(() => KeyboardActions.PressKey("invalid"));
        }

        [Test]
        public void Should_ThrowArgumentException_ForInvalidReleasedKey_ViaKeyboardActions()
        {
            Assert.Throws<ArgumentException>(() => KeyboardActions.ReleaseKey("invalid"));
        }

        [Test]
        public void Should_ThrowArgumentException_ForInvalidHoldedKey_ViaKeyboardActions()
        {
            Assert.Throws<ArgumentException>(() => KeyboardActions.SendKeysWithKeyHold(ValueToSend, "invalid"));
        }
    }
}
