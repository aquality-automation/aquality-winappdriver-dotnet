using OpenQA.Selenium;
using System.Linq;
using System.Reflection;

namespace Aquality.WinAppDriver.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns name of the value from <see cref="Keys"/>, readable in the log.
        /// </summary>
        /// <param name="key">Keyboard key to define.</param>
        /// <returns>Readable value.</returns>
        public static string GetLoggableValueForKeyboardKey(this string key)
        {
            return typeof(Keys)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(field => key.Equals(field.GetValue(null)))?.Name ?? key;
        }
    }
}
