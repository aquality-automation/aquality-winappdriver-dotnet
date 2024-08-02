using Aquality.Selenium.Core.Configurations;
using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Applications;
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
        private ILabel LastTextLabel { get; }
        private ILabel RestorePagesLabel { get; }
        private IButton CloseButton { get; }
        private IButton DontSignInButton { get; }
        public ChromeNavigationPanel() : base(By.TagName("Pane"), "Chrome Navigation panel")
        {
            NoThanksButton = ElementFactory.GetButton(By.Name("No thanks"), "No thanks");
            GotItButton = ElementFactory.GetButton(MobileBy.AccessibilityId("ackButton"), "Got it");
            LastTextLabel = ElementFactory.GetLabel(MobileBy.AccessibilityId("lastTextElement"), "Last element text");
            RestorePagesLabel = ElementFactory.GetLabel(By.Name("Restore pages?"), "Restore pages?");
            CloseButton = RestorePagesLabel.FindChildElement<IButton>(By.Name("Close"), "Close");
            DontSignInButton = ElementFactory.GetButton(MobileBy.AccessibilityId("declineSignInButton"), "Don't sign in");
        }

        public void DontSignIn()
        {
            DontSignInButton.Click();
        }

        public void ClosePopUps()
        {
            State.WaitForExist();
            if (!NoThanksButton.State.WaitForNotDisplayed())
            {
                NoThanksButton.Click();
                NoThanksButton.State.WaitForNotDisplayed();
                if (GotItButton.State.WaitForExist())
                {
                    LastTextLabel.Click();
                    KeyboardActions.SendKeys(ActionKey.Tab, times: 2);
                    KeyboardActions.SendKeys(ActionKey.Enter);
                }
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
