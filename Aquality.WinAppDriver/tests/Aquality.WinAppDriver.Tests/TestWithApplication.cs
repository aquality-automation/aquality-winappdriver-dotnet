using Aquality.WinAppDriver.Applications;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Aquality.WinAppDriver.Tests
{
    [NonParallelizable]
    public class TestWithApplication
    {
        [TearDown]
        public virtual void CleanUp()
        {
            if (AqualityServices.IsApplicationStarted)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                {
                    TestContext.AddTestAttachment(new ScreenshotProvider(AqualityServices.Application).TakeScreenshot());
                }
                
                AqualityServices.Application.Quit();
            }
            AqualityServices.TryToStopAppiumLocalService();
        }
    }
}
