namespace Cake.Issues.Markdownlint
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to rule url resolving.
    /// </content>
    public static partial class MarkdownlintIssuesAliases
    {
        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MarkdownlintRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules with Id smaller than 20 to
        /// search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MarkdownlintAddRuleUrlResolver(x =>
        ///     x.RuleId < 20 ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MarkdownlintAddRuleUrlResolver(
            this ICakeContext context,
            Func<MarkdownlintRuleDescription, Uri> resolver)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            MarkdownlintRuleUrlResolver.Instance.AddUrlResolver(resolver);
        }

        /// <summary>
        /// Registers a new URL resolver with a specific priority.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MarkdownlintRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        /// <example>
        /// <para>Adds a provider of priority 5 returning a link for all rules with Id smaller than 20 to
        /// search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MarkdownlintAddRuleUrlResolver(x =>
        ///     x.RuleId < 20 ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MarkdownlintAddRuleUrlResolver(
            this ICakeContext context,
            Func<MarkdownlintRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            MarkdownlintRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }
    }
}
