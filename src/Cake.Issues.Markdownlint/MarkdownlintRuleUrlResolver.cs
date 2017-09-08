namespace Cake.Issues.Markdownlint
{
    using System;

    /// <summary>
    /// Class for retrieving an URL linking to a site describing a rule.
    /// </summary>
    internal class MarkdownlintRuleUrlResolver : BaseRuleUrlResolver<BaseRuleDescription>
    {
        private static readonly Lazy<MarkdownlintRuleUrlResolver> InstanceValue =
            new Lazy<MarkdownlintRuleUrlResolver>(() => new MarkdownlintRuleUrlResolver());

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintRuleUrlResolver"/> class.
        /// </summary>
        private MarkdownlintRuleUrlResolver()
        {
            this.AddUrlResolver(x =>
                new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#" + x.Rule.ToLowerInvariant()));
        }

        /// <summary>
        /// Gets the instance of the rule resolver.
        /// </summary>
        public static MarkdownlintRuleUrlResolver Instance => InstanceValue.Value;

        /// <inheritdoc/>
        protected override bool TryGetRuleDescription(string rule, BaseRuleDescription ruleDescription)
        {
            return true;
        }
    }
}