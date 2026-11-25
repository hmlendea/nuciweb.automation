# FAQ

## Does the package launch a browser?

No. It defines shared orchestration and requires a concrete `WebProcessor` subclass connected to a browser backend.

## Which browser automation library is required?

None is fixed. The consuming application or adapter selects the backend.

## Is the API asynchronous?

No. The public contract is synchronous and wait helpers block the calling thread.

## What happens when a wait times out?

Shared wait helpers stop polling at the default deadline without a dedicated timeout exception. Callers should verify the desired condition when it is material.

## Does the package persist tabs or page data?

No. Processor state is transient and in memory.
