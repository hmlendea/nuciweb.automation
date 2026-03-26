[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/fund.html) [![Build Status](https://github.com/hmlendea/nuciweb.automation/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nuciweb.automation/actions/workflows/dotnet.yml) [![Latest GitHub release](https://img.shields.io/github/v/release/hmlendea/nuciweb.automation)](https://github.com/hmlendea/nuciweb.automation/releases/latest)

# NuciWeb.Automation

## About

NuciWeb.Automation is a .NET library that defines a reusable abstraction for browser automation.

It provides:

- a common `IWebProcessor` contract
- a `WebProcessor` base class with shared behavior
- tab management, navigation, waiting, DOM querying, text input, selection, and alert helpers
- a backend-agnostic API that concrete browser drivers can implement

This package is intended to sit between application code and a concrete automation backend. It centralizes the higher-level automation workflow so implementations can focus on the driver-specific operations.

## Features

- Browser tab lifecycle management
- URL navigation with retry support
- IFrame navigation and switching
- XPath-based element lookup and interaction
- Text, attribute, class, style, id, source, and hyperlink retrieval
- Visibility and existence checks
- Waiting helpers for common automation scenarios
- Alert handling
- Script execution
- Select element helpers
- Optional retry behavior for transient DOM lookup failures

## Target Framework

- .NET 10.0

## Installation

[![Get it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciWeb.Automation)

**.NET CLI**:
```bash
dotnet add package NuciWeb.Automation
```

**Package Manager**:
```powershell
Install-Package NuciWeb.Automation
```

## When To Use It

Use this library when you want to:

- write automation code against a stable interface instead of a specific browser driver
- share common automation logic across multiple backends
- implement a custom browser automation provider while reusing the higher-level workflow already present in `WebProcessor`

This package does not include a browser engine or a ready-to-run driver by itself. A concrete implementation must supply the low-level browser operations.

## Basic Usage

Application code can depend on `IWebProcessor` and stay independent from the browser backend:

```csharp
using NuciWeb.Automation;

public sealed class LoginAutomation
{
	private readonly IWebProcessor webProcessor;

	public LoginAutomation(IWebProcessor webProcessor)
	{
		this.webProcessor = webProcessor;
	}

	public void Run()
	{
		webProcessor.GoToUrl("https://example.com/login");
		webProcessor.WaitForElementToBeVisible("//input[@name='email']");

		webProcessor.SetText("//input[@name='email']", "user@example.com");
		webProcessor.SetText("//input[@name='password']", "secret");
		webProcessor.Click("//button[@type='submit']");

		webProcessor.WaitForElementToExist("//main");
	}
}
```

## Implementing A Backend

To create a working automation backend, inherit from `WebProcessor` and implement the abstract `Perform*` members.

The base class already handles:

- tab bookkeeping
- retry and wait orchestration
- convenience overloads
- higher-level selection and lookup helpers
- common element existence and visibility workflows

Example skeleton:

```csharp
using System;
using System.Collections.Generic;
using NuciWeb.Automation;

public sealed class CustomBrowserProcessor : WebProcessor
{
	protected override string PerformNewTab(string url) => throw new NotImplementedException();
	protected override void PerformSwitchToTab(string tab) => throw new NotImplementedException();
	protected override void PerformCloseTab(string tab) => throw new NotImplementedException();
	protected override void PerformGoToUrl(string url, int httpRetries, TimeSpan retryDelay) => throw new NotImplementedException();
	protected override void PerformSwitchToIframe(string xpath) => throw new NotImplementedException();
	protected override void PerformRefresh() => throw new NotImplementedException();
	protected override string PerformExecuteScript(string script) => throw new NotImplementedException();
	protected override void PerformAcceptAlert() => throw new NotImplementedException();
	protected override void PerformDismissAlert() => throw new NotImplementedException();
	protected override string PerformGetPageSource() => throw new NotImplementedException();
	protected override IEnumerable<string> PerformGetAttribute(string xpath, string attribute) => throw new NotImplementedException();
	protected override IEnumerable<string> PerformGetText(string xpath) => throw new NotImplementedException();
	protected override IEnumerable<string> PerformGetSelectedText(string xpath) => throw new NotImplementedException();
	protected override void PerformSetText(string xpath, string text) => throw new NotImplementedException();
	protected override bool PerformDoesElementExist(string xpath) => throw new NotImplementedException();
	protected override bool PerformIsElementVisible(string xpath) => throw new NotImplementedException();
	protected override bool PerformIsSelected(string xpath) => throw new NotImplementedException();
	protected override bool PerformIsCheckboxChecked(string xpath) => throw new NotImplementedException();
	protected override void PerformMoveToElement(string xpath) => throw new NotImplementedException();
	protected override void PerformClick(string xpath) => throw new NotImplementedException();
	protected override int PerformGetSelectOptionsCount(string xpath) => throw new NotImplementedException();
	protected override void PerformSelectOptionByIndex(string xpath, int index) => throw new NotImplementedException();
	protected override void PerformSelectOptionByText(string xpath, string text) => throw new NotImplementedException();
	protected override void PerformSelectOptionByValue(string xpath, object value) => throw new NotImplementedException();
}
```

## API Overview

The public API exposed by `IWebProcessor` includes these main groups of operations:

- Tab management: `Tabs`, `CurrentTab`, `NewTab`, `SwitchToTab`, `CloseTab`
- Navigation: `GoToUrl`, `GoToIframe`, `SwitchToIframe`, `Refresh`
- Scripting: `ExecuteScript`, `GetVariableValue`
- Alerts: `AcceptAlert`, `DismissAlert`
- Element inspection: `GetAttribute`, `GetText`, `GetValue`, `GetClass`, `GetId`, `GetStyle`, `GetSource`, `GetHyperlink`
- Text interaction: `SetText`, `AppendText`, `ClearText`
- State checks: `DoesElementExist`, `IsElementVisible`, `IsSelected`, `HasClass`
- Waiting helpers: `Wait`, `WaitForElementToExist`, `WaitForElementToBeVisible`, `WaitForAnyElementToExist`, `WaitForAllElementsToBeVisible`, and related methods
- Selection helpers: `SelectOptionByIndex`, `SelectOptionByValue`, `SelectOptionByText`, `SelectRandomOption`
- Pointer interaction: `MoveToElement`, `Click`, `ClickAny`, `UpdateCheckbox`

## DOM Failure Retries

The `RetryOnDomFailure` property enables retry logic for some read operations such as attribute, text, and selected-text retrieval. This is useful when the underlying browser backend occasionally fails during transient DOM updates.

## Design Notes

- The API is XPath-centric.
- The base class uses a small default polling delay for wait loops.
- Navigation includes retry-aware overloads for transient HTTP failures.
- Disposal closes tracked tabs through the abstraction instead of assuming a specific driver lifecycle.

## License

This project is licensed under the GNU General Public License v3.0 or later. See [LICENSE](./LICENSE).