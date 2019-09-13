using Aquality.WinAppDriver.Applications;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests
{
    [NonParallelizable]
    public class TestWithApplication
    {
        [TearDown]
        public void CleanUp()
        {
            if (ApplicationManager.IsStarted)
            {
                ApplicationManager.Application.Driver.Quit();
            }
            ApplicationManager.TryToStopAppiumLocalService();
        }
    }
}
