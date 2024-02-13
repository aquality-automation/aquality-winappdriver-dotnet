using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Extensions;
using NUnit.Framework;
using System;
using System.IO;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class MultipleWindowsTest : TestWithCustomApplication
    {
        private static readonly string ProgramFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
        private static readonly string ProgramFilesX86 = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");
        private const string AppPathRelativeFromProgramFiles = @"Google\Chrome\Application\chrome.exe";
        protected override string ApplicationPath => File.Exists(Path.Combine(ProgramFilesX86, AppPathRelativeFromProgramFiles))
            ? Path.Combine(ProgramFilesX86, AppPathRelativeFromProgramFiles)
            : Path.Combine(ProgramFiles, AppPathRelativeFromProgramFiles);

        private static string NewTabName => $"New Tab{TabNamePostfix}";
        private static string DownloadsTabName => $"Downloads{TabNamePostfix}";

        private const string TabNamePostfix = " - Google Chrome";

        [Test]
        public void Should_BePossibleTo_CloseOneOfWindows()
        {
            OpenTwoWindows(out var firstWindow, out var secondWindow);

            secondWindow.Click();
            secondWindow.Close();
            secondWindow.State.WaitForNotDisplayed();
            Assert.IsFalse(secondWindow.State.IsDisplayed, "Second window is not closed");
            Assert.IsTrue(firstWindow.State.IsDisplayed, "First window is closed but should not");
        }

        [Test]
        public void Should_BePossibleTo_ShowAndHideWindow_ViaProcess()
        {
            OpenTwoWindows(out var firstWindow, out var secondWindow);

            var secondWindowProcess = secondWindow.Process;
            secondWindowProcess.ShowWindow(ShowCommand.Minimize);
            secondWindow.State.WaitForNotDisplayed();
            Assert.IsFalse(secondWindow.State.IsDisplayed, "Second window is not minimized");
            Assert.IsTrue(firstWindow.State.IsDisplayed, "First window is not displayed");
            secondWindowProcess.ShowWindow(ShowCommand.ShowNormal);
            secondWindow.State.WaitForDisplayed();
            Assert.IsTrue(secondWindow.State.IsDisplayed, "Second window is not shown");
        }

        private void OpenTwoWindows(out ChromeWindow firstWindow, out ChromeWindow secondWindow)
        {
            var appProcess = ProcessManager.Start(ApplicationPath);

            AqualityServices.SetWindowHandleApplicationFactory(rootSession => new CoreChromeWindow(rootSession).NativeWindowHandle);
            var navigationPanel = new ChromeNavigationPanel();
            navigationPanel.ClosePopUps();
            Assert.IsTrue(navigationPanel.State.WaitForDisplayed());
            var firstWindowName = AqualityServices.Application.Driver.Title;
            firstWindow = new ChromeWindow(firstWindowName);
            Assert.IsTrue(firstWindow.State.WaitForDisplayed(), $"{firstWindow.Name} window is not displayed");

            firstWindow.Click();
            navigationPanel.OpenDownloads();
            firstWindow = new ChromeWindow(DownloadsTabName);
            Assert.IsTrue(firstWindow.State.WaitForDisplayed(), $"First window is not displayed with the new name {firstWindow.Name}");

            navigationPanel.OpenNewWindow();
            secondWindow = new ChromeWindow(NewTabName);
            Assert.IsTrue(secondWindow.State.WaitForDisplayed(), $"Second window with the name {secondWindow.Name} is not displayed");
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
