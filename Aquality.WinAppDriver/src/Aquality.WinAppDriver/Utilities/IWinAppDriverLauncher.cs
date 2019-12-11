using System.Diagnostics;

namespace Aquality.WinAppDriver.Utilities
{
    /// <summary>
    /// Provides ability to start and stop WinAppDriver
    /// </summary>
    public interface IWinAppDriverLauncher
    {
        /// <summary>
        /// Attepmts to find running WinAppDriver process; if no process found, starts it
        /// </summary>
        /// <returns>Instance of process if it was started or null if it wasn't.</returns>
        Process StartWinAppDriverIfRequired();

        /// <summary>
        /// Attempts to stop WinAppDriver process
        /// </summary>
        /// <returns>True if the process was found and stopped, false otherwise</returns>
        bool TryToStopWinAppDriver();
    }
}
