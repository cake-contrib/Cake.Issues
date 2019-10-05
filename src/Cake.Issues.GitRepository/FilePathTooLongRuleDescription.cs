namespace Cake.Issues.GitRepository
{
    /// <summary>
    /// Description of the rule which checks if the path of a file in the repository is too long.
    /// </summary>
    public class FilePathTooLongRuleDescription : BaseGitRepositoryIssuesRuleDescription
    {
        /// <inheritdoc />
        public override string RuleName => "FilePathTooLong";

        /// <inheritdoc />
        public override IssuePriority Priority => IssuePriority.Warning;
    }
}
