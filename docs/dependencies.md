# Dependencies

## Runtime

- `NuciWeb` 4.1.0 supplies shared HTML attribute-name constants used by convenience getters.
- .NET 10.0 supplies collections, timing, randomisation, disposal, and threading APIs.

## Development

- NUnit 5.0.0 provides unit-test attributes and assertions.
- Moq 4.21.0 is referenced by the test project for test doubles.
- Microsoft.NET.Test.Sdk 18.11.0 and NUnit3TestAdapter 6.3.1 integrate tests with `dotnet test`.

The repository has no browser-driver dependency. Adapters and consuming applications select that dependency outside this package.
