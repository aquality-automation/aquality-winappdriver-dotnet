using Aquality.WinAppDriver.Applications;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class MultipleWindowsTest : TestWithCustomApplication
    {
        protected override string ApplicationPath => @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

        private string NewTabName => $"New Tab{TabNamePostfix}";
        private string DownloadsTabName => $"Downloads{TabNamePostfix}";

        private const string TabNamePostfix = " - Google Chrome";

        [Test]
        public void Should_BePossibleTo_WorkWithTwoWindows()
        {
            ProcessManager.Start(ApplicationPath);

            AqualityServices.SetWindowHandleApplicationFactory(rootSession => new CoreChromeWindow(rootSession).NativeWindowHandle);
            var navigationPanel = new ChromeNavigationPanel();
            Assert.IsTrue(navigationPanel.IsDisplayed);
            var firstTabName = AqualityServices.Application.Driver.Title;
            var firstWindow = new ChromeWindow(firstTabName);
            Assert.IsTrue(firstWindow.IsDisplayed, $"{firstWindow.Name} window is not displayed");
            firstWindow.Click();
            navigationPanel.OpenDownloads();
            firstWindow = new ChromeWindow(DownloadsTabName);
            Assert.IsTrue(firstWindow.IsDisplayed, $"First window is not displayed with the new name {firstWindow.Name}");
            navigationPanel.OpenNewTab();
            var secondWindow = new ChromeWindow(NewTabName);
            Assert.IsTrue(secondWindow.IsDisplayed, $"Second window with the name {secondWindow.Name} is not displayed");
            secondWindow.Click();
            secondWindow.Close();
            Assert.IsTrue(firstWindow.IsDisplayed);
        }

        [TearDown]
        public override void CleanUp()
        {
            var screenName = "screen.png";
            AqualityServices.Application.RootSession.GetScreenshot().SaveAsFile(screenName);
            TestContext.AddTestAttachment(screenName);
            //base.CleanUp();
        }
    }
}
