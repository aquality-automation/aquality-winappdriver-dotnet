using Aquality.WinAppDriver.Actions;
using CoreElement = Aquality.Selenium.Core.Elements.Interfaces.IElement;

namespace Aquality.WinAppDriver.Elements.Interfaces
{
    public interface IElement : CoreElement
    {
        IKeyboardActions KeyboardActions { get; }
    }
}
