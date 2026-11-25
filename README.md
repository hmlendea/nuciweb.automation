[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Build Status](https://github.com/hmlendea/nuciweb.automation/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nuciweb.automation/actions/workflows/dotnet.yml)
[![Security Scan](https://github.com/hmlendea/nuciweb.automation/actions/workflows/security.yml/badge.svg)](https://github.com/hmlendea/nuciweb.automation/actions/workflows/security.yml)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nuciweb.automation)](https://github.com/hmlendea/nuciweb.automation/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/NuciWeb.Automation)](https://nuget.org/packages/NuciWeb.Automation)
[![License](https://img.shields.io/github/license/hmlendea/nuciweb.automation)](https://github.com/hmlendea/nuciweb.automation/blob/master/LICENSE)

# NuciWeb.Automation

NuciWeb.Automation is a .NET library that provides a backend-agnostic abstraction for browser automation. It centralizes tab management, navigation, waiting, DOM interaction, selection, scripting, and alert handling while concrete drivers implement the browser-specific operations.

## 📑 Table of Contents

- [Capabilities](#capabilities)
- [Usage](#usage)
  - [Implementing A Backend](#implementing-a-backend)
- [Compatibility](#compatibility)
- [Installation](#installation)
  - [Package Manager Installation](#package-manager-installation)
- [Development](#development)
  - [Requirements](#requirements)
  - [Setup](#setup)
  - [Build](#build)
  - [Test](#test)
  - [Formatting](#formatting)
  - [Dependencies](#dependencies)
- [GitHub Actions](#github-actions)
- [Project Structure](#project-structure)
  - [Projects and Packages](#projects-and-packages)
  - [Directories](#directories)
- [Architecture](#architecture)
- [Documentation](#documentation)
- [Privacy and Data](#privacy-and-data)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [Security](#security)
- [Support](#support)
- [Project Engagement](#project-engagement)
- [License](#license)

## ✨ Capabilities

- Manage browser tabs and current-tab state.
- Navigate to URLs and iframes with retry-aware operations.
- Query and interact with elements through XPath expressions.
- Read attributes, text, classes, styles, IDs, sources, values, and hyperlinks.
- Wait for element existence, visibility, selection, and state changes.
- Execute scripts, handle alerts, select options, and update checkboxes.
- Retry selected DOM reads when `RetryOnDomFailure` is enabled.

## 🚀 Usage

Application code can depend on `IWebProcessor` without depending on a specific browser backend:

```csharp
using NuciWeb.Automation;

public sealed class LoginAutomation(IWebProcessor webProcessor, string password)
{
	public void Run()
	{
		webProcessor.GoToUrl("https://example.com/login");
		webProcessor.WaitForElementToBeVisible("//input[@name='email']");
		webProcessor.SetText("//input[@name='email']", "user@example.com");
		webProcessor.SetText("//input[@name='password']", password);
		webProcessor.Click("//button[@type='submit']");
		webProcessor.WaitForElementToExist("//main");
	}
}
```

The package does not include a browser engine or a ready-to-run driver. The consuming application must provide a concrete implementation.

### Implementing A Backend

Inherit from `WebProcessor` and implement its abstract `Perform*` members. The base class provides tab bookkeeping, waits, retries, convenience overloads, element helpers, and disposal of tracked tabs.

```csharp
public sealed class CustomBrowserProcessor : WebProcessor
{
	protected override string PerformNewTab(string url) => throw new NotImplementedException();
	protected override void PerformSwitchToTab(string tab) => throw new NotImplementedException();
	protected override void PerformCloseTab(string tab) => throw new NotImplementedException();
	protected override void PerformGoToUrl(string url, int httpRetries, TimeSpan retryDelay) => throw new NotImplementedException();
	// Implement the remaining Perform* members for the selected browser backend.
}
```

## 🧩 Compatibility

| Component | Supported Versions | Notes |
|-----------|--------------------|-------|
| .NET | 10.0 | The library targets `net10.0`. |
| Browser backend | Concrete implementation dependent | This package defines the abstraction and does not select a browser driver. |

## 📦 Installation

[![Obtain it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciWeb.Automation)

### Package Manager Installation

```bash
dotnet add package NuciWeb.Automation
```

Or, via the `Package Manager Console`:

```powershell
Install-Package NuciWeb.Automation
```

## 🛠️ Development

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Git

### Setup

```bash
git clone https://github.com/hmlendea/nuciweb.automation.git
cd nuciweb.automation
dotnet restore
```

### Build

```bash
dotnet build --no-restore
```

### Test

```bash
dotnet test
```

### Formatting

```bash
dotnet format
```

### Dependencies

| Package | Version | Scope | Purpose |
|---------|---------|-------|---------|
| `NuciWeb` | 4.1.0 | Runtime | Shared web utility types used by the automation abstraction. |
| `NUnit` | 5.0.0 | Development | Unit-test framework. |
| `Moq` | 4.21.0 | Development | Test doubles for unit tests. |

## ⚙️ GitHub Actions

| Workflow | Purpose | What it does |
|----------|---------|--------------|
| [`.NET`](https://github.com/hmlendea/nuciweb.automation/blob/master/.github/workflows/dotnet.yml) | Build validation | Restores, builds, and tests the solution on pushes and pull requests to `master`. |
| [`Security`](https://github.com/hmlendea/nuciweb.automation/blob/master/.github/workflows/security.yml) | Security validation | Checks vulnerable NuGet packages and runs CodeQL analysis for C# on pushes, pull requests, and a weekly schedule. |
| [`GitHub Release`](https://github.com/hmlendea/nuciweb.automation/blob/master/.github/workflows/github-release.yml) | GitHub package release | Packs the project and uploads the NuGet package to a published GitHub release with a SHA256 checksum. |
| [`NuGet.org Release`](https://github.com/hmlendea/nuciweb.automation/blob/master/.github/workflows/nuget-release.yml) | NuGet publication | Packs the project and publishes it to NuGet.org after a GitHub release is published. |

## 🗂️ Project Structure

The repository contains the library project, its unit tests, and shared repository documentation.

### Projects and Packages

| Project | Type | Purpose |
|---------|------|---------|
| `NuciWeb.Automation/` | .NET library | Public browser automation contract and shared orchestration. |
| `NuciWeb.Automation.UnitTests/` | NUnit test project | Unit tests for the library behavior. |

### Directories

| Directory | Purpose |
|-----------|---------|
| `NuciWeb.Automation/Utils/` | Internal execution and retry utilities. |
| `.github/workflows/` | Build and release automation. |

## 🏗️ Architecture

NuciWeb.Automation is an in-process .NET library that separates shared browser-automation orchestration from concrete browser-driver implementations.

See the [architecture documentation](./ARCHITECTURE.md) for the system context, principal components, runtime processes, ownership boundaries, dependencies, constraints, and extension points.

## 📚 Documentation

The repository documentation records the library's public contract, implementation boundaries, execution flows, testing model, and maintenance guidance.

| Resource | Description |
|----------|-------------|
| [Documentation index](./docs/INDEX.md) | Navigation catalogue for the generated documentation. |
| [Quick start](./docs/quick-start.md) | Installation, first use, and adapter implementation guidance. |
| [Architecture deep dive](./docs/architecture.md) | Component boundaries and delegation rules. |
| [API reference](./docs/api-reference/INDEX.md) | Public interface and adapter-contract reference. |
| [Execution flows](./docs/flows/navigation-and-tab-lifecycle.md) | Navigation, DOM interaction, waits, scripts, alerts, and selection. |

## 🛡️ Privacy and Data

NuciWeb.Automation does not collect, transmit, or process personal data. It provides a backend-agnostic browser automation abstraction; any data handling occurs in the consuming application and its chosen driver.

For the detailed description of how the application handles privacy and personal data, see the [Privacy Statement](./PRIVACY.md).

## 🤝 Contributing

You are welcome to submit any suggestion, feedback, or modification to this project.

When doing so, please:
- Maintain cross-platform compatibility
- Preserve the existing public contract unless a breaking change is intentional
- Submit focused pull requests that conform to the existing code style
- Maintain your branch synchronised with `master`
- Revise the documentation when functionality changes
- Properly test all modifications, including edge cases and error conditions
- Include tests for additional or modified functionality
- Raise a new [issue](https://github.com/hmlendea/nuciweb.automation/issues) for problems or suggestions

See the [Contributing Guidelines](./CONTRIBUTING.md) for details on how to report issues, suggest enhancements, and submit changes.

## 📜 Code of Conduct

See the [Code of Conduct](./CODE_OF_CONDUCT.md) for the standards of conduct expected in all project spaces.

## 🔒 Security

NuciWeb.Automation follows coordinated disclosure.

For information on reporting security vulnerabilities, see the [Security Policy](./SECURITY.md).

## 💬 Support

For support, please [open an issue](https://github.com/hmlendea/nuciweb.automation/issues).

## 💝 Project Engagement

Discovered a problem with NuciWeb.Automation or have a suggestion for it? [Open an issue](https://github.com/hmlendea/nuciweb.automation/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) and giving it a star ⭐️ on GitHub!

[![Donate](https://raw.githubusercontent.com/hmlendea/readme-assets/master/donate_generic.png)](https://hmlendea.go.ro/funding)

## 📄 License

This project is being distributed under the `GNU General Public License v3.0 or later`.
See [LICENSE](./LICENSE) for additional information.
