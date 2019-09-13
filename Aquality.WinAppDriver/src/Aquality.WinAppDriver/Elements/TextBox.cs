using Aquality.WinAppDriver.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Elements
{
    /// <summary>
    /// Defines TextBox UI element.
    /// </summary>
    public class TextBox : Element, ITextBox
    {
        private const string SecretMask = "*********";

        protected internal TextBox(By locator, string name) : base(locator, name)
        {
        }

        public string Value => GetAttribute("Value.Value");

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.text.field");

        public void ClearAndType(string value, bool secret = false)
        {
            LogElementAction("loc.text.clearing");
            LogElementAction("loc.text.typing", secret ? SecretMask : value);
            DoWithRetry(() =>
            {
                GetElement().Clear();
                GetElement().SendKeys(value);
            });
        }

        public void Type(string value, bool secret = false)
        {
            LogElementAction("loc.text.typing", secret ? SecretMask : value);
            DoWithRetry(() => GetElement().SendKeys(value));
        }
    }
}
