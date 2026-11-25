# Change guide

## Public API changes

Update `IWebProcessor`, the base implementation, relevant tests, and the API documentation. Treat signatures and overloads as compatibility contracts.

## Shared behaviour changes

Modify `WebProcessor`, then add or revise a focused fixture in `NuciWeb.Automation.UnitTests`. Preserve tab ownership, current-tab sequencing, default timing, and disposal semantics unless the change is intentional and documented.

## Adapter contract changes

Any new or changed protected `Perform*` member affects every external subclass. Document the required translation and verify the base call sequence with a test adapter.

## Dependency or release changes

Update the project file, dependency documentation, README compatibility/install sections, and applicable workflow or package instructions. Run restore, build, and tests.
