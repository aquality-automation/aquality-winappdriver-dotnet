using System.Drawing;
using Aquality.WinAppDriver.Elements.Interfaces;

namespace Aquality.WinAppDriver.Forms
{
    /// <summary>
    /// Defines an interface for any form on any application's window.
    /// </summary>
    public interface IForm : IElement
    {
        /// <summary>
        /// Gets size of the form element defined by its locator.
        /// </summary>
        Size Size { get; }
    }
}