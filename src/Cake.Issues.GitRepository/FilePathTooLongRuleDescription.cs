namespace Cake.Issues.GitRepository
{
    /// <summary>
    /// Description of the rule which checks if the path of a file in the repository is too long.
    /// </summary>
    public class FilePathTooLongRuleDescription : BaseGitRepositoryIssuesRuleDescription
    {
        /// <inheritdoc />
        public override string RuleId => "FilePathTooLong";

        /// <inheritdoc />
        public override string RuleName => "File path too long";

        /// <inheritdoc />
        public override IssuePriority Priority => IssuePriority.Warning;
    }
}
