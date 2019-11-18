namespace Cake.Issues.Markdownlint
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class for retrieving an URL linking to a site describing a rule.
    /// </summary>
    internal class MarkdownlintRuleUrlResolver : BaseRuleUrlResolver<MarkdownlintRuleDescription>
    {
        private static readonly Lazy<MarkdownlintRuleUrlResolver> InstanceValue =
            new Lazy<MarkdownlintRuleUrlResolver>(() => new MarkdownlintRuleUrlResolver());

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintRuleUrlResolver"/> class.
        /// </summary>
        internal MarkdownlintRuleUrlResolver()
        {
            this.AddUrlResolver(x =>
                new Uri("https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#" + x.Rule.ToLowerInvariant()));
        }

        /// <summary>
        /// Gets the instance of the rule resolver.
        /// </summary>
        public static MarkdownlintRuleUrlResolver Instance => InstanceValue.Value;

        /// <inheritdoc/>
        protected override bool TryGetRuleDescription(string rule, MarkdownlintRuleDescription ruleDescription)
        {
            var regex = new Regex(@"^MD(\d*)$");
            var match = regex.Match(rule);

            if (!match.Success)
            {
                return false;
            }

            var groups = match.Groups;

            if (groups.Count != 2)
            {
                return false;
            }

            if (!int.TryParse(groups[1].Value, out int ruleId))
            {
                return false;
            }

            ruleDescription.RuleId = ruleId;

            return true;
        }
    }
}