using Cake.Core;

public class BuildParameters : IssuesParameters
{
    public CodeCoverageParameters CodeCoverage { get; }

    public MsBuildParameters Build { get; }

    public PackagingParameters Packaging { get; }

    public BuildParameters(ICakeContext context)
    {
        this.OutputDirectory = "../BuildArtifacts";
        this.CodeCoverage = new CodeCoverageParameters(context);
        this.Build = new MsBuildParameters(context);
        this.Packaging = new PackagingParameters(context);
    }
}
