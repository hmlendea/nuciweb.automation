using System;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorScriptAndAlertTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenProcessor_WhenExecutingScript_ThenCallsPerformExecuteScript()
        {
            _processor.ExecuteScript("alert('test');");
        }

        [Test]
        public void GivenProcessor_WhenGettingVariableValue_ThenCallsPerformExecuteScriptWithReturn()
        {
            string result = _processor.GetVariableValue("window.location.href");
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GivenProcessor_WhenAcceptingAlert_ThenCallsPerformAcceptAlert()
        {
            _processor.AcceptAlert();
        }

        [Test]
        public void GivenProcessor_WhenDismissingAlert_ThenCallsPerformDismissAlert()
        {
            _processor.DismissAlert();
        }

        [Test]
        public void GivenProcessor_WhenGettingPageSource_ThenCallsPerformGetPageSource()
        {
            string source = _processor.GetPageSource();
            Assert.That(source, Is.EqualTo(string.Empty));
        }
    }
}