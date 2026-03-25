using System;
using System.Collections.Generic;

namespace NuciWeb.Automation
{
    /// <summary>
    /// Interface for a web processor that manages browser tabs and interactions.
    /// This interface provides methods for tab management, navigation, element interaction,
    /// and alert handling in a web browser context.
    /// It is designed to be implemented by classes that handle web automation tasks,
    /// such as opening URLs, switching between tabs, and interacting with web elements.
    /// The interface also includes methods for executing scripts, handling alerts,
    /// and retrieving information from the current page, such as HTML source, attributes,
    /// class names, hyperlinks, styles, IDs, and more.
    /// Implementations of this interface should ensure proper resource management by implementing IDisposable.
    /// The interface is intended to be used in scenarios where web automation is required,
    /// such as testing, web scraping, or browser automation tasks.
    /// </summary>
    public interface IWebProcessor : IDisposable
    {
        /// <summary>
        /// Gets the name of the web processor.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to retry on DOM failure when getting attributes or classes of elements.
        /// </summary>
        bool RetryOnDomFailure { get; set; }

        /// <summary>
        /// Gets the list of tabs currently managed by this web processor.
        /// Each tab is represented by its window handle.
        /// </summary>
        IList<string> Tabs { get; }

        /// <summary>
        /// Gets the current tab (window handle) that this processor is working with.
        /// </summary>
        string CurrentTab { get; }

        /// <summary>
        /// Switches to the specified tab.
        /// </summary>
        /// <param name="tab">The tab to switch to.</param>
        void SwitchToTab(string tab);

        /// <summary>
        /// Creates a new tab in the web processor.
        /// </summary>
        /// <returns>The new tab.</returns>
        string NewTab();
        /// <summary>
        /// Creates a new tab in the web processor with the specified URL.
        /// </summary>
        /// <param name="url">The URL to open in the new tab.</param>
        /// <returns>The new tab.</returns>
        string NewTab(string url);

        /// <summary>
        /// Closes the current tab in the web processor.
        /// </summary>
        void CloseTab();
        /// <summary>
        /// Closes the specified tab in the web processor.
        /// </summary>
        /// <param name="tab">The tab to close.</param>
        void CloseTab(string tab);

        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        void GoToUrl(string url);
        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="httpRetries">The number of HTTP retries to attempt if the request fails.</param>
        void GoToUrl(string url, int httpRetries);
        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="retryDelay">The delay to wait before retrying the request if it fails.</param>
        void GoToUrl(string url, TimeSpan retryDelay);
        /// <summary>
        /// Navigates to the specified URL in the current tab of the web processor.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <param name="httpRetries">The number of HTTP retries to attempt if the request fails.</param>
        /// <param name="retryDelay">The delay to wait before retrying the request if it fails.</param>
        void GoToUrl(string url, int httpRetries, TimeSpan retryDelay);

        /// <summary>
        /// Navigates to the specified iframe in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath for the iframe to navigate to.</param>
        void GoToIframe(string xpath);

        /// <summary>
        /// Switches to the specified iframe in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath for the iframe to switch to.</param>
        void SwitchToIframe(string xpath);

        /// <summary>
        /// Refreshes the current tab in the web processor.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Executes a script in the context of the current tab in the web processor.
        /// </summary>
        /// <param name="script">The script to execute.</param>
        void ExecuteScript(string script);
        /// <summary>
        /// Gets the value of a variable in the context of the current tab in the web processor.
        /// </summary>
        /// <param name="variableName">The name of the variable to get the value of.</param>
        /// <returns>The value of the variable.</returns>
        string GetVariableValue(string variableName);

        /// <summary>
        /// Accepts the current alert in the web processor.
        /// </summary>
        void AcceptAlert();

        /// <summary>
        /// Dismisses the current alert in the web processor.
        /// </summary>
        void DismissAlert();

