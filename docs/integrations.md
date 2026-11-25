# Integrations

| Integration | Required | Role |
|-------------|----------|------|
| `NuciWeb` 4.1.0 | Yes | Supplies shared HTML attribute names. |
| Concrete browser automation backend | Adapter-dependent | Executes browser commands and returns page state. |
| Selenium, Playwright, or custom driver | No fixed choice | Possible backend families; none is included in this repository. |
| NuGet.org | Release-time | Publishes the package after a GitHub release. |

The library does not call external services directly. Network and browser traffic originate from the consuming adapter.
