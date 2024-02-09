using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeNavigationPanel : Form
    {
        public ChromeNavigationPanel() : base(By.XPath("//MenuItem[@Name = 'Chrome']"), $"Chrome Navigation panel")
        {
        }

        public void OpenDownloads()
        {
            KeyboardActions.SendKeysWithKeyHold("j", ModifierKey.Control);
        }

        public void OpenNewWindow()
        {
            KeyboardActions.SendKeysWithKeyHold("n", ModifierKey.Control);
        }
    }
}
