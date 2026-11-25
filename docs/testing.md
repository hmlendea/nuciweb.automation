# Testing

The `NuciWeb.Automation.UnitTests` project uses NUnit and a lightweight `TestWebProcessor` subclass. The adapter stores element values, visibility, selection, checkbox state, and select counts in dictionaries, allowing shared orchestration to be tested without a browser.

Fixtures cover:

- tab creation, switching, closure, and invalid handles or indices;
- URL, iframe, and refresh delegation;
- attributes, classes, links, sources, styles, IDs, and values;
- text and selected-text reads, writes, append, clear, and retry mode;
- existence and visibility aggregates;
- waits and text-length polling;
- pointer movement, clicks, checkbox updates, and select operations;
- scripts, page source, and alerts.

Run the complete suite with `dotnet test` or target `NuciWeb.Automation.UnitTests/NuciWeb.Automation.UnitTests.csproj` directly.
