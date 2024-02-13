using Aquality.WinAppDriver.Applications;
using OpenQA.Selenium;
using WindowsDriverSupplier = System.Func<OpenQA.Selenium.Appium.Windows.WindowsDriver>;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines base class for a separate window of any application.
    /// </summary>
    public abstract class Window : Form
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the window.</param>
        /// <param name="name">Name of the window.</param>
        /// <param name="customSessionSupplier">Custom WinAppDriver session supplier.</param>
        protected Window(By locator, string name, WindowsDriverSupplier customSessionSupplier = null) 
            : base(locator, name, customSessionSupplier: ResolveWindowsSessionSupplier(customSessionSupplier))
        {
        }

        /// <summary>
        /// Returns native handle of the current window. 
        /// If the window element is top level window, this value could be used to start the driver for already running application, e.g.:
        /// class CoreChromeWindow(WindowsDriver rootSession) : Window(MobileBy.ClassName("Chrome_WidgetWin_1"), nameof(CoreChromeWindow), () => rootSession)
        /// AqualityServices.SetWindowHandleApplicationFactory(rootSession => new CoreChromeWindow(rootSession).NativeWindowHandle);
        /// </summary>
        public string NativeWindowHandle => int.Parse(GetElement().GetAttribute("NativeWindowHandle")).ToString("x");

        private static WindowsDriverSupplier ResolveWindowsSessionSupplier(WindowsDriverSupplier customSessionSupplier)
        {
            return customSessionSupplier ?? (() => AqualityServices.Application.RootSession);
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.window");
    }
}
