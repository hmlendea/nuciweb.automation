# Navigation and tab lifecycle

1. The consumer calls `NewTab`, `GoToUrl`, or another tab operation.
2. `NewTab` asks the adapter for a handle, adds it to `Tabs`, and selects it.
3. `GoToUrl` creates `about:blank` when `CurrentTab` is empty; otherwise it reselects the current handle.
4. Navigation is delegated with the configured retry count and delay.
5. `SwitchToTab` rejects handles absent from `Tabs` with `ArgumentException`.
6. `CloseTab` delegates browser closure and removes the handle.
7. `Dispose` iterates over a copy of `Tabs` and closes each tracked handle.

The base class does not reconcile handles created outside its methods or restore state after process loss.