        /// <summary>
        /// Gets the HTML source of the current page of the web processor.
        /// </summary>
        /// <returns>The HTML source of the current page.</returns>
        string GetPageSource();

        /// <summary>
        /// Gets the value of the specified attribute for the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="attribute">The name of the attribute to get the value of.</param>
        /// <returns>The value of the attribute for the first matching element.</returns>
        string GetAttribute(string xpath, string attribute);

        /// <summary>
        /// Gets the values of the specified attribute for all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <param name="attribute">The name of the attribute to get the values of.</param>
        /// <returns>A list of values of the attribute for all matching elements.</returns>
        IList<string> GetAttributeOfMany(string xpath, string attribute);

        /// <summary>
        /// Gets the class name of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The class name of the first matching element.</returns>
        string GetClass(string xpath);

        /// <summary>
        /// Gets the class names of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of class names of all matching elements.</returns>
        IList<string> GetClassOfMany(string xpath);

        /// <summary>
        /// Gets the class names of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>A list of class names of the first matching element.</returns>
        IList<string> GetClasses(string xpath);

        /// <summary>
        /// Gets the hyperlink of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The hyperlink of the first matching element.</returns>
        string GetHyperlink(string xpath);

        /// <summary>
        /// Gets the hyperlinks of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of hyperlinks of all matching elements.</returns>
        IList<string> GetHyperlinkOfMany(string xpath);

        /// <summary>
        /// Gets the source of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The source of the first matching element.</returns>
        string GetSource(string xpath);

        /// <summary>
        /// Gets the sources of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of sources of all matching elements.</returns>
        IList<string> GetSourceOfMany(string xpath);

        /// <summary>
        /// Gets the style of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The style of the first matching element.</returns>
        string GetStyle(string xpath);

        /// <summary>
        /// Gets the styles of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of styles of all matching elements.</returns>
        IList<string> GetStyleOfMany(string xpath);

        /// <summary>
        /// Gets the ID of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The ID of the first matching element.</returns>
        string GetId(string xpath);

        /// <summary>
        /// Gets the IDs of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of IDs of all matching elements.</returns>
        IList<string> GetIdOfMany(string xpath);

        /// <summary>
        /// Gets the value of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The value of the first matching element.</returns>
        string GetValue(string xpath);

        /// <summary>
        /// Gets the values of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of values of all matching elements.</returns>
        IList<string> GetValueOfMany(string xpath);

        /// <summary>
        /// Gets the text of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The text of the first matching element.</returns>
        string GetText(string xpath);

        /// <summary>
        /// Gets the text of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of text of all matching elements.</returns>
        IList<string> GetTextOfMany(string xpath);

        /// <summary>
        /// Gets the selected text of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>The selected text of the first matching element.</returns>
        string GetSelectedText(string xpath);

        /// <summary>
        /// Gets the selected text of all elements matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match elements against.</param>
        /// <returns>A list of selected text of all matching elements.</returns>
        IList<string> GetSelectedTextOfMany(string xpath);

        /// <summary>
        /// Sets the value of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="text">The text to set as the value.</param>
        void SetText(string xpath, string text);

        /// <summary>
        /// Appends text to the value of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="text">The text to append to the value.</param>
        void AppendText(string xpath, string text);

        /// <summary>
        /// Clears the text of the first element matching the XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void ClearText(string xpath);

        /// <summary>
        /// Checks if the first element matching the XPath has a specific class in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="className">The class name to check for.</param>
        /// <returns>True if the element has the class, false otherwise.</returns>
        bool HasClass(string xpath, string className);

        /// <summary>
        /// Checks if the first element matching the XPath is selected in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>True if the element is selected, false otherwise.</returns>
        bool IsSelected(string xpath);

        /// <summary>
        /// Waits for the default amount of time.
        /// </summary>
        void Wait();
        /// <summary>
        /// Waits for a specified number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to wait.</param>
        void Wait(int milliseconds);
        /// <summary>
        /// Waits until the specified target time is reached.
        /// </summary>
        /// <param name="targetTime">The target time to wait until.</param>
        void Wait(DateTime targetTime);
        /// <summary>
        /// Waits for a specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to wait for.</param>
        void Wait(TimeSpan timeSpan);

