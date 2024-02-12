using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeNavigationPanel : Form
    {
        public ChromeNavigationPanel() : base(By.Name("Chrome"), $"Chrome Navigation panel")
        {
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
