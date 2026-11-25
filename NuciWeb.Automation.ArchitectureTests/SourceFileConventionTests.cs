using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using NUnit.Framework;

namespace NuciWeb.Automation.ArchitectureTests
{
    [TestFixture]
    public sealed class SourceFileConventionTests
    {
        [Test]
        public void GivenProductionSources_WhenChecked_ThenEachFileContainsOneMatchingType()
        {
            string[] sourceNames = GetSourceNames();
            List<string> violations = [];

            foreach (string sourceName in sourceNames)
            {
                string fileName = sourceName["ProductionSources/".Length..];
                string stem = Path.GetFileNameWithoutExtension(fileName);
                string source = ReadSource(sourceName);
                TypeDeclarationSyntax[] declarations = CSharpSyntaxTree.ParseText(source)
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<TypeDeclarationSyntax>()
                    .Where(declaration => declaration.Parent is not TypeDeclarationSyntax)
                    .ToArray();

                if (declarations.Length != 1 || !string.Equals(declarations[0].Identifier.ValueText, stem, StringComparison.Ordinal))
                {
                    violations.Add($"{fileName}: expected one top-level type named {stem}.");
                }
            }

            Assert.That(violations, Is.Empty, string.Join(Environment.NewLine, violations));
        }

        [Test]
        public void GivenProductionSources_WhenChecked_ThenTheyUseBlockNamespaces()
        {
            List<string> violations = [];

            foreach (string sourceName in GetSourceNames())
            {
                string source = ReadSource(sourceName);
                if (CSharpSyntaxTree.ParseText(source).GetRoot().DescendantNodes().OfType<FileScopedNamespaceDeclarationSyntax>().Any())
                {
                    violations.Add(sourceName);
                }
            }

            Assert.That(violations, Is.Empty, string.Join(Environment.NewLine, violations));
        }

        [Test]
        public void GivenProductionSources_WhenChecked_ThenTheyDoNotUsePreviewDiagnosticFeatures()
        {
            List<string> violations = [];

            foreach (string sourceName in GetSourceNames())
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(ReadSource(sourceName), new CSharpParseOptions(LanguageVersion.Latest));
                foreach (Diagnostic diagnostic in tree.GetDiagnostics())
                {
                    if (diagnostic.Id is "CS8652" or "CS9202")
                    {
                        violations.Add($"{sourceName}: {diagnostic.GetMessage()}");
                    }
                }
            }

            Assert.That(violations, Is.Empty, string.Join(Environment.NewLine, violations));
        }

        [Test]
        public void GivenProductionSources_WhenChecked_ThenTheyContainNoRegionsOrNullableDirectives()
        {
            List<string> violations = [];

            foreach (string sourceName in GetSourceNames())
            {
                string source = ReadSource(sourceName);
                if (source.Contains("#region", StringComparison.Ordinal) || source.Contains("#nullable enable", StringComparison.Ordinal))
                {
                    violations.Add(sourceName);
                }
            }

            Assert.That(violations, Is.Empty, string.Join(Environment.NewLine, violations));
        }

        private static string[] GetSourceNames()
        {
            Assembly assembly = typeof(SourceFileConventionTests).Assembly;
            string[] sourceNames = assembly.GetManifestResourceNames()
                .Where(name => name.StartsWith("ProductionSources/", StringComparison.Ordinal))
                .OrderBy(name => name, StringComparer.Ordinal)
                .ToArray();

            Assert.That(sourceNames, Is.Not.Empty, "No production source files were embedded.");
            return sourceNames;
        }

        private static string ReadSource(string sourceName)
        {
            using Stream stream = typeof(SourceFileConventionTests).Assembly.GetManifestResourceStream(sourceName)!;
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
