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
            if (ApplicationManager.IsApplicationStarted())
            {
                ApplicationManager.Application.Quit();
            }
            ApplicationManager.TryToStopAppiumLocalService();
        }
    }
}
