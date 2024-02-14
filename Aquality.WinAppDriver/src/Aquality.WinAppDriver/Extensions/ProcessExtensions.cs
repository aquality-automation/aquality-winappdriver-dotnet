using Aquality.WinAppDriver.Applications;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Aquality.WinAppDriver.Extensions
{
    public static class ProcessExtensions
    {
        /// <summary>
        /// Sends ShowCommand to the window associated with the specified process.
        /// </summary>
        /// <param name="process">Process to send the command.</param>
        /// <param name="showCommand">A command that controls how the window is to be shown.</param>
        public static void ShowWindow(this Process process, ShowCommand showCommand)
        {
            AqualityServices.LocalizedLogger.Info("loc.process.runcommand", process.ProcessName, $"{nameof(ShowWindow)}: {showCommand}");
            var intPtr = process.MainWindowHandle.ToInt32();
            ShowWindow(intPtr, Convert.ToInt32(showCommand));
        }

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
    }
}
