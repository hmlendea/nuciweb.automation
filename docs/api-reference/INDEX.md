# API reference index

There are no controllers or HTTP endpoints. The public API is the .NET interface `NuciWeb.Automation.IWebProcessor` and the abstract base class `NuciWeb.Automation.WebProcessor`.

| Surface | Main operations | Authentication |
|---------|-----------------|----------------|
| `IWebProcessor` | Tabs, navigation, DOM reads/writes, waits, interaction, scripts, alerts, selection, disposal | None supplied by this library |
| `WebProcessor` | Shared implementation plus protected `Perform*` adapter contract | None supplied by this library |

## Global contract

- Target framework: `net10.0`.
- Element selectors are XPath strings passed to the adapter.
- Calls are synchronous.
- Backend exceptions propagate; the library does not define an HTTP error model, rate limiting policy, or authorisation scheme.

See [components/web-processor.md](../components/web-processor.md) for operation groups and [invariants](../invariants.md) for behavioural contracts.
