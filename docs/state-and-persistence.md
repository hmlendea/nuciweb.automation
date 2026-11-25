# State and persistence

The library stores active browser-session coordination in memory only. `Tabs` tracks handles created through the processor; `CurrentTab` identifies the selected handle. Retry preference and the random generator are also processor-lifetime state.

No state is serialised, cached across processes, written to disk, or restored after a restart. Browser cookies, local storage, page state, credentials, and application data belong to the selected backend and consuming application.

Explicit `Dispose` closes tracked tabs through the adapter. It does not provide a general backend shutdown contract beyond those tab closures.
