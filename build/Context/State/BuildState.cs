using Cake.Common.Tools.GitVersion;

public class BuildState : IssuesState
{
    public GitVersion Version { get; set; }

    public BuildState(BuildContext context)
        : base(context, RepositoryInfoProviderType.CakeGit)
    {
    }
}
