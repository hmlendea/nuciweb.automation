# Browser automation behaviour

The library presents a synchronous, XPath-oriented browser automation vocabulary.

- Tab methods maintain an in-memory ordered handle list.
- Navigation accepts URL, retry-count, and retry-delay overloads.
- `GoToIframe` reads the iframe source and navigates to it; `SwitchToIframe` delegates iframe context switching.
- Attribute, text, selected-text, class, ID, hyperlink, source, style, and value reads are available in single and collection forms.
- `IsElementVisible` first checks existence, then asks the adapter for visibility.
- `ClickAny` selects the first visible candidate and throws `ArgumentException` if none is visible.
- Checkbox updates click only when the current checked state differs from the requested state.
- Random option selection chooses an index from the adapter-reported option count.
