using System;
using System.Collections.Generic;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorElementAttributeTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenElementExists_WhenGettingAttribute_ThenReturnsValue()
        {
            _processor.SetElement("//input", "test-value");

            string result = _processor.GetAttribute("//input", "value");

            Assert.That(result, Is.EqualTo("test-value"));
        }

        [Test]
        public void GivenElementExists_WhenGettingAttributeOfMany_ThenReturnsList()
        {
            _processor.SetElement("//input", "test-value");

            IList<string> result = _processor.GetAttributeOfMany("//input", "value");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("test-value"));
        }

        [Test]
        public void GivenElementDoesNotExist_WhenGettingAttribute_ThenThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _processor.GetAttribute("//nonexistent", "value"));
        }

        [Test]
        public void GivenRetryOnDomFailureEnabled_WhenGettingAttribute_ThenUsesRetry()
        {
            _processor.RetryOnDomFailure = true;
            _processor.SetElement("//input", "test-value");

            string result = _processor.GetAttribute("//input", "value");

            Assert.That(result, Is.EqualTo("test-value"));
        }

        [Test]
        public void GivenElementExists_WhenGettingClass_ThenReturnsClassAttribute()
        {
            _processor.SetElement("//div", "class1 class2");

            string result = _processor.GetClass("//div");

            Assert.That(result, Is.EqualTo("class1 class2"));
        }

        [Test]
        public void GivenElementExists_WhenGettingClassOfMany_ThenReturnsList()
        {
            _processor.SetElement("//div", "class1 class2");

            IList<string> result = _processor.GetClassOfMany("//div");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("class1 class2"));
        }

        [Test]
        public void GivenElementExists_WhenGettingClasses_ThenReturnsSplitClasses()
        {
            _processor.SetElement("//div", "class1 class2");

            IList<string> result = _processor.GetClasses("//div");

            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Contains.Item("class1"));
            Assert.That(result, Contains.Item("class2"));
        }

        [Test]
        public void GivenElementExists_WhenGettingHyperlink_ThenReturnsHrefAttribute()
        {
            _processor.SetElement("//a", "https://example.com");

            string result = _processor.GetHyperlink("//a");

            Assert.That(result, Is.EqualTo("https://example.com"));
        }

        [Test]
        public void GivenElementExists_WhenGettingHyperlinkOfMany_ThenReturnsList()
        {
            _processor.SetElement("//a", "https://example.com");

            IList<string> result = _processor.GetHyperlinkOfMany("//a");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("https://example.com"));
        }

        [Test]
        public void GivenElementExists_WhenGettingSource_ThenReturnsSrcAttribute()
        {
            _processor.SetElement("//img", "image.png");

            string result = _processor.GetSource("//img");

            Assert.That(result, Is.EqualTo("image.png"));
        }

        [Test]
        public void GivenElementExists_WhenGettingSourceOfMany_ThenReturnsList()
        {
            _processor.SetElement("//img", "image.png");

            IList<string> result = _processor.GetSourceOfMany("//img");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("image.png"));
        }

        [Test]
        public void GivenElementExists_WhenGettingStyle_ThenReturnsStyleAttribute()
        {
            _processor.SetElement("//div", "color: red;");

            string result = _processor.GetStyle("//div");

            Assert.That(result, Is.EqualTo("color: red;"));
        }

        [Test]
        public void GivenElementExists_WhenGettingStyleOfMany_ThenReturnsList()
        {
            _processor.SetElement("//div", "color: red;");

            IList<string> result = _processor.GetStyleOfMany("//div");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("color: red;"));
        }

        [Test]
        public void GivenElementExists_WhenGettingId_ThenReturnsIdAttribute()
        {
            _processor.SetElement("//div", "element-id");

            string result = _processor.GetId("//div");

            Assert.That(result, Is.EqualTo("element-id"));
        }

        [Test]
        public void GivenElementExists_WhenGettingIdOfMany_ThenReturnsList()
        {
            _processor.SetElement("//div", "element-id");

            IList<string> result = _processor.GetIdOfMany("//div");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("element-id"));
        }

        [Test]
        public void GivenElementExists_WhenGettingValue_ThenReturnsValueAttribute()
        {
            _processor.SetElement("//input", "input-value");

            string result = _processor.GetValue("//input");

            Assert.That(result, Is.EqualTo("input-value"));
        }

        [Test]
        public void GivenElementExists_WhenGettingValueOfMany_ThenReturnsList()
        {
            _processor.SetElement("//input", "input-value");

            IList<string> result = _processor.GetValueOfMany("//input");

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("input-value"));
        }
    }
}