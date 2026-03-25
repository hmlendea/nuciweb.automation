using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NuciWeb.Automation.Utils;

namespace NuciWeb.Automation
{
    public abstract class WebProcessor() : IWebProcessor
    {
        /// <summary>
        /// Gets the name of the web processor.
        /// </summary>
        public string Name => GetType().Name.Replace("Processor", string.Empty);

        /// <summary>
        /// Gets or sets a value indicating whether to retry on DOM failure when getting attributes or classes of elements.
        /// </summary>
        public bool RetryOnDomFailure { get; set; } = false;

        /// <summary>
        /// Gets the list of tabs currently managed by this web processor.
        /// Each tab is represented by its window handle.
        /// </summary>
        public IList<string> Tabs { get; private set; } = [];

        /// <summary>
        /// Gets the current tab (window handle) that this processor is working with.
        /// </summary>
        public string CurrentTab { get; private set; }

        /// <summary>
        /// Gets the random number generator used by this processor.
        /// This can be used for generating random values, such as for random delays or selections.
        ///
        /// </summary>
        public Random Random { get; private set; } = new Random();

        /// <summary>
        /// The default duration to wait for certain operations, such as element visibility or existence.
        /// </summary>
        protected static readonly TimeSpan DefaultWaitDuration = TimeSpan.FromMilliseconds(333);

        /// <summary>
        /// The default timeout for operations that require waiting, such as loading a page or waiting for an element.
        /// This is used to ensure that operations do not hang indefinitely and have a reasonable timeout.
        /// The default value is set to 20 seconds.
        /// </summary>
        protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(20);

        /// <summary>
        /// The default number of attempts to retry HTTP requests when navigating to a URL.
        /// This is used to handle transient network issues or server errors that may occur when trying to
        /// load a page. The default value is set to 3 attempts.
        /// </summary>
        protected static readonly int DefaultHttpAttemptsAmount = 3;

        ~WebProcessor() => Dispose(false);

        /// <summary>
        /// Disposes the web processor, closing all tabs and switching back to the first tab.
        /// This method is called when the processor is no longer needed, ensuring that all resources are
        /// released properly.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (string tab in Tabs.ToList())
            {
                CloseTab(tab);
            }
        }

        /// <summary>
        /// Switches to the specified tab.
        /// </summary>
        /// <param name="index">The index of the tab to switch to.</param>
        public void SwitchToTab(int index)
            => SwitchToTab(Tabs[index]);
        /// <summary>
        /// Switches to the specified tab.
        /// </summary>
        /// <param name="tab">The tab to switch to.</param>
        public void SwitchToTab(string tab)
        {
            if (tab.Equals(CurrentTab))
            {
                return;
            }

            if (!Tabs.Contains(tab))
            {
                throw new ArgumentException("The specified tab does not belong to this processor.");
            }

            CurrentTab = tab;
            PerformSwitchToTab(tab);
        }

        /// <summary>
        /// Creates a new tab in the web processor.
        /// </summary>
        /// <returns>The new tab.</returns>
        public string NewTab()
            => NewTab("about:blank");
        /// <summary>
        /// Creates a new tab in the web processor with the specified URL.
        /// </summary>
        /// <param name="url">The URL to open in the new tab.</param>
        /// <returns>The new tab.</returns>
        public string NewTab(string url)
        {
            string tab = PerformNewTab(url);

            Tabs.Add(tab);

            SwitchToTab(tab);

            return tab;
        }

