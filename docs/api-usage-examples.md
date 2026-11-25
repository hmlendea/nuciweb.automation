# API usage examples

## Navigate and interact

```csharp
processor.GoToUrl("https://example.com");
processor.WaitForElementToBeVisible("//input[@name='query']");
processor.SetText("//input[@name='query']", "NuciWeb");
processor.Click("//button[@type='submit']");
```

## Read a collection

```csharp
IList<string> links = processor.GetHyperlinkOfMany("//a");
```

## Manage a second tab

```csharp
string tab = processor.NewTab("https://example.com/secondary");
processor.SwitchToTab(tab);
processor.CloseTab(tab);
```

The examples assume that `processor` is a concrete adapter exposed as `IWebProcessor`.
