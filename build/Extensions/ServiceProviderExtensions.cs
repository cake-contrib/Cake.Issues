using Cake.Core.Packaging;
using Cake.Frosting;

internal static class ServiceProviderExtensions
{
    public static void InstallTool(this IServiceProvider provider, string packageName, string packageVersion)
    {
        var toolInstaller = (IToolInstaller)provider.GetService(typeof(IToolInstaller));
        toolInstaller.Install(new PackageReference($"nuget:?package={packageName}&version={packageVersion}"));
    }
}