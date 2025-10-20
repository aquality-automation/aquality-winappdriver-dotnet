using Aquality.Selenium.Core.Elements;
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
        private IButton MoreButton { get; }
        private ILabel RestorePagesLabel { get; }
        private IButton CloseButton { get; }
        private IButton DontSignInButton { get; }
        public ChromeNavigationPanel() : base(By.TagName("Pane"), "Chrome Navigation panel")
        {
            NoThanksButton = ElementFactory.GetButton(MobileBy.AccessibilityId("declineButton"), "No thanks", ElementState.ExistsInAnyState);
            GotItButton = ElementFactory.GetButton(MobileBy.AccessibilityId("ackButton"), "Got it", ElementState.ExistsInAnyState);
            MoreButton = ElementFactory.GetButton(MobileBy.AccessibilityId("moreButton"), "More");
            RestorePagesLabel = ElementFactory.GetLabel(By.Name("Restore pages?"), "Restore pages?");
            CloseButton = RestorePagesLabel.FindChildElement<IButton>(By.Name("Close"), "Close");
            DontSignInButton = ElementFactory.GetButton(MobileBy.AccessibilityId("declineSignInButton"), "Don't sign in");
        }

        public bool IsSignInPresent => DontSignInButton.State.WaitForDisplayed();

        public void DontSignIn()
        {
            DontSignInButton.Click();
        }

        public void ClosePopUps()
        {
            State.WaitForExist();
            if (NoThanksButton.State.WaitForExist())
            {
                NoThanksButton.Click();
                NoThanksButton.State.WaitForNotExist();
            }
            if (MoreButton.State.IsDisplayed)
            {
                MoreButton.Click();
                MoreButton.MouseActions.Scroll(1000, ScrollDirection.Vertical);
            }
            if (GotItButton.State.WaitForExist())
            {
                GotItButton.MouseActions.MoveToElement();
                GotItButton.Click();
            }
            ConditionalWait.WaitForTrue(() =>
            {
                if (RestorePagesLabel.State.IsExist)
                {
                    RestorePagesLabel.Click();
                    CloseButton.Click();
                }

                return !RestorePagesLabel.State.IsExist;
            }, message: "Restore pages? popup must be closed to proceed");
            
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