        /// <summary>
        /// Waits for the text length of the first element matching the XPath to reach a specified length in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="length">The length to wait for.</param>
        void WaitForTextLength(string xpath, int length);

        /// <summary>
        /// Waits for any element matching the provided XPaths to exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        void WaitForAnyElementToExist(params string[] xpaths);

        /// <summary>
        /// Waits for all elements matching the provided XPaths to exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        void WaitForAllElementsToExist(params string[] xpaths);

        /// <summary>
        /// Waits for any element matching the provided XPaths to be visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        void WaitForAnyElementToBeVisible(params string[] xpaths);

        /// <summary>
        /// Waits for all elements matching the provided XPaths to be visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        void WaitForAllElementsToBeVisible(params string[] xpaths);

        /// <summary>
        /// Waits for an element matching the provided XPath to exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void WaitForElementToExist(string xpath);

        /// <summary>
        /// Waits for an element matching the provided XPath to disappear in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void WaitForElementToDisappear(string xpath);

        /// <summary>
        /// Waits for an element matching the provided XPath to be visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void WaitForElementToBeVisible(string xpath);

        /// <summary>
        /// Waits for an element matching the provided XPath to be invisible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void WaitForElementToBeInvisible(string xpath);

        /// <summary>
        /// Checks if all elements matching the provided XPaths exist in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if all elements exist, false otherwise.</returns>
        bool DoAllElementsExist(params string[] xpaths);

        /// <summary>
        /// Checks if any element matching the provided XPaths exists in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if any element exists, false otherwise.</returns>
        bool DoesAnyElementExist(params string[] xpaths);

        /// <summary>
        /// Checks if an element matching the provided XPath exists in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>True if the element exists, false otherwise.</returns>
        bool DoesElementExist(string xpath);

        /// <summary>
        /// Checks if all elements matching the provided XPaths are visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if all elements are visible, false otherwise.</returns>
        bool AreAllElementsVisible(params string[] xpaths);

        /// <summary>
        /// Checks if any element matching the provided XPaths is visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        /// <returns>True if any element is visible, false otherwise.</returns>
        bool IsAnyElementVisible(params string[] xpaths);

        /// <summary>
        /// Checks if an element matching the provided XPath is visible in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <returns>True if the element is visible, false otherwise.</returns>
        bool IsElementVisible(string xpath);

        /// <summary>
        /// Moves the mouse cursor to the first element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void MoveToElement(string xpath);

        /// <summary>
        /// Clicks on any of the elements matching the provided XPaths in the current tab of the web processor.
        /// </summary>
        /// <param name="xpaths">The XPaths to match elements against.</param>
        void ClickAny(params string[] xpaths);

        /// <summary>
        /// Clicks on the first element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        void Click(string xpath);

        /// <summary>
        /// Clicks on the first element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the element against.</param>
        /// <param name="status">The status to wait for after clicking.</param>
        void UpdateCheckbox(string xpath, bool status);

        /// <summary>
        /// Selects an option by index in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        /// <param name="index">The index of the option to select.</param>
        void SelectOptionByIndex(string xpath, int index);

        /// <summary>
        /// Selects an option by value in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        /// <param name="value">The value of the option to select.</param>
        void SelectOptionByValue(string xpath, object value);

        /// <summary>
        /// Selects an option by text in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        /// <param name="text">The text of the option to select.</param>
        void SelectOptionByText(string xpath, string text);

        /// <summary>
        /// Selects a random option in the first select element matching the provided XPath in the current tab of the web processor.
        /// </summary>
        /// <param name="xpath">The XPath to match the select element against.</param>
        void SelectRandomOption(string xpath);
    }
}
