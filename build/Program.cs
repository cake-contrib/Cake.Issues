using System.Reflection;
using Cake.Frosting;
using Cake.Frosting.Issues.Recipe;

return new CakeHost()
    .UseContext<BuildContext>()
    .UseLifetime<BuildLifetime>()
    .AddAssembly(Assembly.GetAssembly(typeof(IssuesTask)))
    .InstallTool(new Uri("dotnet:?package=GitVersion.Tool&version=6.8.2"))
    .InstallTool(new Uri("nuget:?package=CodecovUploader&version=0.8.0"))
    .InstallTool(new Uri("nuget:?package=NuGet.CommandLine&version=7.9.0"))
    .Run(args);
