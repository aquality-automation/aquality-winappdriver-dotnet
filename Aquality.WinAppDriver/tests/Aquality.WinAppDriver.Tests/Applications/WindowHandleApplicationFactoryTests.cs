using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Configurations;
using Aquality.WinAppDriver.Extensions;
using Aquality.WinAppDriver.Tests.Forms;
using NUnit.Framework;

namespace Aquality.WinAppDriver.Tests.Applications
{
    public class WindowHandleApplicationFactoryTests : TestWithCustomApplication
    {
        protected override string ApplicationPath => AqualityServices.Get<IDriverSettings>().ApplicationPath;

        [Test]
        public void Should_BePossibleTo_SetWindowHandleApplicationFactory()
        {
            ProcessManager.Start(ApplicationPath);
            AqualityServices.SetWindowHandleApplicationFactory(rootSession => new CalculatorWindow(() => rootSession).GetNativeWindowHandle());
            Assert.IsTrue(new CalculatorForm().IsDisplayed);
        }
    }
}
