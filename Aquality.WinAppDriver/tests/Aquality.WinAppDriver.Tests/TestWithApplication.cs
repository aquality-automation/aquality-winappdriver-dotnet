using Aquality.WinAppDriver.Applications;
using NUnit.Framework;

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
                AqualityServices.Application.Quit();
            }
            AqualityServices.TryToStopAppiumLocalService();
        }
    }
}
