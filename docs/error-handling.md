# Error handling

The library preserves backend exceptions rather than mapping them to a common exception hierarchy.

- Unknown tab handles cause `ArgumentException`.
- Invalid tab indices use normal list-index behaviour and can cause `ArgumentOutOfRangeException`.
- Single-result reads call `First()` and can cause `InvalidOperationException` for no matches.
- `ClickAny` causes `ArgumentException` when no candidate is visible.
- Retry-enabled DOM reads rethrow the latest adapter exception after the timeout.
- Wait helpers stop at their deadline without returning a timeout status or throwing a library-specific timeout exception.

Adapters should document their own browser, network, and driver exception semantics.
