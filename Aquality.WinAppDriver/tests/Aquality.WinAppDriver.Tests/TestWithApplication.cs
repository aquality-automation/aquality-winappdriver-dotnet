using Aquality.WinAppDriver.Applications;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Text;

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
                    File.WriteAllText("source.xml", AqualityServices.Application.Driver.PageSource, Encoding.UTF8);
                    TestContext.AddTestAttachment("source.xml");
                }
                
                AqualityServices.Application.Quit();
            }
            AqualityServices.TryToStopAppiumLocalService();
        }
    }
}
