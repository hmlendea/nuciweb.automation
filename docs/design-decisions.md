# Design decisions

## Backend-neutral core

Browser-driver operations are protected abstract methods rather than a package-specific dependency. This keeps the public orchestration reusable across browser technologies, at the cost of requiring adapters to implement the complete `Perform*` surface.

## Template-method orchestration

The base class owns cross-backend sequencing such as tab tracking, overload defaults, polling, and convenience transformations. Adapters own translation to driver APIs.

## Synchronous contract

The API uses synchronous methods and blocking waits. This matches the current consumer contract but occupies the calling thread during delays.

## Transient state

Only active session coordination is stored, so the library remains small and does not create persistence, recovery, or migration obligations.
