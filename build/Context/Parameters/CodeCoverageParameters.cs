using Cake.Common;
using Cake.Core;

public class CodeCoverageParameters(ICakeContext context)
{
    public string CodecovRepoToken { get; } = context.EnvironmentVariable("CODECOV_REPO_TOKEN");
}