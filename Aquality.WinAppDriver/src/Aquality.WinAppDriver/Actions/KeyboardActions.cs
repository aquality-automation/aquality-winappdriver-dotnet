using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Appium.Windows;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;

namespace Aquality.WinAppDriver.Actions
{
    public class KeyboardActions : IKeyboardActions
    {
        private readonly LocalizationLogger localizationLogger;
        private readonly WindowsDriver<WindowsElement> windowsDriver;

        public KeyboardActions(LocalizationLogger localizationLogger, WindowsDriver<WindowsElement> windowsDriver)
        {
            this.localizationLogger = localizationLogger;
            this.windowsDriver = windowsDriver;
        }

        public void PressKey(string keyToPress)
        {
            localizationLogger.Info("loc.keyboard.presskey", keyToPress);
            new SeleniumActions(windowsDriver).KeyDown(keyToPress).Build().Perform();
        }

        public void ReleaseKey(string keyToRelease)
        {
            localizationLogger.Info("loc.keyboard.releasekey", keyToRelease);
            new SeleniumActions(windowsDriver).KeyUp(keyToRelease).Build().Perform();
        }

        public void SendKeys(string keySequence)
        {
            localizationLogger.Info("loc.keyboard.sendkeys", keySequence);
            new SeleniumActions(windowsDriver).SendKeys(keySequence).Build().Perform();
        }

        public void SendKeysWithKeyHold(string keySequence, string keyToHold)
        {
            localizationLogger.Info("loc.keyboard.sendkeys.withkeyhold", keySequence);
            new SeleniumActions(windowsDriver).KeyDown(keyToHold).SendKeys(keySequence).KeyUp(keyToHold).Build().Perform();
        }
    }
}
