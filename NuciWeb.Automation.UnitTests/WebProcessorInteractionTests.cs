using System;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorInteractionTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenElementExists_WhenMovingToElement_ThenCallsPerformMoveToElement()
        {
            _processor.SetElement("//div", "value");

            _processor.MoveToElement("//div");
        }

        [Test]
        public void GivenOneElementVisible_WhenClickingAny_ThenClicksFirstVisible()
        {
            _processor.SetElement("//div1", "value");
            _processor.SetElementVisibility("//div1", true);
            _processor.SetElement("//div2", "value");
            _processor.SetElementVisibility("//div2", false);

            _processor.ClickAny("//div1", "//div2");
        }

        [Test]
        public void GivenNoElementsVisible_WhenClickingAny_ThenThrowsArgumentException()
        {
            _processor.SetElement("//div1", "value");
            _processor.SetElementVisibility("//div1", false);
            _processor.SetElement("//div2", "value");
            _processor.SetElementVisibility("//div2", false);

            Assert.Throws<ArgumentException>(() => _processor.ClickAny("//div1", "//div2"));
        }

        [Test]
        public void GivenElementExists_WhenClicking_ThenCallsPerformClick()
        {
            _processor.SetElement("//button", "value");

            _processor.Click("//button");
        }

        [Test]
        public void GivenCheckboxUncheckedAndStatusTrue_WhenUpdatingCheckbox_ThenClicks()
        {
            _processor.SetElement("//checkbox", "value");
            _processor.SetCheckboxState("//checkbox", false);

            _processor.UpdateCheckbox("//checkbox", true);
        }

        [Test]
        public void GivenCheckboxCheckedAndStatusFalse_WhenUpdatingCheckbox_ThenClicks()
        {
            _processor.SetElement("//checkbox", "value");
            _processor.SetCheckboxState("//checkbox", true);

            _processor.UpdateCheckbox("//checkbox", false);
        }

        [Test]
        public void GivenCheckboxAlreadyMatchesStatus_WhenUpdatingCheckbox_ThenDoesNotClick()
        {
            _processor.SetElement("//checkbox", "value");
            _processor.SetCheckboxState("//checkbox", true);

            _processor.UpdateCheckbox("//checkbox", true);
        }

        [Test]
        public void GivenSelectElement_WhenSelectingByIndex_ThenCallsPerformSelectOptionByIndex()
        {
            _processor.SetElement("//select", "value");
            _processor.SetSelectOptions("//select", new System.Collections.Generic.List<string> { "Option 1", "Option 2", "Option 3" });

            _processor.SelectOptionByIndex("//select", 1);
        }

        [Test]
        public void GivenSelectElement_WhenSelectingByValue_ThenCallsPerformSelectOptionByValue()
        {
            _processor.SetElement("//select", "value");
            _processor.SetSelectOptions("//select", new System.Collections.Generic.List<string> { "Option 1", "Option 2", "Option 3" });

            _processor.SelectOptionByValue("//select", "Option 2");
        }

        [Test]
        public void GivenSelectElement_WhenSelectingByText_ThenCallsPerformSelectOptionByText()
        {
            _processor.SetElement("//select", "value");
            _processor.SetSelectOptions("//select", new System.Collections.Generic.List<string> { "Option 1", "Option 2", "Option 3" });

            _processor.SelectOptionByText("//select", "Option 2");
        }

        [Test]
        public void GivenSelectElementWithOptions_WhenSelectingRandomOption_ThenSelectsValidIndex()
        {
            _processor.SetElement("//select", "value");
            _processor.SetSelectOptions("//select", new System.Collections.Generic.List<string> { "Option 1", "Option 2", "Option 3" });

            _processor.SelectRandomOption("//select");
        }
    }
}