namespace Cake.Issues.EsLint
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to rule url resolving.
    /// </content>
    public static partial class EsLintIssuesAliases
    {
        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules starting
        /// with the string <c>Foo</c> to search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// EsLintAddRuleUrlResolver(x =>
        ///     x.Rule.StartsWith("Foo") ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + "%22") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void EsLintAddRuleUrlResolver(
            this ICakeContext context,
            Func<BaseRuleDescription, Uri> resolver)
        {
            context.NotNull();
            resolver.NotNull();

            EsLintRuleUrlResolver.Instance.AddUrlResolver(resolver);
        }

        /// <summary>
        /// Registers a new URL resolver with a specific priority.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        /// <example>
        /// <para>Adds a provider of priority 5 returning a link for all rules starting with the string
        /// <c>Foo</c> to search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// EsLintAddRuleUrlResolver(x =>
        ///     x.Rule.StartsWith("Foo") ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + "%22") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void EsLintAddRuleUrlResolver(
            this ICakeContext context,
            Func<BaseRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull();
            resolver.NotNull();

            EsLintRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }
    }
}
