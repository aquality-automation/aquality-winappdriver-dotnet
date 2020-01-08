using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Utilities;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests
{
    public abstract class TestWithCustomApplication : TestWithApplication
    {
        protected abstract string ApplicationPath { get; }

        protected IProcessManager ProcessManager => AqualityServices.ProcessManager;

        [TearDown]
        public virtual void CleanUp()
        {
            base.CleanUp();
            AqualityServices.SetDefaultFactory();
            var executableName = ApplicationPath.Contains('\\') ? ApplicationPath.Substring(ApplicationPath.LastIndexOf('\\') + 1) : ApplicationPath;
            ProcessManager.TryToStopExecutables(executableName);
        }
    }
}
