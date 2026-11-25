# Data model

The library has no domain entities or persistence model. Its data model is transient processor state.

| State | Representation | Owner | Mutation |
|-------|----------------|-------|----------|
| Processor name | Concrete type name-derived string | `WebProcessor` | Read-only |
| Tab collection | `IList<string>` | `WebProcessor` | New, close |
| Current tab | `string` | `WebProcessor` | Switch and new tab |
| DOM retry preference | `bool` | `WebProcessor` | Consumer |
| Random selector | `Random` | `WebProcessor` | Internal selection |
| Page and DOM state | Backend-defined | Adapter/browser | Browser operations |

The string handles in `Tabs` are opaque to the base class and represent the adapter's browser-window identity.
