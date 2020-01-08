using Aquality.WinAppDriver.Applications;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class MultipleWindowsTest : TestWithCustomApplication
    {
        protected override string ApplicationPath => @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        private string NewTabName => "New Tab";
        private string DownloadsTabName => "Downloads";


        [Test]
        public void Should_BePossibleTo_WorkWithTwoWindows()
        {
            ProcessManager.Start(ApplicationPath);

            AqualityServices.SetWindowHandleApplicationFactory(rootSession => new CoreChromeWindow(rootSession).NativeWindowHandle);
            var navigationPanel = new ChromeNavigationPanel();
            Assert.IsTrue(navigationPanel.IsDisplayed);
            var firstWindow = new ChromeWindow(NewTabName);
            Assert.IsTrue(firstWindow.IsDisplayed, $"{firstWindow.Name} window is not displayed");
            firstWindow.ClickOnDocument();
            navigationPanel.OpenDownloads();
            firstWindow = new ChromeWindow(DownloadsTabName);
            Assert.IsTrue(firstWindow.IsDisplayed, $"First window is not displayed with the new name {firstWindow.Name}");
            navigationPanel.OpenNewTab();
            var secondWindow = new ChromeWindow(NewTabName);
            Assert.IsTrue(secondWindow.IsDisplayed, $"Second window with the name {secondWindow.Name} is not displayed");
            secondWindow.Close();
            Assert.IsTrue(firstWindow.IsDisplayed);
        }
    }
}
