namespace Cake.Issues.Reporting.Console.Tests;

using System;
using System.IO;
using System.Reflection;

internal class TemporarySourceFile : IDisposable
{
    public string FilePath { get; }

    public TemporarySourceFile(string resourcePartialName, Assembly assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var fullResourceName = FindFullResourceName(assembly, resourcePartialName)
            ?? throw new ArgumentException($"Resource '{resourcePartialName}' not found in assembly '{assembly.FullName}'.");

        using var resourceStream = assembly.GetManifestResourceStream(fullResourceName)
            ?? throw new InvalidOperationException($"Could not load resource stream for '{fullResourceName}'.");

        this.FilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        using var fileStream = File.Create(this.FilePath);
        resourceStream.CopyTo(fileStream);
    }

    private static string FindFullResourceName(Assembly assembly, string partialName) =>
        Array.Find(
            assembly.GetManifestResourceNames(),
            name => name.EndsWith(partialName, StringComparison.OrdinalIgnoreCase));

    public void Dispose()
    {
        if (File.Exists(this.FilePath))
        {
            try
            {
                File.Delete(this.FilePath);
            }
            catch
            {
                // Swallow exceptions to avoid breaking test teardown
            }
        }
    }
}
