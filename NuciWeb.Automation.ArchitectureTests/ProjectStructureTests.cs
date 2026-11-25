using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using NUnit.Framework;

namespace NuciWeb.Automation.ArchitectureTests
{
    [TestFixture]
    public sealed class ProjectStructureTests
    {
        [Test]
        public void GivenProductionProject_WhenChecked_ThenRequiredPropertiesAreConfigured()
        {
            XDocument project = LoadProject("NuciWeb.Automation.csproj");
            Assert.That(GetValue(project, "TargetFramework"), Is.EqualTo("net10.0"));
            Assert.That(GetValue(project, "ImplicitUsings"), Is.EqualTo("disable"));
            Assert.That(GetValue(project, "LangVersion"), Is.EqualTo("latest"));
            Assert.That(GetValue(project, "Nullable"), Is.EqualTo("enable"));
            Assert.That(GetValue(project, "IsPackable"), Is.EqualTo("true"));
        }

        [Test]
        public void GivenUnitTestProject_WhenChecked_ThenRequiredPropertiesAreConfigured()
        {
            XDocument project = LoadProject("NuciWeb.Automation.UnitTests.csproj");
            Assert.That(GetValue(project, "TargetFramework"), Is.EqualTo("net10.0"));
            Assert.That(GetValue(project, "ImplicitUsings"), Is.EqualTo("disable"));
            Assert.That(GetValue(project, "LangVersion"), Is.EqualTo("latest"));
            Assert.That(GetValue(project, "Nullable"), Is.EqualTo("enable"));
            Assert.That(GetValue(project, "IsPackable"), Is.EqualTo("false"));
            Assert.That(GetValue(project, "IsTestProject"), Is.EqualTo("true"));
            Assert.That(GetValue(project, "ExcludeFromCodeCoverage"), Is.EqualTo("true"));
        }

        [Test]
        public void GivenProductionProject_WhenChecked_ThenPackageReferencesAreAlphabetical()
        {
            XDocument project = LoadProject("NuciWeb.Automation.csproj");
            string[] packages = project.Descendants("PackageReference")
                .Select(element => (string)element.Attribute("Include")!)
                .ToArray();
            Assert.That(packages, Is.EqualTo(packages.OrderBy(value => value, StringComparer.Ordinal).ToArray()));
        }

        private static XDocument LoadProject(string projectFile)
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", projectFile.Contains("UnitTests", StringComparison.Ordinal) ? "NuciWeb.Automation.UnitTests" : "NuciWeb.Automation", projectFile));
            Assert.That(File.Exists(path), Is.True, $"Required project file is unavailable: {projectFile}");
            return XDocument.Load(path);
        }

        private static string? GetValue(XDocument project, string name)
            => project.Descendants(name).Select(element => (string)element).FirstOrDefault();
    }
}
