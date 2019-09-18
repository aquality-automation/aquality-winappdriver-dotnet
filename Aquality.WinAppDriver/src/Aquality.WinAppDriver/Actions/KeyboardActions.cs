using Aquality.Selenium.Core.Localization;
using Aquality.WinAppDriver.Extensions;
using OpenQA.Selenium.Appium.Windows;
using System;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Implements Keyboard actions for the whole application.
    /// </summary>
    public class KeyboardActions : IKeyboardActions
    {
        private readonly LocalizationLogger localizationLogger;
        private readonly Func<WindowsDriver<WindowsElement>> windowsDriverSupplier;

        public KeyboardActions(LocalizationLogger localizationLogger, Func<WindowsDriver<WindowsElement>> windowsDriverSupplier)
        {
            this.localizationLogger = localizationLogger;
            this.windowsDriverSupplier = windowsDriverSupplier;
        }

        public void PressKey(string keyToPress)
        {
            LogAction("loc.keyboard.presskey", keyToPress.GetLoggableValueForKeyboardKey());
            PerformAction(actions => actions.KeyDown(keyToPress));
        }

        public void ReleaseKey(string keyToRelease)
        {
            LogAction("loc.keyboard.releasekey", keyToRelease.GetLoggableValueForKeyboardKey());
            PerformAction(actions => actions.KeyUp(keyToRelease));
        }

        public void SendKeys(string keySequence)
        {
            LogAction("loc.keyboard.sendkeys", keySequence);
            PerformAction(actions => actions.SendKeys(keySequence));
        }

        public void SendKeysWithKeyHold(string keySequence, string keyToHold)
        {
            LogAction("loc.keyboard.sendkeys.withkeyhold", keySequence, keyToHold.GetLoggableValueForKeyboardKey());
            PerformAction(actions => actions.KeyDown(keyToHold).SendKeys(keySequence).KeyUp(keyToHold));
        }

        /// <summary>
        /// Performs submitted action against new <see cref="SeleniumActions"/> object.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        protected virtual void PerformAction(Func<SeleniumActions, SeleniumActions> action)
        {
            action(new SeleniumActions(windowsDriverSupplier())).Build().Perform();
        }

        /// <summary>
        /// Logs keyboard action in specific format.
        /// </summary>
        /// <param name="messageKey">Key of the localized message.</param>
        /// <param name="args">Arguments for the localized message.</param>
        protected virtual void LogAction(string messageKey, params object[] args)
        {
            localizationLogger.Info(messageKey, args);
        }
    }
}
