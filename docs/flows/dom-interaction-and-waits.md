# DOM interaction and waits

DOM methods pass XPath values to the adapter. Collection reads are materialised into `IList<string>` values. Single-result methods call `First()`, so an empty result becomes an `InvalidOperationException`.

Condition helpers repeatedly evaluate existence, visibility, disappearance, or text length until success or 20 seconds elapse. Each unsuccessful iteration waits 333 milliseconds. They return after the deadline without a dedicated timeout exception, so callers must verify the condition when failure matters.

When `RetryOnDomFailure` is true, attribute, text, and selected-text collection reads retry adapter exceptions until a non-null result or the default timeout.