        /// <summary>
        /// Closes the current tab in the web processor.
        /// </summary>
        public void CloseTab() => CloseTab(CurrentTab);
        /// <summary>
        /// Closes the specified tab in the web processor.
        /// </summary>
        /// <param name="tab">The tab to close.</param>
        public void CloseTab(string tab)
        {
            if (!Tabs.Contains(tab))
            {
                throw new ArgumentException("The specified tab does not belong to this processor.");
            }

            PerformCloseTab(tab);
            Tabs.Remove(tab);
        }

        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        public void GoToUrl(string url) => GoToUrl(url, DefaultHttpAttemptsAmount);
        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="httpRetries">The number of HTTP retries to attempt if the request fails.</param>
        public void GoToUrl(string url, int httpRetries) => GoToUrl(url, httpRetries, DefaultWaitDuration);
        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="retryDelay">The delay to wait before retrying the request if it fails.</param>
        public void GoToUrl(string url, TimeSpan retryDelay) => GoToUrl(url, DefaultHttpAttemptsAmount, retryDelay);
        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="httpRetries">The number of HTTP retries to attempt if the request fails.</param>
        /// <param name="retryDelay">The delay to wait before retrying the request if it fails.</param>
        public void GoToUrl(string url, int httpRetries, TimeSpan retryDelay)
        {
            if (string.IsNullOrWhiteSpace(CurrentTab))
            {
                NewTab();
            }
            else
            {
                SwitchToTab(CurrentTab);
            }

            PerformGoToUrl(url, httpRetries, retryDelay);
        }

        /// <summary>
        /// Navigates to the specified iframe in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath for the iframe to navigate to.</param>
        public void GoToIframe(string xpath)
            => GoToUrl(GetSource(xpath));

