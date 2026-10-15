# Privacy and Personal Data

This document describes how NuciWeb.Automation handles personal data. NuciWeb.Automation is a .NET library that provides a reusable abstraction for browser automation. It is distributed as a NuGet package and integrated into applications by developers. The library itself does not collect, transmit, or process personal data; any data handling occurs within the consuming application and its chosen browser automation backend.

## 📑 Table of Contents

- [What This Document Covers](#what-this-document-covers)
- [Self-Hosted Deployments](#self-hosted-deployments)
- [Data We Handle](#data-we-handle)
- [Processing and Use](#processing-and-use)
- [Storage, Retention, and Deletion](#storage-retention-and-deletion)
- [External Processing and Integrations](#external-processing-and-integrations)
- [Data Protection and Security](#data-protection-and-security)
- [Document Changes](#document-changes)
- [Contact](#contact)

## 🔎 What This Document Covers

This document describes how NuciWeb.Automation at https://github.com/hmlendea/nuciweb.automation handles personal data. It covers the library behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 🏠 Self-Hosted Deployments

NuciWeb.Automation is a library that developers integrate into their own applications. The library does not operate as a service and does not send data to the project maintainers. The consuming application and its chosen browser automation backend (for example, Selenium, Playwright, or a custom driver) control all browser interactions, data flows, storage, logs, backups, access controls, retention, and request handling. The project maintainers do not receive telemetry, crash reports, update checks, or any other data from applications that use this library.

## 📥 Data We Handle

### Data Provided to the Application

The library does not request or receive personal data from users, administrators, or connected systems. Any personal data processed during browser automation is supplied by the consuming application and handled by the chosen backend.

### Data Generated or Collected by the Application

The library does not generate or collect personal data automatically. It does not include telemetry, crash reporting, update checks, or any background data collection.

### Data Received from Integrations

The library does not receive personal data from integrations or third parties. It provides an abstraction (`IWebProcessor`) that concrete backends implement; data flows depend entirely on the backend chosen by the consuming application.

## 🧭 Processing and Use

The library processes no personal data. It provides:

- A common `IWebProcessor` contract for browser automation
- A `WebProcessor` base class with shared behaviour (tab management, navigation, waiting, DOM querying, text input, selection, alert handling)
- Backend-agnostic APIs that concrete browser drivers implement

Any processing of personal data occurs in the consuming application and its chosen backend, not in this library.

## 🗄️ Storage, Retention, and Deletion

The library does not store personal data. It holds only transient in-memory state required for browser automation (for example, tab handles and configuration flags such as `RetryOnDomFailure`). This state is released when the `IWebProcessor` implementation is disposed. The consuming application and its backend control all persistent storage, retention, and deletion.

## 🔗 External Processing and Integrations

The library has no built-in external data transfers. It depends on the `NuciWeb` package (version 4.0.0) for shared web utilities, but that dependency does not introduce personal data processing by this library. Any external processing is determined by the concrete browser automation backend chosen by the consuming application.

| Service or integration | Purpose | Data involved | Configuration or documentation |
|-----------------------|---------|---------------|--------------------------------|
| NuciWeb (4.0.0) | Shared web utilities | None | https://www.nuget.org/packages/NuciWeb |

## 🛡️ Data Protection and Security

The library follows standard .NET secure coding practices. It does not handle secrets, credentials, or personal data. The consuming application and its backend are responsible for securing browser sessions, managing authentication credentials, protecting logs, and applying network and access controls appropriate to their deployment.

## 🔄 Document Changes

Update this document when library data flows, storage, integrations, or deployment responsibilities change. The current version is published at https://github.com/hmlendea/nuciweb.automation/blob/master/PRIVACY.md.

## 📬 Contact

For questions about library data handling, contact the project maintainers via the GitHub repository at https://github.com/hmlendea/nuciweb.automation. For a consuming application, contact the application operator. Include the library version and backend in use; do not send passwords, access tokens, or other secrets.