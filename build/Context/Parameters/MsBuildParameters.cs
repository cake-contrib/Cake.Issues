using Cake.Common.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Core;

public class MsBuildParameters(ICakeContext context)
{
    public string Configuration { get; } = "Release"; // Configuration is also hardcoded in nuspec files

    public DotNetMSBuildSettings GetSettings()
    {
        return new DotNetMSBuildSettings()
            .SetConfiguration(this.Configuration) 
            .WithProperty("ContinuousIntegrationBuild", (!context.BuildSystem().IsLocalBuild).ToString());
    }
}