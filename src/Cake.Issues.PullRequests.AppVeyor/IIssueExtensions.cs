namespace Cake.Issues.PullRequests.AppVeyor
{
    using Cake.Common.Build.AppVeyor;

    /// <summary>
    /// Extensions for <see cref="IIssue"/>.
    /// </summary>
    internal static class IIssueExtensions
    {
        /// <summary>
        /// Returns the corresponding category of an AppVeyor message for an issue priority.
        /// </summary>
        /// <param name="priority">Priority of the issue.</param>
        /// <returns>Category for the AppVeyor message.</returns>
        public static AppVeyorMessageCategoryType ToAppVeyorMessageCategoryType(this int? priority)
        {
            if (priority == null)
            {
                return AppVeyorMessageCategoryType.Warning;
            }

            return priority switch
            {
                (int)IssuePriority.Error => AppVeyorMessageCategoryType.Error,
                (int)IssuePriority.Warning => AppVeyorMessageCategoryType.Warning,
                (int)IssuePriority.Hint or (int)IssuePriority.Suggestion => AppVeyorMessageCategoryType.Information,
                _ => AppVeyorMessageCategoryType.Warning,
            };
        }
    }
}
