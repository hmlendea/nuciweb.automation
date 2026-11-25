using System;

using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using NuciWeb.Automation;

namespace NuciWeb.Automation.ArchitectureTests
{
    internal static class ProductionArchitecture
    {
        public static readonly System.Reflection.Assembly ProductionAssembly = typeof(IWebProcessor).Assembly;

        public static readonly Architecture Architecture = new ArchLoader()
            .LoadAssemblies(ProductionAssembly)
            .Build();
    }
}
