namespace Cake.Issues.GitRepository
{
    /// <summary>
    /// Description of the rule which checks if a binary file in the repository is tracked by Git Large File System.
    /// </summary>
    public class BinaryFileNotTrackedByLfsRuleDescription : BaseGitRepositoryIssuesRuleDescription
    {
        /// <inheritdoc />
        public override string RuleName => "BinaryFileNotTrackedByLfs";

        /// <inheritdoc />
        public override IssuePriority Priority => IssuePriority.Warning;
    }
}
