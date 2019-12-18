using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Configurations;
using NUnit.Framework;
using System.Diagnostics;

namespace Aquality.WinAppDriver.Tests
{
    [NonParallelizable]
    public class TestWithApplication
    {
        private Process winAppDriverProcess;

        [SetUp]
        public void SetUp()
        {
            if (AqualityServices.Get<IApplicationProfile>().IsRemote)
            {
                winAppDriverProcess =
                    AqualityServices.WinAppDriverLauncher.StartWinAppDriverIfRequired();
            }            
        }

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsApplicationStarted)
            {
                AqualityServices.Application.Quit();
            }
            AqualityServices.TryToStopAppiumLocalService();
            if (winAppDriverProcess != null)
            {
                AqualityServices.WinAppDriverLauncher.TryToStopWinAppDriver();
            }
        }
    }
}
