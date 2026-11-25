using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using ArchUnitNET.Domain;
using ArchUnitNET.NUnit;
using NuciWeb.Automation;
using NUnit.Framework;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace NuciWeb.Automation.ArchitectureTests
{
    [TestFixture]
    public sealed class DependencyArchitectureTests
    {
        [Test]
        public void GivenProductionAssembly_WhenLoaded_ThenExpectedTypesArePresent()
        {
            Assert.That(ProductionArchitecture.ProductionAssembly.GetTypes(), Is.Not.Empty);
            Assert.That(ProductionArchitecture.ProductionAssembly.GetTypes().Any(type => type.Namespace?.Contains("UnitTests", StringComparison.Ordinal) == true), Is.False);
        }

        [Test]
        public void GivenProductionTypes_WhenChecked_ThenTheyDoNotDependOnTestTypes()
        {
            Classes().That().ResideInAssembly(ProductionArchitecture.ProductionAssembly)
                .Should().NotDependOnAny(Types().That().ResideInNamespace("NuciWeb.Automation.UnitTests"))
                .Check(ProductionArchitecture.Architecture);
        }

        [Test]
        public void GivenWebProcessor_WhenChecked_ThenItImplementsThePublicContract()
        {
            Classes().That().Are(typeof(WebProcessor)).Should().ImplementInterface(typeof(IWebProcessor))
                .Check(ProductionArchitecture.Architecture);
        }

        [Test]
        public void GivenProductionTypes_WhenChecked_ThenNoTypeUsesInternalsVisibleTo()
        {
            System.Reflection.Assembly productionAssembly = ProductionArchitecture.ProductionAssembly;
            Assert.That(productionAssembly.GetCustomAttributes<InternalsVisibleToAttribute>(), Is.Empty);
        }

        [Test]
        public void GivenProductionTypes_WhenChecked_ThenNoTypeHasAnExplicitSelfDependency()
        {
            foreach (Type type in ProductionArchitecture.ProductionAssembly.GetTypes())
            {
                Assert.That(type.BaseType, Is.Not.EqualTo(type), $"{type.FullName} has itself as a base type.");
                Assert.That(type.GetInterfaces(), Does.Not.Contain(type), $"{type.FullName} implements itself.");
            }
        }
    }
}
