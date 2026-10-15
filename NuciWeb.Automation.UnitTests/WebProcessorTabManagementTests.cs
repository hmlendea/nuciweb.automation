using System;
using System.Collections.Generic;
using NUnit.Framework;
using NuciWeb.Automation;

namespace NuciWeb.Automation.UnitTests
{
    [TestFixture]
    public class WebProcessorTabManagementTests
    {
        private TestWebProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new TestWebProcessor();
        }

        [Test]
        public void GivenNewProcessor_WhenGettingTabs_ThenReturnsInitialTab()
        {
            Assert.That(_processor.Tabs, Has.Count.EqualTo(1));
            Assert.That(_processor.Tabs[0], Is.EqualTo("tab1"));
        }

        [Test]
        public void GivenNewProcessor_WhenGettingCurrentTab_ThenReturnsInitialTab()
        {
            Assert.That(_processor.CurrentTab, Is.EqualTo("tab1"));
        }

        [Test]
        public void GivenProcessor_WhenCreatingNewTab_ThenAddsTabAndSwitchesToIt()
        {
            string newTab = _processor.NewTab();

            Assert.That(_processor.Tabs, Has.Count.EqualTo(2));
            Assert.That(_processor.Tabs, Contains.Item(newTab));
            Assert.That(_processor.CurrentTab, Is.EqualTo(newTab));
        }

        [Test]
        public void GivenProcessor_WhenCreatingNewTabWithUrl_ThenAddsTabAndSwitchesToIt()
        {
            string newTab = _processor.NewTab("https://example.com");

            Assert.That(_processor.Tabs, Has.Count.EqualTo(2));
            Assert.That(_processor.Tabs, Contains.Item(newTab));
            Assert.That(_processor.CurrentTab, Is.EqualTo(newTab));
        }

        [Test]
        public void GivenProcessorWithMultipleTabs_WhenSwitchingToExistingTab_ThenUpdatesCurrentTab()
        {
            string tab2 = _processor.NewTab();
            string tab3 = _processor.NewTab();

            _processor.SwitchToTab(tab2);

            Assert.That(_processor.CurrentTab, Is.EqualTo(tab2));
        }

        [Test]
        public void GivenProcessor_WhenSwitchingToSameTab_ThenDoesNothing()
        {
            string currentTab = _processor.CurrentTab;

            _processor.SwitchToTab(currentTab);

            Assert.That(_processor.CurrentTab, Is.EqualTo(currentTab));
        }

        [Test]
        public void GivenProcessor_WhenSwitchingToNonExistentTab_ThenThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _processor.SwitchToTab("nonexistent"));
        }

        [Test]
        public void GivenProcessorWithMultipleTabs_WhenClosingCurrentTab_ThenRemovesTab()
        {
            string tab2 = _processor.NewTab();

            _processor.CloseTab();

            Assert.That(_processor.Tabs, Has.Count.EqualTo(1));
            Assert.That(_processor.Tabs, Does.Not.Contain(tab2));
        }

        [Test]
        public void GivenProcessorWithMultipleTabs_WhenClosingSpecificTab_ThenRemovesTab()
        {
            string tab2 = _processor.NewTab();
            string tab3 = _processor.NewTab();

            _processor.CloseTab(tab2);

            Assert.That(_processor.Tabs, Has.Count.EqualTo(2));
            Assert.That(_processor.Tabs, Does.Not.Contain(tab2));
        }

        [Test]
        public void GivenProcessor_WhenClosingNonExistentTab_ThenThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _processor.CloseTab("nonexistent"));
        }

        [Test]
        public void GivenProcessor_WhenSwitchingByValidIndex_ThenUpdatesCurrentTab()
        {
            _processor.NewTab();
            _processor.NewTab();

            _processor.SwitchToTab(1);

            Assert.That(_processor.CurrentTab, Is.EqualTo(_processor.Tabs[1]));
        }

        [Test]
        public void GivenProcessor_WhenSwitchingByInvalidIndex_ThenThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _processor.SwitchToTab(10));
        }
    }
}