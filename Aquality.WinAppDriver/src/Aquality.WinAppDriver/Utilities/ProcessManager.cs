using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace Aquality.WinAppDriver.Utilities
{
    /// <summary>
    /// Implementation of <see cref="IProcessManager"/>
    /// </summary>
    public class ProcessManager : IProcessManager
    {
        private readonly ILocalizedLogger localizedLogger;

        public ProcessManager(ILocalizedLogger localizedLogger)
        {
            this.localizedLogger = localizedLogger;
        }

        public bool IsExecutableRunning(string name)
        {
            var pureName = GetPureExecutableName(name);
            return IsProcessRunning(pureName);
        }

        public bool IsProcessRunning(string name)
        {
            return Process.GetProcessesByName(name).Any();
        }

        public bool TryToStopExecutables(string name)
        {
            var pureName = GetPureExecutableName(name);
            return TryToStopProcesses(pureName);
        }

        public bool TryToStopProcesses(string name)
        {
            localizedLogger.Info("loc.processes.stop", name);
            var processes = Process.GetProcessesByName(name);
            var result = processes.Any();
            if (!result)
            {
                localizedLogger.Warn("loc.processes.notfound", name);
            }
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                    process.Dispose();
                }
                catch (SystemException e)
                {
                    Logger.Instance.Warn(e.Message);
                    result = false;
                }
            }

            return result;
        }

        private static string GetPureExecutableName(string executableName)
        {
            return executableName.Replace(".exe", string.Empty);
        }

        public Process Start(string fileName)
        {
            localizedLogger.Info("loc.processes.start", fileName);
            return Process.Start(fileName);
        }
    }
}
