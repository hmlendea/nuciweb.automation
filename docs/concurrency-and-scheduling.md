# Concurrency and scheduling

`WebProcessor` is synchronous and mutable. `Wait` uses `Thread.Sleep`, and condition helpers poll on the calling thread. No asynchronous API, scheduler, lock, or concurrent collection is provided.

Treat one processor instance as single-threaded. If a consuming application needs concurrent workflows, it should coordinate access or create separate processor instances according to the selected backend's session rules.

Disposal is explicit through `IDisposable`; the finalizer only invokes the non-disposing path, which intentionally performs no tab closure.
