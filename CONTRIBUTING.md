# Contributing to NuciWeb.Automation

This document covers the guidelines and processes for contributing to this project, including how to report issues, suggest enhancements, submit code changes, and follow project standards.

## 📑 Table of Contents

- [How to Contribute](#how-to-contribute)
- [Development Setup](#development-setup)
- [Code Style](#code-style)
- [Testing](#testing)
- [Documentation](#documentation)
- [Code of Conduct](#code-of-conduct)
- [Security](#security)
- [License](#license)

## 🤝 How to Contribute

### Reporting Issues

- Search existing issues first.
- Use the issue templates if available.
- Provide clear reproduction steps.
- Include environment details (OS, .NET version, browser driver).

### Suggesting Enhancements

- Check the roadmap and existing discussions.
- Explain the use case and expected behaviour.
- Consider implementation complexity.

### Code Contributions

#### Prerequisites

- .NET 10.0 SDK or newer
- Git

#### Development Setup

```bash
# Clone the repository
git clone https://github.com/hmlendea/nuciweb.automation.git
cd nuciweb.automation

# Install dependencies
dotnet restore
```

#### Making Changes

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Make your changes.
4. Run tests: `dotnet test`
5. Run linters: `dotnet format`
6. Commit with clear and descriptive messages.
7. Push to your fork.
8. Open a Pull Request.

#### Pull Request Guidelines

- Target the `master` branch.
- Keep PRs focused and atomic.
- Revise documentation if applicable.
- Include tests for any new functionality.
- Ensure the CI checks pass.

### Code Style

Follow the project's coding standards:
- C# coding conventions as defined in the repository instructions
- Run `dotnet format` before committing

### Testing

```bash
# Run all tests
dotnet test

# Run specific test suite
dotnet test NuciWeb.Automation.UnitTests/NuciWeb.Automation.UnitTests.csproj
```

### Documentation

- Revise relevant docs for changes.
- Follow the documentation style guide.
- Preview changes locally if possible.

## 📋 Code of Conduct

Please refer to the dedicated [CODE_OF_CONDUCT.md](./CODE_OF_CONDUCT.md) file for the full code of conduct policy, including expected conduct, unacceptable conduct, reporting concerns, and enforcement.

## 🔒 Security

### Reporting Security Issues

Security issues should be reported privately to the project maintainers at hmlendea@nucilandia.ro. Please do not disclose security vulnerabilities publicly until they have been addressed.

### Security Policy

The project maintains a security policy document at [SECURITY.md](./SECURITY.md) that describes known issues, supported versions, and mitigation steps.

### Supported Versions

| Version | Distribution Channel | Supported |
|---------|--------------------|-----------|
| Latest version | NuGet | ✅ |
| Preceding versions | Any distribution channel | ❌ |

### Scope

The following report categories are in scope for this repository:
- Vulnerabilities in the NuciWeb.Automation library code
- Vulnerabilities in the WebProcessor base class and IWebProcessor interface
- Vulnerabilities in the NuciWeb.Automation.UnitTests project

The following categories are out of scope unless explicitly stated to the contrary:
- Vulnerabilities in downstream applications that consume this library
- Vulnerabilities in browser automation backends (Selenium, Playwright, custom drivers)
- Vulnerabilities in the NuciWeb dependency (version 4.0.0)
- Vulnerabilities in the .NET runtime or SDK

### Disclosure Policy

This project follows coordinated disclosure:
1. Vulnerabilities are investigated privately.
2. A remediation plan is prepared and validated.
3. Public disclosure is published after a fix, mitigation, or agreed risk decision is available.
4. Credit is attributed in accordance with reporter preference and project policy.

## 📄 License

### GPL-3.0-or-later

This project is licensed under the GNU General Public License v3.0 or later. See the [LICENSE](./LICENSE) file for the full license text.

Contributions to this project will be licensed under the same license. By submitting a pull request, you agree that your contributions will be licensed under GPL-3.0-or-later.