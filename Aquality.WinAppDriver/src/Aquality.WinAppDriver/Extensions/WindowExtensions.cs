using Aquality.WinAppDriver.Forms;

namespace Aquality.WinAppDriver.Extensions
{
    public static class WindowExtensions
    {
        public static string GetNativeWindowHandle(this Window window)
        {
            return int.Parse(window.GetElement().GetAttribute("NativeWindowHandle")).ToString("x");
        }
    }
}
