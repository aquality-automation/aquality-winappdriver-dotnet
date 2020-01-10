using Aquality.WinAppDriver.Forms;

namespace Aquality.WinAppDriver.Tests.Forms
{
    public interface ITestForm : IForm
    {
        string ElementType { get; }
    }
}