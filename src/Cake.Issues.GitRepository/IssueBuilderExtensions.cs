namespace Cake.Issues.GitRepository
{
    using System;

    /// <summary>
    /// Extensions for <see cref="IssueBuilder"/>.
    /// </summary>
    internal static class IssueBuilderExtensions
    {
        /// <summary>
        /// Sets the rule and priority of the issue.
        /// </summary>
        /// <param name="issueBuilder">Issue builder on which the properties should be set.</param>
        /// <param name="ruleDescription">Rule metadata.</param>
        /// <returns>Issue Builder instance.</returns>
        public static IssueBuilder OfRule(this IssueBuilder issueBuilder, BaseGitRepositoryIssuesRuleDescription ruleDescription)
        {
            issueBuilder.NotNull();
            ruleDescription.NotNull();

            return
                issueBuilder
                    .OfRule(
                        ruleDescription.RuleId,
                        ruleDescription.RuleName,
                        new Uri($"https://cakeissues.net/docs/issue-providers/gitrepository/rules/{ruleDescription.RuleId}"))
                    .WithPriority(ruleDescription.Priority);
        }
    }
}
