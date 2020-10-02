using Aquality.WinAppDriver.Extensions;
using System;
using System.Diagnostics;
using System.IO;

namespace Aquality.WinAppDriver.Utilities
{
    public class WinAppDriverLauncher : IWinAppDriverLauncher
    {
        private const string ExecutableName = "WinAppDriver.exe";
        private const string FolderName = "Windows Application Driver";

        private readonly IProcessManager processManager;

        public WinAppDriverLauncher(IProcessManager processManager)
        {
            this.processManager = processManager;
        }

        public Process StartWinAppDriverIfRequired()
        {
            Process result = null;
            if (!processManager.IsExecutableRunning(ExecutableName))
            {
                var exePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), FolderName, ExecutableName);
                result = processManager.Start(exePath);
                result.ShowWindow(ShowCommand.Minimize);
            }
            return result;
        }

        public bool TryToStopWinAppDriver()
        {
            return processManager.TryToStopExecutables(ExecutableName);
        }
    }
}
