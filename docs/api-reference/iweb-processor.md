# `IWebProcessor`

Namespace: `NuciWeb.Automation`

`IWebProcessor` is the public `IDisposable` contract. It exposes processor identity and state, tab management, URL and iframe navigation, refresh, script and alert operations, page and element reads, text mutation, existence and visibility checks, waits, clicks, pointer movement, checkbox updates, and select-option operations.

## State

- `Name` identifies the concrete processor type without the `Processor` suffix.
- `Tabs` contains tracked tab handles.
- `CurrentTab` identifies the selected handle.
- `RetryOnDomFailure` enables retry for attribute, text, and selected-text collection reads.

## Selector operations

Single-element methods return the first result and therefore may throw when no result exists. `*OfMany` methods return a materialised list. Aggregate methods use all/any semantics over the supplied XPath values.

## Timing

`Wait` overloads block synchronously. Condition helpers poll for up to the base implementation's default timeout and do not expose a timeout-result value.

## Adapter boundary

Consumers normally depend on this interface. Concrete implementations inherit `WebProcessor` and implement its protected `Perform*` members rather than implementing browser operations directly in consumer workflows.
