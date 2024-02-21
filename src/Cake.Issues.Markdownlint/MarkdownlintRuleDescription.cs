namespace Cake.Issues.Markdownlint
{
    /// <summary>
    /// Class describing rules appearing in Markdownlint logs.
    /// </summary>
    public class MarkdownlintRuleDescription : BaseRuleDescription
    {
        /// <summary>
        /// Gets or sets the identifier of the rule.
        /// </summary>
        public int RuleId { get; set; }
    }
}