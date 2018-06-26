namespace Cake.Issues.Reporting.Generic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using RazorEngine.Compilation;
    using RazorEngine.Compilation.ReferenceResolver;

    /// <summary>
    /// Custom resolver for RazorEngine.
    /// Required since we're embedding references with Costura.Fody.
    /// </summary>
    internal class RazorEngineReferenceResolver : IReferenceResolver
    {
        /// <inheritdoc />
        public IEnumerable<CompilerReference> GetReferences(
            TypeContext context,
            IEnumerable<CompilerReference> includeAssemblies)
        {
            IEnumerable<string> loadedAssemblies =
                new UseCurrentAssembliesReferenceResolver()
                .GetReferences(context, includeAssemblies)
                .Select(r => r.GetFile())
                .ToArray();

            // All assemblies which can be used inside the templates need to be listed here.
            yield return CompilerReference.From(this.FindLoaded(loadedAssemblies, "System.dll"));
            yield return CompilerReference.From(this.FindLoaded(loadedAssemblies, "System.Core.dll"));
            yield return CompilerReference.From(this.FindLoaded(loadedAssemblies, "netstandard.dll"));
            yield return CompilerReference.From(this.FindLoaded(loadedAssemblies, "Newtonsoft.Json.dll"));
            yield return CompilerReference.From(this.FindLoaded(loadedAssemblies, "Cake.Core.dll"));
            yield return CompilerReference.From(this.FindLoaded(loadedAssemblies, "Cake.Issues.dll"));
            yield return CompilerReference.From(typeof(RazorEngine.Engine).Assembly);
        }

        private string FindLoaded(IEnumerable<string> refs, string find)
        {
            return refs.First(r => r.EndsWith(Path.DirectorySeparatorChar + find));
        }
    }
}
