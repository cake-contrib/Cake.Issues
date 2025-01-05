using Cake.Core;

/// <summary>
/// Class holding state during execution of build.
/// </summary>
public class BuildContext : IssuesContext<BuildParameters, BuildState>
{
    public Paths Paths { get; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        Paths = new Paths(this);
    }

    protected override BuildParameters CreateIssuesParameters()
    {
        return new BuildParameters(this);
    }

    protected override BuildState CreateIssuesState()
    {
        return new BuildState(this);
    }
}
