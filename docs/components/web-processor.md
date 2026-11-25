# Web processor component

`NuciWeb.Automation.WebProcessor` is the central template-method component.

## Responsibilities

- Track tab handles and selected-tab state.
- Select or create a tab before navigation and current-tab operations.
- Expose XPath-based convenience methods for common HTML attributes.
- Poll element conditions and text length.
- Translate high-level text, checkbox, click, select, script, alert, and navigation calls into protected adapter calls.
- Close every tracked tab during explicit disposal.

## Defaults

- Default wait: 333 milliseconds.
- Default timeout: 20 seconds.
- Default navigation attempts: 3.

## Protected adapter surface

The subclass implements tab creation, switching and closure; navigation, iframe switching, refresh; DOM existence and visibility; attribute and text reads; script and page-source retrieval; alerts; pointer movement and clicks; text updates; checkbox state; and select operations.

## Important sequencing

`NewTab` calls the adapter, records the returned handle, and selects it. `GoToUrl` creates `about:blank` when no current tab exists, otherwise reselects the current tab, then delegates navigation. `CloseTab` delegates closure before removing the handle from `Tabs`.
