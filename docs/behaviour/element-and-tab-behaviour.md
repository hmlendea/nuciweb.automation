# Element and tab behaviour

Element methods use XPath and operate against the selected tab. The base class supplies first-result and collection variants, aggregate existence and visibility predicates, text transformations, and interaction helpers. Tab methods validate ownership against the in-memory `Tabs` list before delegating to the adapter.

For a complete operation inventory, see [`IWebProcessor`](../api-reference/iweb-processor.md).