# Quick start

## Install

```bash
dotnet add package NuciWeb.Automation
```

The package targets `.NET 10.0` and does not include a browser driver.

## Consume the contract

```csharp
using NuciWeb.Automation;

public sealed class LoginAutomation(IWebProcessor processor)
{
    public void Run()
    {
        processor.GoToUrl("https://example.com/login");
        processor.WaitForElementToBeVisible("//input[@name='email']");
        processor.SetText("//input[@name='email']", "user@example.com");
        processor.Click("//button[@type='submit']");
    }
}
```

Provide an implementation of `WebProcessor` through the consuming application's composition boundary, then dispose it when the browser session ends.

## Implement an adapter

Override every protected abstract `Perform*` member in `WebProcessor`. Keep driver handles and browser-specific translation in the subclass; retain tab bookkeeping and shared behaviour in the base class.
