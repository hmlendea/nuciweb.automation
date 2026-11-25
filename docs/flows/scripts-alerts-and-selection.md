# Scripts, alerts, and selection

- `ExecuteScript` selects `CurrentTab` and delegates script execution.
- `GetVariableValue` evaluates `return <variableName>;` through the adapter after one default wait.
- `AcceptAlert` and `DismissAlert` delegate directly.
- `SelectOptionByIndex`, `SelectOptionByText`, and `SelectOptionByValue` delegate selection; non-string values are converted with `ToString()`.
- `SelectRandomOption` obtains the option count and chooses a random index in the adapter-defined range.

Script contents and variable names are supplied by the consumer and are not validated by this library.
