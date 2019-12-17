using OpenQA.Selenium;
using System;
using System.Linq;
using System.Reflection;

namespace Aquality.WinAppDriver.Actions
{
    internal static class KeyExtensions
    {
        public static string GetKeysString(this ModifierKey modifierKey)
        {
            return GetKeysString<ModifierKey>(modifierKey);
        }

        public static string GetKeysString(this ActionKey actionKey, int times = 1)
        {
            return string.Concat(Enumerable.Repeat(GetKeysString(actionKey), times));
        }

        public static string GetKeysOrEmptyString(this ActionKey? actionKey, int times = 1)
        {
            return actionKey.HasValue 
                ? string.Concat(Enumerable.Repeat(GetKeysString(actionKey.Value), times)) 
                : string.Empty;
        }

        private static string GetKeysString<T>(T name) where T : struct, Enum
        {
            return typeof(Keys)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(field => field.Name == name.ToString())?.GetValue(null).ToString();
        }
    }
}
