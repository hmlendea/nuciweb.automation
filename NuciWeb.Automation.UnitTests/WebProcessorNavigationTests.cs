using System;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorNavigationTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenProcessor_WhenNavigatingToUrl_ThenCallsPerformGoToUrl()
        {
            _processor.GoToUrl("https://example.com");
        }

        [Test]
        public void GivenProcessor_WhenNavigatingToUrlWithRetries_ThenCallsPerformGoToUrlWithRetries()
        {
            _processor.GoToUrl("https://example.com", 5);
        }

        [Test]
        public void GivenProcessor_WhenNavigatingToUrlWithRetryDelay_ThenCallsPerformGoToUrlWithRetryDelay()
        {
            _processor.GoToUrl("https://example.com", TimeSpan.FromSeconds(2));
        }

        [Test]
        public void GivenProcessor_WhenNavigatingToUrlWithRetriesAndDelay_ThenCallsPerformGoToUrlWithBoth()
        {
            _processor.GoToUrl("https://example.com", 5, TimeSpan.FromSeconds(2));
        }

        [Test]
        public void GivenProcessor_WhenNavigatingToIframe_ThenCallsGoToUrlWithSource()
        {
            _processor.SetElement("//iframe", "https://iframe.example.com");
            _processor.GoToIframe("//iframe");
        }

        [Test]
        public void GivenProcessor_WhenSwitchingToIframe_ThenCallsPerformSwitchToIframe()
        {
            _processor.SwitchToIframe("//iframe");
        }

        [Test]
        public void GivenProcessor_WhenRefreshing_ThenCallsPerformRefresh()
        {
            _processor.Refresh();
        }
    }
}