# Execution utilities

`NuciWeb.Automation.Utils.ExecutionUtils` contains the internal `RetryUntilTheResultIsNotNull<T>` helper.

The helper invokes an operation until it returns a non-null reference or the supplied timeout expires. An exception causes one default `WebProcessor.Wait()` interval before retrying; when the deadline is exceeded, the latest exception is rethrown. The helper is used by collection reads when `RetryOnDomFailure` is enabled.

The utility has no persistent state and is not part of the public API.
