using System.Diagnostics;

namespace Aquality.WinAppDriver.Utilities
{
    /// <summary>
    /// Provides ability to detect running processes and to stop them.
    /// </summary>
    public interface IProcessManager
    {
        /// <summary>
        /// Defines if any process with the specified name is running.
        /// </summary>
        /// <param name="name">Pure name of the process (e.g. for WinAppDriver.exe pure name is WinAppDriver).</param>
        /// <returns>True if any process by the name specified was found, false otherwise.</returns>
        bool IsProcessRunning(string name);

        /// <summary>
        /// Defines if any executable with the specified name is running.
        /// </summary>
        /// <param name="name">Name of the executable (e.g. WinAppDriver.exe).</param>
        /// <returns>True if any process was found by the specified executable name, false otherwise.</returns>
        bool IsExecutableRunning(string name);

        /// <summary>
        /// Starts a process resource by specifying the name of a document or application 
        /// file and associates the resource with a new System.Diagnostics.Process component.
        /// </summary>
        /// <param name="fileName">The name of a document or application file to run in the process.</param>
        /// <returns>A new <see cref="Process"/> that is associated with the process resource,
        /// or null if no process resource is started.</returns>
        /// <see cref="Process.Start(string)"/>
        Process Start(string fileName);

        /// <summary>
        /// Attempts to stop all processes with the specified name.
        /// </summary>
        /// <param name="name">Pure name of the process (e.g. for WinAppDriver.exe pure name is WinAppDriver).</param>
        /// <returns>True if any process was found and all of found processes were stopped successfully, false otherwise.</returns>
        bool TryToStopProcesses(string name);

        /// <summary>
        /// Attempts to stop all processes with the specified name.
        /// </summary>
        /// <param name="name">Name of the executable (e.g. WinAppDriver.exe).</param>
        /// <returns>True if any process was found and all of found processes were stopped successfully, false otherwise.</returns>
        bool TryToStopExecutables(string name);
    }
}
