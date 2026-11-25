# Ambiguities and open questions

- No concrete browser-driver implementation is present, so backend-specific lifecycle, timeout, and exception behaviour cannot be documented here.
- The public API does not expose a timeout result or exception for condition waits; callers must inspect state after a wait when required.
- The finalizer does not close tabs because `Dispose(false)` returns before cleanup; explicit disposal is therefore the reliable cleanup path.
- Root `PRIVACY.md` and `SECURITY.md` refer to `NuciWeb` version 4.0.0 while the project file references 4.1.0. This documentation records the project-file version and the discrepancy should be reconciled in a policy-document update.
