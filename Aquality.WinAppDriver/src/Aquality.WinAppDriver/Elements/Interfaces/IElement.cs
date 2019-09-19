using Aquality.WinAppDriver.Elements.Actions;
using CoreElement = Aquality.Selenium.Core.Elements.Interfaces.IElement;
using IKeyboardActions = Aquality.WinAppDriver.Actions.IKeyboardActions;

namespace Aquality.WinAppDriver.Elements.Interfaces
{
    /// <summary>
    /// Descibes behavior of any application element.
    /// </summary>
    public interface IElement : CoreElement
    {
        /// <summary>
        /// Provides access to <see cref="IKeyboardActions"/> against the current element.
        /// </summary>
        IKeyboardActions KeyboardActions { get; }

        /// <summary>
        /// Provides access to <see cref="IMouseActions"/> against the current element.
        /// </summary>
        IMouseActions MouseActions { get; }
    }
}
