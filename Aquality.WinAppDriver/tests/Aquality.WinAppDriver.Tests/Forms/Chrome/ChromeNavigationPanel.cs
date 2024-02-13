using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeNavigationPanel : Form
    {
        private IButton NoThanksButton => ElementFactory.GetButton(By.Name("No thanks"), "No thanks");
        private IButton GotItButton => ElementFactory.GetButton(By.Name("Got it"), "Got it");
        private ILabel RestorePagesLabel => ElementFactory.GetLabel(By.Name("Restore pages?"), "Restore pages?");
        private IButton CloseButton => RestorePagesLabel.FindChildElement<IButton>(By.Name("Close"), "Close");
        private IButton DontSignInButton => ElementFactory.GetButton(By.Name("Don't sign in"), "Don't sign in");
        public ChromeNavigationPanel() : base(By.Name("Chrome"), $"Chrome Navigation panel")
        {
        }

        public void ClosePopUps()
        {
            if (!State.WaitForDisplayed() && DontSignInButton.State.IsExist)
            {
                DontSignInButton.Click();
            }
            if (NoThanksButton.State.IsExist)
            {
                NoThanksButton.Click();
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
        }

        public void OpenNewWindow()
        {
            KeyboardActions.SendKeysWithKeyHold("N", ModifierKey.Control);
        }
    }
}
