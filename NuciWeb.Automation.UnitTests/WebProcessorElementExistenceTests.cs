using System;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorElementExistenceTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenAllElementsExist_WhenCheckingDoAllElementsExist_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElement("//span", "value");

            bool result = _processor.DoAllElementsExist("//div", "//span");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenOneElementMissing_WhenCheckingDoAllElementsExist_ThenReturnsFalse()
        {
            _processor.SetElement("//div", "value");

            bool result = _processor.DoAllElementsExist("//div", "//span");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenAnyElementExists_WhenCheckingDoesAnyElementExist_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "value");

            bool result = _processor.DoesAnyElementExist("//div", "//span");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenNoElementsExist_WhenCheckingDoesAnyElementExist_ThenReturnsFalse()
        {
            bool result = _processor.DoesAnyElementExist("//div", "//span");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenElementExists_WhenCheckingDoesElementExist_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "value");

            bool result = _processor.DoesElementExist("//div");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenElementDoesNotExist_WhenCheckingDoesElementExist_ThenReturnsFalse()
        {
            bool result = _processor.DoesElementExist("//nonexistent");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenAllElementsVisible_WhenCheckingAreAllElementsVisible_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);
            _processor.SetElement("//span", "value");
            _processor.SetElementVisibility("//span", true);

            bool result = _processor.AreAllElementsVisible("//div", "//span");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenOneElementNotVisible_WhenCheckingAreAllElementsVisible_ThenReturnsFalse()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);
            _processor.SetElement("//span", "value");
            _processor.SetElementVisibility("//span", false);

            bool result = _processor.AreAllElementsVisible("//div", "//span");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenAnyElementVisible_WhenCheckingIsAnyElementVisible_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);

            bool result = _processor.IsAnyElementVisible("//div", "//span");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenNoElementsVisible_WhenCheckingIsAnyElementVisible_ThenReturnsFalse()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", false);

            bool result = _processor.IsAnyElementVisible("//div", "//span");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenElementVisible_WhenCheckingIsElementVisible_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);

            bool result = _processor.IsElementVisible("//div");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenElementExistsButNotVisible_WhenCheckingIsElementVisible_ThenReturnsFalse()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", false);

            bool result = _processor.IsElementVisible("//div");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenElementDoesNotExist_WhenCheckingIsElementVisible_ThenReturnsFalse()
        {
            bool result = _processor.IsElementVisible("//nonexistent");

            Assert.That(result, Is.False);
        }
    }
}