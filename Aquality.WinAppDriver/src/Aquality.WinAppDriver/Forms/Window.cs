using Aquality.WinAppDriver.Applications;
using OpenQA.Selenium;
using WindowsDriverSupplier = System.Func<OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>>;

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
        private static WindowsDriverSupplier ResolveWindowsSessionSupplier(WindowsDriverSupplier customSessionSupplier)
        {
            return customSessionSupplier ?? (() => AqualityServices.Application.RootSession);
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.window");
    }
}
