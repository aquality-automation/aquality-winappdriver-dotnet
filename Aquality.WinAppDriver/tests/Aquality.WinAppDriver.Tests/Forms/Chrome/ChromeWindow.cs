﻿using Aquality.WinAppDriver.Actions;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;

namespace Aquality.WinAppDriver.Tests.Forms.Chrome
{
    public class ChromeWindow(string name) : Window(By.Name(name), name)
    {
        private ILabel DocumentLabel => FindChildElement<ILabel>(By.TagName("Document"), "Document");

        public void ClickOnDocument()
        {
            DocumentLabel.Click();
        }

        public void Close()
        {
            KeyboardActions.SendKeysWithKeyHold("w", ModifierKey.Control, mayDisappear: true);
        }
    }
}
