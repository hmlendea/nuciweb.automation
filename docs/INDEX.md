# Documentation index

NuciWeb.Automation is a synchronous, backend-agnostic .NET abstraction for browser automation. `IWebProcessor` defines the consumer contract; `WebProcessor` supplies tab management, navigation, DOM helpers, waits, retries, scripting, alerts, selection, and disposal; concrete subclasses connect those operations to a browser driver.

## Start here

- [Quick start](./quick-start.md) for installation and first use.
- [Repository overview](./repository-overview.md) for scope and entry points.
- [Architecture](./architecture.md) for boundaries and delegation.

## Deep dive

- [Components](./components/web-processor.md)
- [API reference](./api-reference/INDEX.md)
- [Data model](./data-model.md)
- [Invariants](./invariants.md)
- [Change guide](./change-guide.md)

## Flows

- [Navigation and tab lifecycle](./flows/navigation-and-tab-lifecycle.md)
- [DOM interaction and waits](./flows/dom-interaction-and-waits.md)
- [Scripts, alerts, and selection](./flows/scripts-alerts-and-selection.md)

## Reference catalogue

- [API reference index](./api-reference/INDEX.md)
- [API usage examples](./api-usage-examples.md)
- [Ambiguities and open questions](./ambiguities-and-open-questions.md)
- [Behaviour](./behaviour/browser-automation.md)
- [Element and tab behaviour](./behaviour/element-and-tab-behaviour.md)
- [Architecture](./architecture.md)
- [Build and deployment](./build-and-deployment.md)
- [Concurrency and scheduling](./concurrency-and-scheduling.md)
- [Configuration](./configuration.md)
- [Dependencies](./dependencies.md)
- [Design decisions](./design-decisions.md)
- [Documentation maintenance](./documentation-maintenance.md)
- [Error handling](./error-handling.md)
- [FAQ](./faq.md)
- [Integrations](./integrations.md)
- [Logging](./logging.md)
- [Repository structure](./repository-structure.md)
- [Security](./security.md)
- [State and persistence](./state-and-persistence.md)
- [Testing](./testing.md)
- [Troubleshooting](./troubleshooting.md)

## Maintenance metadata

- Last reviewed: 2026-11-25
- Owner: repository maintainers
- Coverage: source, project files, workflows, root policies, and unit-test fixtures reviewed; no concrete browser backend is included.
