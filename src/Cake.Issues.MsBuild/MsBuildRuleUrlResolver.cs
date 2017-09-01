namespace Cake.Issues.MsBuild
{
    using System;
    using System.Text;
    using IssueProvider;

    /// <summary>
    /// Class for retrieving an URL linking to a site describing a rule.
    /// </summary>
    internal class MsBuildRuleUrlResolver : BaseRuleUrlResolver<MsBuildRuleDescription>
    {
        private static readonly Lazy<MsBuildRuleUrlResolver> InstanceValue =
            new Lazy<MsBuildRuleUrlResolver>(() => new MsBuildRuleUrlResolver());

        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildRuleUrlResolver"/> class.
        /// </summary>
        private MsBuildRuleUrlResolver()
        {
            // Add resolver for common known issue categories.
            this.AddUrlResolver(x =>
                x.Category.ToUpperInvariant() == "CA" ?
                    new Uri("https://www.google.im/search?q=%22" + x.Rule + ":%22+site:msdn.microsoft.com") :
                    null);
            this.AddUrlResolver(x =>
                x.Category.ToUpperInvariant() == "SA" ?
                    new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/" + x.Rule + ".md") :
                    null);
        }

        /// <summary>
        /// Gets the instance of the rule resolver.
        /// </summary>
        public static MsBuildRuleUrlResolver Instance => InstanceValue.Value;

        /// <inheritdoc/>
        protected override bool TryGetRuleDescription(string rule, MsBuildRuleDescription ruleDescription)
        {
            // Parse the rule. Expect it in the form starting with a identifier containing characters
            // followed by the rule id as a number.
            var digitIndex = -1;
            var categoryBuilder = new StringBuilder();
            for (var index = 0; index < rule.Length; index++)
            {
                var currentChar = rule[index];
                if (char.IsDigit(currentChar))
                {
                    digitIndex = index;
                    break;
                }

                categoryBuilder.Append(currentChar);
            }

            // If rule doesn't contain numbers return false.
            if (digitIndex < 0)
            {
                return false;
            }

            // Try to parse the part after the first number to an integer.
            int ruleId;
            if (!int.TryParse(rule.Substring(digitIndex), out ruleId))
            {
                return false;
            }

            ruleDescription.RuleId = ruleId;
            ruleDescription.Category = categoryBuilder.ToString();

            return true;
        }
    }
}
