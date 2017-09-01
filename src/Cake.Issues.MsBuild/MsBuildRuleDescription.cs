namespace Cake.Issues.MsBuild
{
    using IssueProvider;

    /// <summary>
    /// Class describing rules appearing in MsBuild logs.
    /// </summary>
    public class MsBuildRuleDescription : BaseRuleDescription
    {
        /// <summary>
        /// Gets or sets the category of the rule.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the rule.
        /// </summary>
        public int RuleId { get; set; }
    }
}