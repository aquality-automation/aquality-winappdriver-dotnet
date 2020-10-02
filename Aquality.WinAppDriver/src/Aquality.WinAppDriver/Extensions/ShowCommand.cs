namespace Aquality.WinAppDriver.Extensions
{
    /// <summary>
    /// Controls how the window is to be shown. 
    /// This parameter is ignored the first time an application calls ShowWindow, 
    /// if the program that launched the application provides a STARTUPINFO structure.
    /// Otherwise, the first time ShowWindow is called, 
    /// the value should be the value obtained by the WinMain function in its nCmdShow parameter.
    /// For more information, please visit https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow.
    /// </summary>
    public enum ShowCommand
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0,
        /// <summary>
        /// Activates and displays a window. 
        /// If the window is minimized or maximized, the system restores it to its original size and position. 
        /// An application should specify this flag when displaying the window for the first time.
        /// </summary>
        ShowNormal = 1,
        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        ShowMinimized = 2,
        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        ShowMaximized = 3,
        /// <summary>
        /// Displays a window in its most recent size and position. 
        /// This value is similar to <see cref="ShowNormal"/>, except that the window is not activated.
        /// </summary>
        ShowNoActivate = 4,
        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        Show = 5,
        /// <summary>
        /// Minimizes the specified window and activates the next top-level window in the Z order.
        /// </summary>
        Minimize = 6,
        /// <summary>
        /// Displays the window as a minimized window. 
        /// This value is similar to <see cref="ShowMinimized"/>, except the window is not activated.
        /// </summary>
        ShowMinimizedNoActive = 7,
        /// <summary>
        /// Displays the window in its current size and position. 
        /// This value is similar to <see cref="Show"/>, except that the window is not activated.
        /// </summary>
        ShowNoActive = 8,
        /// <summary>
        /// Activates and displays the window. 
        /// If the window is minimized or maximized, the system restores it to its original size and position. 
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        Restore = 9,
        /// <summary>
        /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure 
        /// passed to the CreateProcess function by the program that started the application.
        /// </summary>
        ShowDefault = 10,
        /// <summary>
        /// Minimizes a window, even if the thread that owns the window is not responding. 
        /// This flag should only be used when minimizing windows from a different thread.
        /// </summary>
        ForceMinimize = 11
    }
}
