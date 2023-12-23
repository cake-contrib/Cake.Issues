namespace Cake.Issues.EsLint
{
    using System;

    /// <summary>
    /// Class for retrieving an URL linking to a site describing a rule.
    /// </summary>
    internal class EsLintRuleUrlResolver : BaseRuleUrlResolver<BaseRuleDescription>
    {
        private static readonly Lazy<EsLintRuleUrlResolver> InstanceValue =
            new(() => new EsLintRuleUrlResolver());

        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintRuleUrlResolver"/> class.
        /// </summary>
        private EsLintRuleUrlResolver()
        {
            this.AddUrlResolver(x =>
                new Uri("http://eslint.org/docs/rules/" + x.Rule));
        }

        /// <summary>
        /// Gets the instance of the rule resolver.
        /// </summary>
        public static EsLintRuleUrlResolver Instance => InstanceValue.Value;

        /// <inheritdoc/>
        protected override bool TryGetRuleDescription(string rule, BaseRuleDescription ruleDescription)
        {
            return true;
        }
    }
}