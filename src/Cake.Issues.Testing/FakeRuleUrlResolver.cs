namespace Cake.Issues.Testing
{
    using IssueProvider;

    /// <summary>
    /// Implementation of a <see cref="BaseRuleUrlResolver{T}"/> for use in test cases.
    /// </summary>
    public class FakeRuleUrlResolver : BaseRuleUrlResolver<FakeRuleDescription>
    {
        /// <inheritdoc/>
        protected override bool TryGetRuleDescription(string rule, FakeRuleDescription ruleDescription)
        {
            ruleDescription = new FakeRuleDescription { Rule = rule };
            return true;
        }
    }
}
