# Repository structure

| Path | Role |
|------|------|
| `NuciWeb.Automation/IWebProcessor.cs` | Public interface and operation inventory. |
| `NuciWeb.Automation/WebProcessor.cs` | Shared orchestration, state, convenience methods, waits, and protected extension points. |
| `NuciWeb.Automation/Utils/ExecutionUtils.cs` | Internal retry-until-non-null helper. |
| `NuciWeb.Automation/NuciWeb.Automation.csproj` | `net10.0` package metadata and `NuciWeb` dependency. |
| `NuciWeb.Automation.UnitTests/` | NUnit fixtures and the in-memory `TestWebProcessor` adapter. |
| `.github/workflows/dotnet.yml` | Restore, build, and test validation. |
| `.github/workflows/github-release.yml` | GitHub release package upload and checksum. |
| `.github/workflows/nuget-release.yml` | NuGet publication after a release. |
| `NuciWeb.Automation.slnx` | Solution containing the library and test projects. |
