using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeNavigationPanel : Form
    {
        public ChromeNavigationPanel() : base(By.XPath("//Pane[./Button[@Name='Chrome']]"), $"Chrome Navigation panel")
        {
        }

        public void OpenDownloads()
        {
            KeyboardActions.SendKeysWithKeyHold("j", ModifierKey.Control);
        }

        public void OpenNewTab()
        {
            KeyboardActions.SendKeysWithKeyHold("n", ModifierKey.Control);
        }
    }
}
