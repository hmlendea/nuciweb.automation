# Invariants

- A handle returned by `PerformNewTab` is added to `Tabs` before it becomes current.
- `SwitchToTab(string)` only accepts a handle currently present in `Tabs`.
- `CurrentTab` is the handle selected by the most recent successful tab-selection operation.
- Closing a tracked tab calls `PerformCloseTab` before removing the handle from `Tabs`.
- Public browser operations delegate backend-specific work through the protected adapter surface.
- Default waits are 333 milliseconds, default condition timeout is 20 seconds, and default navigation attempts are 3.
- The processor owns transient state only; it does not persist tabs or page data.
- A processor instance is not synchronised for concurrent use.
