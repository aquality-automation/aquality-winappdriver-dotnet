using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Configurations;
using Aquality.WinAppDriver.Utilities;
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
            if (ApplicationManager.GetRequiredService<IApplicationProfile>().IsRemote)
            {
                winAppDriverProcess =
                    ApplicationManager.GetRequiredService<IWinAppDriverLauncher>().StartWinAppDriverIfRequired();
            }            
        }

        [TearDown]
        public void CleanUp()
        {
            if (ApplicationManager.IsApplicationStarted())
            {
                ApplicationManager.Application.Quit();
            }
            ApplicationManager.TryToStopAppiumLocalService();
            if (winAppDriverProcess != null)
            {
                ApplicationManager.GetRequiredService<IWinAppDriverLauncher>().TryToStopWinAppDriver();
            }
        }
    }
}
