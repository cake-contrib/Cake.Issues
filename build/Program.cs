using Cake.Frosting;
using System.Reflection;

public static class Program
{
    public static int Main(string[] args)
    {
        // renovate: depName=GitVersion.Tool
        const string nuGetVersion = "6.1.0";

        return new CakeHost()
            .UseContext<BuildContext>()
            .UseLifetime<BuildLifetime>()
            .AddAssembly(Assembly.GetAssembly(typeof(IssuesTask)))
            .InstallTool(new Uri($"dotnet:?package=GitVersion.Tool&version={nuGetVersion}"))
            .Run(args);
    }
}
