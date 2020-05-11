﻿using Aquality.WinAppDriver.Applications;
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
            Assert.IsTrue(navigationPanel.State.WaitForDisplayed());
            var firstWindowName = AqualityServices.Application.Driver.Title;
            var firstWindow = new ChromeWindow(firstWindowName);
            Assert.IsTrue(firstWindow.State.WaitForDisplayed(), $"{firstWindow.Name} window is not displayed");
            
            firstWindow.Click();
            navigationPanel.OpenDownloads();
            firstWindow = new ChromeWindow(DownloadsTabName);
            Assert.IsTrue(firstWindow.State.WaitForDisplayed(), $"First window is not displayed with the new name {firstWindow.Name}");
            
            navigationPanel.OpenNewWindow();
            var secondWindow = new ChromeWindow(NewTabName);
            Assert.IsTrue(secondWindow.State.WaitForDisplayed(), $"Second window with the name {secondWindow.Name} is not displayed");
            
            secondWindow.Click();
            secondWindow.Close();
            secondWindow.State.WaitForNotDisplayed();
            Assert.IsFalse(secondWindow.State.IsDisplayed, "Second window is not closed");
            Assert.IsTrue(firstWindow.State.IsDisplayed, "First window is closed but should not");
        }

        [TearDown]
        public override void CleanUp()
        {
            var screenName = "screen.png";
            AqualityServices.Application.RootSession.GetScreenshot().SaveAsFile(screenName);
            TestContext.AddTestAttachment(screenName);
            base.CleanUp();
        }
    }
}