        /// <summary>
        /// Switches to the specified iframe in the current tab of the web processor with a timeout.
        /// </summary>
        /// <param name="xpath">The XPath for the iframe to switch to.</param>
        public void SwitchToIframe(string xpath)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout)
            {
                try
                {
                    PerformSwitchToIframe(xpath);
                }
                finally
                {
                    Wait();
                }
            }
        }

        /// <summary>
        /// Refreshes the current tab in the web processor.
        /// </summary>
        public void Refresh()
            => PerformRefresh();

        /// <summary>
        /// Executes a script in the context of the current tab in the web processor.
        /// </summary>
        /// <param name="script">The script to execute.</param>
        public void ExecuteScript(string script)
        {
            SwitchToTab(CurrentTab);
            PerformExecuteScript(script);
        }

        /// <summary>
        /// Gets the value of a variable in the context of the current tab in the web processor.
        /// </summary>
        /// <param name="variableName">The name of the variable to get the value of.</param>
        /// <returns>The value of the variable.</returns>
        public string GetVariableValue(string variableName)
        {
            Wait();

            return PerformExecuteScript($"return {variableName};");
        }

        /// <summary>
        /// Accepts the current alert in the web processor.
        /// </summary>
        public void AcceptAlert()
            => PerformAcceptAlert();

        /// <summary>
        /// Dismisses the current alert in the web processor.
        /// </summary>
        public void DismissAlert()
            => PerformDismissAlert();

        /// <summary>
        /// Gets the HTML source of the current page of the web processor.
        /// </summary>
        /// <returns>The HTML source of the current page.</returns>
        public string GetPageSource()
            => PerformGetPageSource();

        /// <summary>
        /// Gets the value of the specified attribute for the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="attribute">The name of the attribute to get the value of.</param>
        /// <returns>The value of the attribute for the first matching element.</returns>
        public string GetAttribute(string xpath, string attribute)
            => GetAttributeOfMany(xpath, attribute).First();

        /// <summary>
        /// Gets the values of the specified attribute for all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <param name="attribute">The name of the attribute to get the values of.</param>
        /// <returns>A list of values of the attribute for all matching elements.</returns>
        public IList<string> GetAttributeOfMany(string xpath, string attribute)
        {
            if (RetryOnDomFailure)
            {
                return ExecutionUtils.RetryUntilTheResultIsNotNull<IList<string>>(
                    this,
                    () => [.. PerformGetAttribute(xpath, attribute)],
                    DefaultTimeout);
            }

            return [.. PerformGetAttribute(xpath, attribute)];
        }

        /// <summary>
        /// Gets the class name of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The class name of the first matching element.</returns>
        public string GetClass(string xpath)
            => GetAttribute(xpath, "class");

        /// <summary>
        /// Gets the class names of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of class names of all matching elements.</returns>
        public IList<string> GetClassOfMany(string xpath)
            => GetAttributeOfMany(xpath, "class");

        /// <summary>
        /// Gets the class names of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>A list of class names of the first matching element.</returns>
        public IList<string> GetClasses(string xpath)
            => GetClass(xpath).Split(' ');

        /// <summary>
        /// Gets the hyperlink of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The hyperlink of the first matching element.</returns>
        public string GetHyperlink(string xpath)
            => GetAttribute(xpath, "href");

        /// <summary>
        /// Gets the hyperlinks of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of hyperlinks of all matching elements.</returns>
        public IList<string> GetHyperlinkOfMany(string xpath)
            => GetAttributeOfMany(xpath, "href");

        /// <summary>
        /// Gets the source of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The source of the first matching element.</returns>
        public string GetSource(string xpath)
            => GetAttribute(xpath, "src");

        /// <summary>
        /// Gets the sources of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of sources of all matching elements.</returns>
        public IList<string> GetSourceOfMany(string xpath)
            => GetAttributeOfMany(xpath, "src");

        /// <summary>
        /// Gets the style of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The style of the first matching element.</returns>
        public string GetStyle(string xpath)
            => GetAttribute(xpath, "style");

        /// <summary>
        /// Gets the styles of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of styles of all matching elements.</returns>
        public IList<string> GetStyleOfMany(string xpath)
            => GetAttributeOfMany(xpath, "style");

        /// <summary>
        /// Gets the ID of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The ID of the first matching element.</returns>
        public string GetId(string xpath)
            => GetAttribute(xpath, "id");

        /// <summary>
        /// Gets the IDs of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of IDs of all matching elements.</returns>
        public IList<string> GetIdOfMany(string xpath)
            => GetAttributeOfMany(xpath, "id");

        /// <summary>
        /// Gets the value of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The value of the first matching element.</returns>
        public string GetValue(string xpath)
            => GetAttribute(xpath, "value");

        /// <summary>
        /// Gets the values of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of values of all matching elements.</returns>
        public IList<string> GetValueOfMany(string xpath)
            => GetAttributeOfMany(xpath, "value");

        /// <summary>
        /// Gets the text of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The text of the first matching element.</returns>
        public string GetText(string xpath)
            => GetTextOfMany(xpath).First();

        /// <summary>
        /// Gets the text of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of text of all matching elements.</returns>
        public IList<string> GetTextOfMany(string xpath)
        {
            if (RetryOnDomFailure)
            {
                return ExecutionUtils.RetryUntilTheResultIsNotNull<IList<string>>(
                    this,
                    () => [.. PerformGetText(xpath)],
                    DefaultTimeout);
            }

            return [.. PerformGetText(xpath)];
        }

        /// <summary>
        /// Gets the selected text of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The selected text of the first matching element.</returns>
        public string GetSelectedText(string xpath)
            => GetSelectedTextOfMany(xpath).First();

        /// <summary>
        /// Gets the selected text of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of selected text of all matching elements.</returns>
        public IList<string> GetSelectedTextOfMany(string xpath)
        {
            if (RetryOnDomFailure)
            {
                return ExecutionUtils.RetryUntilTheResultIsNotNull<IList<string>>(
                    this,
                    () => [.. PerformGetSelectedText(xpath)],
                    DefaultTimeout);
            }

            return [.. PerformGetSelectedText(xpath)];
        }

        /// <summary>
        /// Sets the value of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="text">The text to set as the value.</param>
        public void SetText(string xpath, string text)
            => PerformSetText(xpath, text);

        /// <summary>
        /// Appends text to the value of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="text">The text to append to the value.</param>
        public void AppendText(string xpath, string text)
            => SetText(xpath, GetText(xpath) + text);

        /// <summary>
        /// Clears the text of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void ClearText(string xpath)
            => SetText(xpath, string.Empty);

        /// <summary>
        /// Checks if the first element matching the XPath has a specific class in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="className">The class name to check for.</param>
        /// <returns>True if the element has the class, false otherwise.</returns>
        public bool HasClass(string xpath, string className)
            => GetClasses(xpath).Contains(className);

        /// <summary>
        /// Checks if the first element matching the XPath is selected in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>True if the element is selected, false otherwise.</returns>
        public bool IsSelected(string xpath)
            => PerformIsSelected(xpath);

        /// <summary>
        /// Waits for the default amount of time.
        /// </summary>
        public void Wait()
            => Wait(DefaultWaitDuration);
        /// <summary>
        /// Waits for a specified number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to wait.</param>
        public void Wait(int milliseconds)
            => Wait(TimeSpan.FromMilliseconds(milliseconds));
        /// <summary>
        /// Waits until the specified target time is reached.
        /// </summary>
        /// <param name="targetTime">The target time to wait until.</param>
        public void Wait(DateTime targetTime)
            => Wait(targetTime - DateTime.Now);
        /// <summary>
        /// Waits for a specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to wait for.</param>
        public void Wait(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMilliseconds <= 0)
            {
                return;
            }

            Thread.Sleep(timeSpan);
        }

        /// <summary>
        /// Waits for the text length of the first element matching the XPath to reach a specified length in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="length">The length to wait for.</param>
        public void WaitForTextLength(string xpath, int length)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout)
            {
                bool conditionMet =
                    GetValue(xpath).Length == length ||
                    GetText(xpath).Length == length;

                if (conditionMet)
                {
                    break;
                }

                Wait();
            }
        }

        /// <summary>
        /// Waits for any element matching the provided XPaths to exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        public void WaitForAnyElementToExist(params string[] xpaths)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   !DoesAnyElementExist(xpaths))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for all elements matching the provided XPaths to exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        public void WaitForAllElementsToExist(params string[] xpaths)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   !DoAllElementsExist(xpaths))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for any element matching the provided XPaths to be visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        public void WaitForAnyElementToBeVisible(params string[] xpaths)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   !IsAnyElementVisible(xpaths))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for all elements matching the provided XPaths to be visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        public void WaitForAllElementsToBeVisible(params string[] xpaths)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   !AreAllElementsVisible(xpaths))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for an element matching the provided XPath to exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void WaitForElementToExist(string xpath)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   !DoesElementExist(xpath))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for an element matching the provided XPath to disappear in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void WaitForElementToDisappear(string xpath)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   DoesElementExist(xpath))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for an element matching the provided XPath to be visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void WaitForElementToBeVisible(string xpath)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   !IsElementVisible(xpath))
            {
                Wait();
            }
        }

        /// <summary>
        /// Waits for an element matching the provided XPath to be invisible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void WaitForElementToBeInvisible(string xpath)
        {
            SwitchToTab(CurrentTab);

            DateTime beginTime = DateTime.Now;

            while (DateTime.Now - beginTime < DefaultTimeout &&
                   IsElementVisible(xpath))
            {
                Wait();
            }
        }

        /// <summary>
        /// Checks if all elements matching the provided XPaths exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if all elements exist, false otherwise.</returns>
        public bool DoAllElementsExist(params string[] xpaths)
            => xpaths.All(DoesElementExist);

        /// <summary>
        /// Checks if any element matching the provided XPaths exists in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if any element exists, false otherwise.</returns>
        public bool DoesAnyElementExist(params string[] xpaths)
            => xpaths.Any(DoesElementExist);

        /// <summary>
        /// Checks if an element matching the provided XPath exists in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>True if the element exists, false otherwise.</returns>
        public bool DoesElementExist(string xpath)
        {
            SwitchToTab(CurrentTab);

            return PerformDoesElementExist(xpath);
        }

        /// <summary>
        /// Checks if all elements matching the provided XPaths are visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if all elements are visible, false otherwise.</returns>
        public bool AreAllElementsVisible(params string[] xpaths)
            => xpaths.All(IsElementVisible);

        /// <summary>
        /// Checks if any element matching the provided XPaths is visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if any element is visible, false otherwise.</returns>
        public bool IsAnyElementVisible(params string[] xpaths)
            => xpaths.Any(IsElementVisible);

        /// <summary>
        /// Checks if an element matching the provided XPath is visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>True if the element is visible, false otherwise.</returns>
        public bool IsElementVisible(string xpath)
        {
            SwitchToTab(CurrentTab);

            if (!DoesElementExist(xpath))
            {
                return false;
            }

            return PerformIsElementVisible(xpath);
        }

        /// <summary>
        /// Moves the mouse cursor to the first element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void MoveToElement(string xpath)
            => PerformMoveToElement(xpath);

        /// <summary>
        /// Clicks on any of the elements matching the provided XPaths in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        public void ClickAny(params string[] xpaths)
        {
            bool clicked = false;

            foreach (string xpath in xpaths)
            {
                if (IsElementVisible(xpath))
                {
                    Click(xpath);

                    clicked = true;
                    break;
                }
            }

            if (!clicked)
            {
                // TODO: Use a proper message
                throw new ArgumentException("No element to click.");
            }
        }

        /// <summary>
        /// Clicks on the first element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        public void Click(string xpath)
            => PerformClick(xpath);

        /// <summary>
        /// Clicks on the first element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="status">The status to wait for after clicking.</param>
        public void UpdateCheckbox(string xpath, bool status)
        {
            if (!PerformIsCheckboxChecked(xpath) == status)
            {
                Click(xpath);
            }
        }

        /// <summary>
        /// Selects an option by index in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        /// <param name="index">The index of the option to select.</param>
        public void SelectOptionByIndex(string xpath, int index)
            => PerformSelectOptionByIndex(xpath, index);

        /// <summary>
        /// Selects an option by value in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        /// <param name="value">The value of the option to select.</param>
        public void SelectOptionByValue(string xpath, object value)
        {
            string stringValue;

            if (value is string valueAsString)
            {
                stringValue = valueAsString;
            }
            else
            {
                stringValue = value.ToString();
            }

            PerformSelectOptionByValue(xpath, stringValue);
        }

        /// <summary>
        /// Selects an option by text in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        /// <param name="text">The text of the option to select.</param>
        public void SelectOptionByText(string xpath, string text)
            => PerformSelectOptionByText(xpath, text);

        /// <summary>
        /// Selects a random option in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        public void SelectRandomOption(string xpath)
        {
            int optionsCount = PerformGetSelectOptionsCount(xpath);
            int option = Random.Next(0, optionsCount);

            SelectOptionByIndex(xpath, option);
        }

        protected abstract bool PerformDoesElementExist(string xpath);
        protected abstract bool PerformIsCheckboxChecked(string xpath);
        protected abstract bool PerformIsElementVisible(string xpath);
        protected abstract bool PerformIsSelected(string xpath);
        protected abstract IEnumerable<string> PerformGetAttribute(string xpath, string attribute);
        protected abstract IEnumerable<string> PerformGetSelectedText(string xpath);
        protected abstract IEnumerable<string> PerformGetText(string xpath);
        protected abstract int PerformGetSelectOptionsCount(string xpath);
        protected abstract string PerformExecuteScript(string script);
        protected abstract string PerformGetPageSource();
        protected abstract string PerformNewTab(string url);
        protected abstract void PerformAcceptAlert();
        protected abstract void PerformClick(string xpath);
        protected abstract void PerformCloseTab(string tab);
        protected abstract void PerformDismissAlert();
        protected abstract void PerformGoToUrl(string url, int httpRetries, TimeSpan retryDelay);
        protected abstract void PerformMoveToElement(string xpath);
        protected abstract void PerformRefresh();
        protected abstract void PerformSelectOptionByIndex(string xpath, int index);
        protected abstract void PerformSelectOptionByText(string xpath, string text);
        protected abstract void PerformSelectOptionByValue(string xpath, object value);
        protected abstract void PerformSetText(string xpath, string text);
        protected abstract void PerformSwitchToIframe(string xpath);
        protected abstract void PerformSwitchToTab(string tab);
    }
}
