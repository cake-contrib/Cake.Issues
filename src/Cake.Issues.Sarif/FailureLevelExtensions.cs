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
        public static IssuePriority ToPriority(this FailureLevel level)
        {
            switch (level)
            {
                case FailureLevel.None:
                    return IssuePriority.Undefined;
                case FailureLevel.Note:
                    return IssuePriority.Suggestion;
                case FailureLevel.Warning:
                    return IssuePriority.Warning;
                case FailureLevel.Error:
                    return IssuePriority.Error;
                default:
                    return IssuePriority.Undefined;
            }
        }
    }
}
