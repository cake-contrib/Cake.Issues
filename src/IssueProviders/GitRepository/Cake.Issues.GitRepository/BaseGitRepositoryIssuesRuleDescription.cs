namespace Cake.Issues.GitRepository;

/// <summary>
/// Base class for descriptions of rules checked by <see cref="GitRepositoryIssuesProvider"/>.
/// </summary>
public abstract class BaseGitRepositoryIssuesRuleDescription
{
    /// <summary>
    /// Gets the ID of the rule.
    /// </summary>
    public abstract string RuleId { get; }

    /// <summary>
    /// Gets the name of the rule.
    /// </summary>
    public abstract string RuleName { get; }

    /// <summary>
    /// Gets the priority of the rule.
    /// </summary>
    public abstract IssuePriority Priority { get; }
}
