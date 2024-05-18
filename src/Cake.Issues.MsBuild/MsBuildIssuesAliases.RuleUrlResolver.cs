namespace Cake.Issues.MsBuild
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to rule url resolving.
    /// </content>
    public static partial class MsBuildIssuesAliases
    {
        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MsBuildRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules of the category <c>CA</c> to
        /// search <c>msdn.microsoft.com</c> with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MsBuildAddRuleUrlResolver(x =>
        ///     x.Category.ToUpperInvariant() == "CA" ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+site:msdn.microsoft.com") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MsBuildAddRuleUrlResolver(
            this ICakeContext context,
            Func<MsBuildRuleDescription, Uri> resolver)
        {
            context.NotNull();
            resolver.NotNull();

            MsBuildRuleUrlResolver.Instance.AddUrlResolver(resolver);
        }

        /// <summary>
        /// Registers a new URL resolver with a specific priority.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MsBuildRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        /// <example>
        /// <para>Adds a provider of priority 5 returning a link for all rules of the category <c>CA</c> to
        /// search <c>msdn.microsoft.com</c> with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MsBuildAddRuleUrlResolver(x =>
        ///     x.Category.ToUpperInvariant() == "CA" ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+site:msdn.microsoft.com") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MsBuildAddRuleUrlResolver(
            this ICakeContext context,
            Func<MsBuildRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull();
            resolver.NotNull();

            MsBuildRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }
    }
}
