using Aquality.Selenium.Core.Elements;
using Aquality.WinAppDriver.Elements.Interfaces;
using System.Reflection;

namespace Aquality.WinAppDriver.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IElement"/> classes.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// element.GetType().Name provides a non-localized name; 
        /// ElementType property is localized, but has the protected access and is not defined in the IElement interface.
        /// So current method allows to get ElementType if the current element is assignable from <see cref="Element"/>
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetElementType(this IElement element)
        {
            string elementType = null;
            if (typeof(Element).IsAssignableFrom(element.GetType()))
            {
                elementType = element.GetType().GetProperty("ElementType", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(element).ToString();
            }

            return elementType ?? element.GetType().Name;
        }
    }
}
