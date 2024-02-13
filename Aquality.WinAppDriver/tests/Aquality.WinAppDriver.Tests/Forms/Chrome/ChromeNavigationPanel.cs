using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeNavigationPanel : Form
    {
        private IButton NoThanksButton { get; }
        private IButton GotItButton { get; }
        private ILabel RestorePagesLabel { get; }
        private IButton CloseButton { get; }
        private IButton DontSignInButton { get; }
        public ChromeNavigationPanel() : base(By.TagName("Pane"), $"Chrome Navigation panel")
        {
            NoThanksButton = ElementFactory.GetButton(By.Name("No thanks"), "No thanks");
            GotItButton = ElementFactory.GetButton(MobileBy.AccessibilityId("ackButton"), "Got it");
            RestorePagesLabel = ElementFactory.GetLabel(By.Name("Restore pages?"), "Restore pages?");
            CloseButton = RestorePagesLabel.FindChildElement<IButton>(By.Name("Close"), "Close");
            DontSignInButton = ElementFactory.GetButton(MobileBy.AccessibilityId("declineSignInButton"), "Don't sign in");
    }

        public void ClosePopUps()
        {
            if (!State.WaitForExist() && DontSignInButton.State.WaitForExist())
            {
                DontSignInButton.Click();
                State.WaitForExist();
            }
            if (NoThanksButton.State.IsExist)
            {
                NoThanksButton.Click();
                GotItButton.State.WaitForExist();
            }
            if (GotItButton.State.IsExist)
            {
                GotItButton.Click();
            }
            if (RestorePagesLabel.State.IsExist)
            {
                CloseButton.Click();
            }
        }

        public void OpenDownloads()
        {
            KeyboardActions.SendKeysWithKeyHold("J", ModifierKey.Control);
            State.WaitForDisplayed();
        }

        public void OpenNewWindow()
        {
            KeyboardActions.SendKeysWithKeyHold("N", ModifierKey.Control);
            State.WaitForDisplayed();
        }
    }
}
