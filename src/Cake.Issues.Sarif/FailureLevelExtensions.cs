namespace Cake.Issues.Sarif
{
    using Microsoft.CodeAnalysis.Sarif;

    /// <summary>
    /// Extension methods for the <see cref="FailureLevel"/> enumeration.
    /// </summary>
    internal static class FailureLevelExtensions
    {
        /// <summary>
        /// Returns the priority of the issue.
        /// </summary>
        /// <param name="level">Level of the sarif result.</param>
        /// <returns>Priority of the issue.</returns>
        public static IssuePriority ToPriority(this FailureLevel level) =>
            level switch
            {
                FailureLevel.None => IssuePriority.Undefined,
                FailureLevel.Note => IssuePriority.Suggestion,
                FailureLevel.Warning => IssuePriority.Warning,
                FailureLevel.Error => IssuePriority.Error,
                _ => IssuePriority.Undefined,
            };
    }
}