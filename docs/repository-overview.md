# Repository overview

## Purpose

NuciWeb.Automation packages the shared control surface for browser automation without selecting Selenium, Playwright, or another driver. It is a library, not a browser service or executable application.

## Entry points

- `NuciWeb.Automation.IWebProcessor` is the public consumer-facing contract.
- `NuciWeb.Automation.WebProcessor` is the abstract template-method implementation.
- `NuciWeb.Automation.Utils.ExecutionUtils` contains the internal retry helper.

## Scope

The repository owns orchestration and transient processor state. A consuming application owns composition, credentials, workflows, browser-driver implementation, browser process, network access, and persistent data.

## Projects

- `NuciWeb.Automation/` is the packable `net10.0` library.
- `NuciWeb.Automation.UnitTests/` verifies the shared implementation with `TestWebProcessor`.
