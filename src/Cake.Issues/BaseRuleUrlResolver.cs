namespace Cake.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for retrieving a URL linking to a site containing help for a rule.
    /// </summary>
    /// <typeparam name="T">Type of the rule description.</typeparam>
    public abstract class BaseRuleUrlResolver<T>
        where T : BaseRuleDescription, new()
    {
        private readonly List<Tuple<Func<T, Uri>, int>> registeredUrlResolver = [];

        /// <summary>
        /// Registers a new resolver with default priority of <c>0</c>.
        /// </summary>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        public void AddUrlResolver(Func<T, Uri> resolver)
        {
            resolver.NotNull();

            this.AddUrlResolver(resolver, 0);
        }

        /// <summary>
        /// Registers a new resolver with a specific priority.
        /// </summary>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        public void AddUrlResolver(Func<T, Uri> resolver, int priority)
        {
            resolver.NotNull();

            this.registeredUrlResolver.Add(new Tuple<Func<T, Uri>, int>(resolver, priority));
        }

        /// <summary>
        /// Returns a URL linking to a site describing a specific rule.
        /// </summary>
        /// <param name="rule">Code of the rule for which the URL should be retrieved.</param>
        /// <returns>URL linking to a site describing the rule, or <c>null</c> if <paramref name="rule"/>
        /// could not be parsed.</returns>
        public Uri ResolveRuleUrl(string rule)
        {
            rule.NotNullOrWhiteSpace();

            var ruleDescription = new T { Rule = rule };
            if (!this.TryGetRuleDescription(rule, ruleDescription))
            {
                return null;
            }

            return
                this.registeredUrlResolver
                    .OrderByDescending(x => x.Item2)
                    .Select(x => x.Item1(ruleDescription))
                    .FirstOrDefault(x => x != null);
        }

        /// <summary>
        /// Parses a rule into a <see cref="BaseRuleDescription"/>.
        /// </summary>
        /// <param name="rule">Rule which should be parsed.</param>
        /// <param name="ruleDescription">Description of the rule.</param>
        /// <returns><c>true</c> if rule could be parsed successfully, otherwise <c>false</c>.</returns>
        protected abstract bool TryGetRuleDescription(string rule, T ruleDescription);
    }
}