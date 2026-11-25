# Troubleshooting

| Symptom | Likely cause | Evidence-based action |
|---------|--------------|----------------------|
| `ArgumentException` when switching or closing | The handle is not present in `Tabs`. | Create or register the tab through the processor and use its returned handle. |
| `InvalidOperationException` from a getter | The adapter returned no value for a single-result read. | Check existence and selector correctness before calling the single-result method. |
| A wait returns without the expected element | The condition was not met before the 20-second deadline. | Verify the condition explicitly and inspect adapter/browser state. |
| DOM reads fail intermittently | The backend reports transient DOM failures. | Enable `RetryOnDomFailure` for supported collection reads and investigate the backend condition. |
| Browser-specific operation is unavailable | No concrete adapter implements the relevant `Perform*` method. | Implement or revise the adapter; the core package does not select a driver. |
