using System;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorElementStateTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenElementHasClass_WhenCheckingHasClass_ThenReturnsTrue()
        {
            _processor.SetElement("//div", "class1 class2");

            bool result = _processor.HasClass("//div", "class1");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenElementDoesNotHaveClass_WhenCheckingHasClass_ThenReturnsFalse()
        {
            _processor.SetElement("//div", "class1 class2");

            bool result = _processor.HasClass("//div", "class3");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenElementIsSelected_WhenCheckingIsSelected_ThenReturnsTrue()
        {
            _processor.SetElementSelection("//option", true);

            bool result = _processor.IsSelected("//option");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenElementIsNotSelected_WhenCheckingIsSelected_ThenReturnsFalse()
        {
            _processor.SetElementSelection("//option", false);

            bool result = _processor.IsSelected("//option");

            Assert.That(result, Is.False);
        }
    }
}