# Build and deployment

## Build

```bash
dotnet restore
dotnet build --no-restore
```

## Test

```bash
dotnet test
```

## Distribution

The library is packed as the `NuciWeb.Automation` NuGet package. Published GitHub releases trigger the GitHub Release workflow, which packs the project and uploads the package with a SHA256 checksum. The NuGet.org workflow then publishes the package using NuGet trusted publishing.

The package is an in-process dependency. It has no service process, deployment environment, listening port, database, or independent health check.
