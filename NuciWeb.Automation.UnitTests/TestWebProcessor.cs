using System;
using System.Collections.Generic;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    internal sealed class TestWebProcessor : WebProcessor
    {
        private readonly Dictionary<string, string> _elements = [];
        private readonly Dictionary<string, bool> _elementVisibility = [];
        private readonly Dictionary<string, bool> _elementSelection = [];
        private readonly Dictionary<string, bool> _checkboxStates = [];
        private readonly Dictionary<string, int> _selectOptionCounts = [];
        private readonly Dictionary<string, List<string>> _selectOptions = [];

        public TestWebProcessor()
        {
            Tabs.Add("tab1");
            SwitchToTab("tab1");
        }

        public void SetElement(string xpath, string value)
            => _elements[xpath] = value;

        public void SetElementVisibility(string xpath, bool visible)
            => _elementVisibility[xpath] = visible;

        public void SetElementSelection(string xpath, bool selected)
            => _elementSelection[xpath] = selected;

        public void SetCheckboxState(string xpath, bool checkedState)
            => _checkboxStates[xpath] = checkedState;

        public void SetSelectOptions(string xpath, List<string> options)
        {
            _selectOptions[xpath] = options;
            _selectOptionCounts[xpath] = options.Count;
        }

        protected override bool PerformDoesElementExist(string xpath)
            => _elements.ContainsKey(xpath);

        protected override bool PerformIsCheckboxChecked(string xpath)
            => _checkboxStates.TryGetValue(xpath, out bool value) && value;

        protected override bool PerformIsElementVisible(string xpath)
            => _elementVisibility.TryGetValue(xpath, out bool value) && value;

        protected override bool PerformIsSelected(string xpath)
            => _elementSelection.TryGetValue(xpath, out bool value) && value;

        protected override IEnumerable<string> PerformGetAttribute(string xpath, string attribute)
        {
            if (_elements.TryGetValue(xpath, out string value))
            {
                yield return value;
            }
        }

        protected override IEnumerable<string> PerformGetSelectedText(string xpath)
        {
            if (_elements.TryGetValue(xpath, out string value))
            {
                yield return value;
            }
        }

        protected override IEnumerable<string> PerformGetText(string xpath)
        {
            if (_elements.TryGetValue(xpath, out string value))
            {
                yield return value;
            }
        }

        protected override int PerformGetSelectOptionsCount(string xpath)
            => _selectOptionCounts.TryGetValue(xpath, out int count) ? count : 0;

        protected override string PerformExecuteScript(string script)
            => string.Empty;

        protected override string PerformGetPageSource()
            => string.Empty;

        protected override string PerformNewTab(string url)
        {
            string tab = $"tab{Tabs.Count + 1}";
            return tab;
        }

        protected override void PerformAcceptAlert()
        {
        }

        protected override void PerformClick(string xpath)
        {
        }

        protected override void PerformCloseTab(string tab)
        {
            Tabs.Remove(tab);
        }

        protected override void PerformDismissAlert()
        {
        }

        protected override void PerformGoToUrl(string url, int httpRetries, TimeSpan retryDelay)
        {
        }

        protected override void PerformMoveToElement(string xpath)
        {
        }

        protected override void PerformRefresh()
        {
        }

        protected override void PerformSelectOptionByIndex(string xpath, int index)
        {
        }

        protected override void PerformSelectOptionByText(string xpath, string text)
        {
        }

        protected override void PerformSelectOptionByValue(string xpath, object value)
        {
        }

        protected override void PerformSetText(string xpath, string text)
        {
            _elements[xpath] = text;
        }

        protected override void PerformSwitchToIframe(string xpath)
        {
        }

        protected override void PerformSwitchToTab(string tab)
        {
        }
    }
}