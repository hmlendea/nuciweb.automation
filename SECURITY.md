# Security Policy

This document describes the security policy for NuciWeb.Automation, including supported versions, vulnerability reporting procedures, and disclosure expectations.

## 📑 Table of Contents

- [Supported Versions](#supported-versions)
- [Reporting a Vulnerability](#reporting-a-vulnerability)
- [Scope](#scope)
- [Disclosure Policy](#disclosure-policy)
- [Automated Security Scanning](#automated-security-scanning)

## 🛡️ Supported Versions

Use this table to indicate which project versions currently receive security maintenance.
`Latest version` is literal text and must not be replaced with a version number.

| Version | Distribution Channel | Supported |
|---------|--------------------|-----------|
| Latest version | NuGet | ✅ |
| Latest version | GitHub Releases | ✅ |
| Preceding versions | Any distribution channel | ❌ |

## 🚨 Reporting a Vulnerability

Please do not disclose suspected vulnerabilities publicly before maintainers have had an opportunity to validate and remediate them.

To report a vulnerability:
- [GitHub Security Advisories](https://github.com/hmlendea/nuciweb.automation/security/advisories)
- Contact the maintainers directly at hmlendea@nucilandia.ro

## 📌 Scope

The original maintainers are not responsible for changes, modifications, or security issues introduced by downstream forks.

The subsequent report categories are in scope for this repository:
- Vulnerabilities in the NuciWeb.Automation library code
- Vulnerabilities in the WebProcessor base class and IWebProcessor interface
- Vulnerabilities in the NuciWeb.Automation.UnitTests project

The subsequent categories are out of scope unless explicitly stated to the contrary:
- Vulnerabilities in downstream applications that consume this library
- Vulnerabilities in browser automation backends (Selenium, Playwright, custom drivers)
- Vulnerabilities in the NuciWeb dependency (version 4.1.0)
- Vulnerabilities in the .NET runtime or SDK

## 📢 Disclosure Policy

This project follows coordinated disclosure:
1. Vulnerabilities are investigated privately.
2. A remediation plan is prepared and validated.
3. Public disclosure is published after a fix, mitigation, or agreed risk decision is available.
4. Credit is attributed in accordance with reporter preference and project policy.

## 🔍 Automated Security Scanning

This repository runs automated security scans on pushes, pull requests, and a weekly scheduled run. The scan includes:
- **NuGet packages** Vulnerability checks include transitive dependencies.
- **C# source code** CodeQL analysis builds the solution and uploads results to GitHub Code Scanning.

Results are published to the [Security](https://github.com/hmlendea/nuciweb.automation/security) tab and the [Actions](https://github.com/hmlendea/nuciweb.automation/actions/workflows/security.yml) workflow page.
