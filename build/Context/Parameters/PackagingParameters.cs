using Cake.Common;
using Cake.Core;

public class PackagingParameters(ICakeContext context)
{
    public string NuGetApiKey { get; } = context.EnvironmentVariable("NUGET_API_KEY");
}