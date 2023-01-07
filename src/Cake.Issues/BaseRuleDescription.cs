﻿namespace Cake.Issues
{
    /// <summary>
    /// Base class for all rule descriptions.
    /// </summary>
    public abstract class BaseRuleDescription
    {
        /// <summary>
        /// Gets the full identifier of the rule.
        /// </summary>
        public string Rule { get; init; }
    }
}