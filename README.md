[![Build Status](https://dev.azure.com/aquality-automation/aquality-automation/_apis/build/status/aquality-automation.aquality-winappdriver-dotnet?branchName=master)](https://dev.azure.com/aquality-automation/aquality-automation/_build/latest?definitionId=4&branchName=master)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=aquality-automation_aquality-winappdriver-dotnet&metric=alert_status)](https://sonarcloud.io/dashboard?id=aquality-automation_aquality-winappdriver-dotnet)
[![NuGet](https://img.shields.io/nuget/v/Aquality.WinAppDriver)](https://www.nuget.org/packages/Aquality.WinAppDriver)
# Aquality WinAppDriver for .NET

### Overview

This package is a library designed to simplify your work with WinAppDriver, aimed to control Windows application using Appium and Selenium WebDriver API.

You've got to use this set of methods, related to most common actions performed with windows application elements.

Most of performed methods are logged using NLog, so you can easily see a history of performed actions in your log.

We use interfaces where is possible, so you can implement your own version of target interface with no need to rewrite other classes.

> Breaking news!

> Starting from v3.0.0 onwards, this package has dropped the support of Appium 1, and is only compatible to Appium 2.
> You will need to install appium 2 before using this package: `npm install -g appium@next`. 
> This require specific version of node.js to be preinstalled on your machine, details [here](http://appium.io/docs/en/contributing-to-appium/appium-from-source/#nodejs).
> It's good idea to remove all previous appium versions and artifacts prior to installation to avoid conflicts.
> Use the `appium driver install --source=npm appium-windows-driver` command to add it to your Appium 2 dist.
> More details could be found at https://github.com/appium/appium-windows-driver
>
> Important: Current version is compatible to [WinAppDriver v1.2.1](https://github.com/microsoft/WinAppDriver/releases/tag/v1.2.1). 
> If you are using WinAppDriver v1.3-rc you will need to uninstall it and install v1.2.1, as there are compatibility issues.

### Quick start

1. To start work with this package, simply add the nuget dependency Aquality.WinAppDriver to your project.

2. Configure path to your application executable at settings.json:
 - Create a `Resources` folder in your project and copy [settings.json](Aquality.WinAppDriver/src/Aquality.WinAppDriver/Resources/settings.json) into it. 
 - Select `Copy to Output Directory`: `Copy always` option at settings.json file properties.
 - Open settings.json and find `applicationPath` option under the `driverSettings` section. Replace the value with the path to your app.

3. Ensure that [WinAppDriver](https://github.com/microsoft/WinAppDriver) is set up at your machine where the code would be executed.

4. Start Appium service before your code execution. There are several options to do this:
 - Start appium with cmd/PS command "appium", e.g.: `start cmd.exe @cmd /k "appium"`
 - Start Appium automatically via AppiumLocalService - this option is integrated in our package, just set `"isRemote"` to `false` at your settings.json. You can read more [here](https://github.com/appium/appium-dotnet-driver/wiki/How-to-start-an-AppiumDriver-locally).
 
5. (optional) Launch an application directly by calling `AqualityServices.Application.Launch();`. 

> Note: 
If you don't start an Application directly, it would be started with the first call of any Aquality service or class requiring interacting with the Application.

6. That's it! Now you are able work with Application via AqualityServices or via element services.
```csharp

 AqualityServices.Application.MouseActions.Click();
 AqualityServices.Application.KeyboardActions.SendKeysWithKeyHold("n", ModifierKey.Control);
```

7. To interact with Application's windows, forms and elements, we recommend following the PageObjects pattern. This approach is fully integrated into our package.
To start with that, you will need to create a separate class for each window/form of your application, and inherit this class from the [Window](Aquality.WinAppDriver/src/Aquality.WinAppDriver/Forms/Window.cs) or [Form](Aquality.WinAppDriver/src/Aquality.WinAppDriver/Forms/Form.cs) respectively. 


> Note: 
> - By "Form" we mean part of the main window of your application (by default) or part of the any other window (specified as a constructor parameter). An example of form is a MenuBar or some section.
> - By "Window" we mean the separate window of your application (would be searched from the [RootSession](https://github.com/microsoft/WinAppDriver/wiki/Frequently-Asked-Questions#when-and-how-to-create-a-desktop-session) by default). In other words, the "Window" is a form which is separate from the main application window. There are several cases where you should use Window class instead of Form:
>   - Your application consists of several Windows;
>   - You need to interact with more than one application;
>   - You need to perform actions before/after the main application run (e.g. reinstall, update).

8. From the PageObject perspective, each Form consists of elements on it (e.g. Buttons, TextBox, Labels and so on). 
To interact with elements, on your form class create fields of type IButton, ITextBox, ILabel, and initialize them using the ElementFactory. Created elements have a various methods to interact with them. We recommend combining actions into a business-level methods:

```csharp

using Aquality.Selenium.Core.Configurations;
using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Elements.Interfaces;
using Aquality.WinAppDriver.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace CalcApp.Forms
{
    public class CalculatorForm : Form
    {
        private readonly ITextBox LeftArgumentTextBox;
        private readonly IButton OneButton;
        private readonly IButton TwoButton;
        private readonly IButton PlusButton;
        private readonly IButton EqualsButton;
        private readonly ILabel ResultsLabel;

        public CalculatorForm() : base(By.Name("Day Maxi Calc  v.1.5 Freeware"), "Calculator")
        {
            LeftArgumentTextBox = ElementFactory.GetTextBox(MobileBy.AccessibilityId("50"), "Left Argument");
            OneButton = ElementFactory.GetButton(By.Name("1"), "1");
            TwoButton = RelativeElementFactory.GetButton(By.Name("2"), "2");
            PlusButton = ElementFactory.FindChildElement<IButton>(this, By.Name("+"), "+");
            EqualsButton = FindChildElement<IButton>(By.Name("="), "=");
            ResultsLabel = ElementFactory.GetLabel(By.XPath("//*[@AutomationId='48']"), "Results bar");
        }

        private ITimeoutConfiguration TimeoutConfiguration => AqualityServices.Get<ITimeoutConfiguration>();

        public int CountOnePlusTwo()
        {
            const string expectedRightArgument = "1";
            OneButton.Click();
            ConditionalWait.WaitForTrue(() => LeftArgumentTextBox.Value == expectedRightArgument, 
                timeout: TimeoutConfiguration.Condition,
                message: $"Right argument should become '{expectedRightArgument}' after click on {OneButton.Name}");

            PlusButton.MouseActions.DoubleClick();
            TwoButton.MouseActions.Click();
            EqualsButton.State.WaitForClickable();
            EqualsButton.Click();
            return int.Parse(ResultsLabel.Text);
        }
    }
}
```



Please take a look at more examples in our tests at https://github.com/aquality-automation/aquality-winappdriver-dotnet/tree/master/Aquality.WinAppDriver/tests/Aquality.WinAppDriver.Tests/Forms.


### License
Library's source code is made available under the [Apache 2.0 license](https://github.com/aquality-automation/aquality-winappdriver-dotnet/blob/master/LICENSE).
