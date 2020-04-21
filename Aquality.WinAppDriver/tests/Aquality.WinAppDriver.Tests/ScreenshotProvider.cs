using Aquality.WinAppDriver.Applications;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Aquality.WinAppDriver.Tests
{
    internal class ScreenshotProvider
    {
        private readonly IWindowsApplication application;

        internal ScreenshotProvider(IWindowsApplication application)
        {
            this.application = application;
        }

        internal string TakeScreenshot()
        {
            var image = GetImage();
            var directory = Path.Combine(Environment.CurrentDirectory, "screenshots");
            EnsureDirectoryExists(directory);
            var screenshotName = $"{GetType().Name}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString("n").Substring(0, 5)}.png";
            var path = Path.Combine(directory, screenshotName);
            image.Save(path, ImageFormat.Png);
            return path;
        }

        private Image GetImage()
        {
            using (var stream = new MemoryStream(application.RootSession.GetScreenshot().AsByteArray))
            {
                return Image.FromStream(stream);
            }
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
