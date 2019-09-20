using OpenQA.Selenium;
using System.Linq;
using System.Reflection;

namespace Aquality.WinAppDriver.Actions
{
    /// <summary>
    /// Represents modifier keys which could be used in <see cref="IKeyboardActions"/>.
    /// Directly related to <see cref="Keys"/>
    /// </summary>
    public enum ModifierKey
    {
        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        Alt,
        /// <summary>
        /// Represents the function key COMMAND.
        /// </summary>
        Command,
        /// <summary>
        /// Represents the Control key.
        /// </summary>
        Control,
        /// <summary>
        /// Represents the Left Alt key.
        /// </summary>
        LeftAlt,
        /// <summary>
        /// Represents the Left Control key.
        /// </summary>
        LeftControl,
        /// <summary>
        /// Represents the Left Shift key.
        /// </summary>
        LeftShift,
        /// <summary>
        /// Represents the function key META.
        /// </summary>
        Meta,
        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        Shift
    }

    internal static class ModifierKeyExtensions
    {
        public static string GetKeysString(this ModifierKey modifierKey)
        {
            return typeof(Keys)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(field => field.Name == modifierKey.ToString())?.GetValue(null).ToString();
        }
    }
}
