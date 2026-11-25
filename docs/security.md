# Security

This document complements the root [SECURITY.md](../SECURITY.md).

## Library boundary

The library has no authentication, authorisation, telemetry, persistence, or built-in external data transfer. It accepts URLs, XPath expressions, scripts, text, and variable names from the consumer and delegates them to the adapter.

## Consumer responsibilities

Consumers and adapters must protect credentials, browser sessions, page data, logs, and network access. Script and XPath inputs should be treated as trusted only when the consuming workflow establishes that trust.

## Reporting

Report suspected vulnerabilities through the channels and process in the root security policy. Do not publish sensitive details in a public issue before maintainers have assessed them.
