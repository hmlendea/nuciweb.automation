# Security Policy

This document describes the security policy for NuciWeb.Automation, including supported versions, vulnerability reporting procedures, and disclosure expectations.

## 📑 Table of Contents

- [Supported Versions](#supported-versions)
- [Reporting a Vulnerability](#reporting-a-vulnerability)
- [Scope](#scope)
- [Disclosure Policy](#disclosure-policy)

## 🛡️ Supported Versions

Use this table to indicate which project versions currently receive security maintenance.

| Version | Distribution Channel | Supported |
|---------|--------------------|-----------|
| Latest version | NuGet | ✅ |
| Preceding versions | Any distribution channel | ❌ |

## 🚨 Reporting a Vulnerability

Please do not disclose suspected vulnerabilities publicly before maintainers have had an opportunity to validate and remediate them.

To report a vulnerability:
- [GitHub Security Advisories](https://github.com/hmlendea/nuciweb.automation/security/advisories)
- Contact the maintainers directly at hmlendea@nucilandia.ro

## 📌 Scope

The subsequent report categories are in scope for this repository:
- Vulnerabilities in the NuciWeb.Automation library code
- Vulnerabilities in the WebProcessor base class and IWebProcessor interface
- Vulnerabilities in the NuciWeb.Automation.UnitTests project

The subsequent categories are out of scope unless explicitly stated to the contrary:
- Vulnerabilities in downstream applications that consume this library
- Vulnerabilities in browser automation backends (Selenium, Playwright, custom drivers)
- Vulnerabilities in the NuciWeb dependency (version 4.0.0)
- Vulnerabilities in the .NET runtime or SDK

## 📢 Disclosure Policy

This project follows coordinated disclosure:
1. Vulnerabilities are investigated privately.
2. A remediation plan is prepared and validated.
3. Public disclosure is published after a fix, mitigation, or agreed risk decision is available.
4. Credit is attributed in accordance with reporter preference and project policy.