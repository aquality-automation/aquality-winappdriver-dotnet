using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeNavigationPanel : Form
    {
        private IButton NoThanksButton => ElementFactory.GetButton(By.Name("No thanks"), "No thanks");
        private IButton GotItButton => ElementFactory.GetButton(MobileBy.AccessibilityId("ackButton"), "Got it");
        private ILabel RestorePagesLabel => ElementFactory.GetLabel(By.Name("Restore pages?"), "Restore pages?");
        private IButton CloseButton => RestorePagesLabel.FindChildElement<IButton>(By.Name("Close"), "Close");
        private IButton DontSignInButton => ElementFactory.GetButton(MobileBy.AccessibilityId("declineSignInButton"), "Don't sign in");
        public ChromeNavigationPanel() : base(By.Name("Chrome"), $"Chrome Navigation panel")
        {
        }

        public void ClosePopUps()
        {
            if (!State.WaitForExist() && DontSignInButton.State.WaitForExist())
            {
                DontSignInButton.Click();
            }
            if (NoThanksButton.State.IsExist)
            {
                NoThanksButton.Click();
            }
            if (RestorePagesLabel.State.IsExist)
            {
                CloseButton.Click();
            }
            if (GotItButton.State.IsExist)
            {
                GotItButton.Click();
            }
        }

        public void OpenDownloads()
        {
            KeyboardActions.SendKeysWithKeyHold("J", ModifierKey.Control);
        }

        public void OpenNewWindow()
        {
            KeyboardActions.SendKeysWithKeyHold("N", ModifierKey.Control);
        }
    }
}
