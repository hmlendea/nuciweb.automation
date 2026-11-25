# Configuration

There are no configuration files, environment variables, command-line options, or service profiles in this repository.

Runtime configuration is limited to public processor properties and method arguments:

| Setting | Default | Effect |
|---------|---------|--------|
| `RetryOnDomFailure` | `false` | Retries attribute, text, and selected-text collection reads after adapter exceptions. |
| `GoToUrl` retry count | `3` | Default navigation attempt count passed to the adapter. |
| Retry delay | `333 ms` | Default delay passed by the navigation overload chain. |
| Condition timeout | `20 s` | Maximum polling duration in shared wait helpers. |
