using System;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorWaitTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenProcessor_WhenWaitingDefault_ThenCallsWaitWithDefaultDuration()
        {
            _processor.Wait();
        }

        [Test]
        public void GivenProcessor_WhenWaitingMilliseconds_ThenCallsWaitWithTimeSpan()
        {
            _processor.Wait(500);
        }

        [Test]
        public void GivenProcessor_WhenWaitingUntilTargetTime_ThenCallsWaitWithTimeSpan()
        {
            _processor.Wait(DateTime.Now.AddMilliseconds(100));
        }

        [Test]
        public void GivenProcessor_WhenWaitingTimeSpan_ThenCallsThreadSleep()
        {
            _processor.Wait(TimeSpan.FromMilliseconds(100));
        }

        [Test]
        public void GivenProcessor_WhenWaitingNegativeTimeSpan_ThenReturnsImmediately()
        {
            _processor.Wait(TimeSpan.FromMilliseconds(-100));
        }

        [Test]
        public void GivenElementWithText_WhenWaitingForTextLength_ThenWaitsUntilLengthMatches()
        {
            _processor.SetElement("//input", "Hello");

            _processor.WaitForTextLength("//input", 5);
        }

        [Test]
        public void GivenElementExists_WhenWaitingForAnyElementToExist_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");

            _processor.WaitForAnyElementToExist("//div", "//span");
        }

        [Test]
        public void GivenAllElementsExist_WhenWaitingForAllElementsToExist_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElement("//span", "value");

            _processor.WaitForAllElementsToExist("//div", "//span");
        }

        [Test]
        public void GivenElementVisible_WhenWaitingForAnyElementToBeVisible_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);

            _processor.WaitForAnyElementToBeVisible("//div", "//span");
        }

        [Test]
        public void GivenAllElementsVisible_WhenWaitingForAllElementsToBeVisible_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);
            _processor.SetElement("//span", "value");
            _processor.SetElementVisibility("//span", true);

            _processor.WaitForAllElementsToBeVisible("//div", "//span");
        }

        [Test]
        public void GivenElementExists_WhenWaitingForElementToExist_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");

            _processor.WaitForElementToExist("//div");
        }

        [Test]
        public void GivenElementDoesNotExist_WhenWaitingForElementToDisappear_ThenReturnsImmediately()
        {
            _processor.WaitForElementToDisappear("//nonexistent");
        }

        [Test]
        public void GivenElementVisible_WhenWaitingForElementToBeVisible_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", true);

            _processor.WaitForElementToBeVisible("//div");
        }

        [Test]
        public void GivenElementNotVisible_WhenWaitingForElementToBeInvisible_ThenReturnsImmediately()
        {
            _processor.SetElement("//div", "value");
            _processor.SetElementVisibility("//div", false);

            _processor.WaitForElementToBeInvisible("//div");
        }
    }
}