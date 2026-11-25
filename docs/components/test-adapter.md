# Test adapter component

`NuciWeb.Automation.UnitTests.TestWebProcessor` is an internal in-memory adapter used to isolate `WebProcessor` orchestration from a real browser.

It starts with `tab1`, stores synthetic element values and state in dictionaries, and implements protected `Perform*` members with deterministic no-browser behaviour. Tests configure it through `SetElement`, visibility, selection, checkbox, and select-option helpers.

It is test infrastructure, not a production browser implementation.
