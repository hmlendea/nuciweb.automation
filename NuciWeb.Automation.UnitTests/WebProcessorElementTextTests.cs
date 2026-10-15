using System;
using System.Collections.Generic;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorElementTextTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenElementExists_WhenGettingText_ThenReturnsText()
        {
            _processor.SetElement("//div", "Hello World");

            string result = _processor.GetText("//div");

            Assert.That(result, Is.EqualTo("Hello World"));
        }

        [Test]
        public void GivenElementExists_WhenGettingTextOfMany_ThenReturnsList()
        {
            _processor.SetElement("//div", "Hello World");

            IList<string> result = _processor.GetTextOfMany("//div");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Hello World"));
        }

        [Test]
        public void GivenRetryOnDomFailureEnabled_WhenGettingText_ThenUsesRetry()
        {
            _processor.RetryOnDomFailure = true;
            _processor.SetElement("//div", "Hello World");

            string result = _processor.GetText("//div");

            Assert.That(result, Is.EqualTo("Hello World"));
        }

        [Test]
        public void GivenElementExists_WhenGettingSelectedText_ThenReturnsSelectedText()
        {
            _processor.SetElement("//option", "Selected Option");

            string result = _processor.GetSelectedText("//option");

            Assert.That(result, Is.EqualTo("Selected Option"));
        }

        [Test]
        public void GivenElementExists_WhenGettingSelectedTextOfMany_ThenReturnsList()
        {
            _processor.SetElement("//option", "Selected Option");

            IList<string> result = _processor.GetSelectedTextOfMany("//option");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Selected Option"));
        }

        [Test]
        public void GivenRetryOnDomFailureEnabled_WhenGettingSelectedText_ThenUsesRetry()
        {
            _processor.RetryOnDomFailure = true;
            _processor.SetElement("//option", "Selected Option");

            string result = _processor.GetSelectedText("//option");

            Assert.That(result, Is.EqualTo("Selected Option"));
        }

        [Test]
        public void GivenElementExists_WhenSettingText_ThenUpdatesElement()
        {
            _processor.SetElement("//input", "old value");

            _processor.SetText("//input", "new value");

            Assert.That(_processor.GetText("//input"), Is.EqualTo("new value"));
        }

        [Test]
        public void GivenElementExists_WhenAppendingText_ThenAppendsToExisting()
        {
            _processor.SetElement("//input", "Hello");

            _processor.AppendText("//input", " World");

            Assert.That(_processor.GetText("//input"), Is.EqualTo("Hello World"));
        }

        [Test]
        public void GivenElementExists_WhenClearingText_ThenSetsEmpty()
        {
            _processor.SetElement("//input", "Hello World");

            _processor.ClearText("//input");

            Assert.That(_processor.GetText("//input"), Is.EqualTo(string.Empty));
        }
    }
}