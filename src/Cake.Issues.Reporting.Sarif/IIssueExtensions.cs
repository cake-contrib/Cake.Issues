namespace Cake.Issues.Reporting.Sarif
{
    using System;
    using Cake.Core.IO;
    using Microsoft.CodeAnalysis.Sarif;

    /// <summary>
    /// Extensions for <see cref="IIssue"/>.
    /// </summary>
    internal static class IIssueExtensions
    {
        /// <summary>
        /// Returns the kind of the issue.
        /// </summary>
        /// <param name="issue">Issue for which the kind should be returned.</param>
        /// <returns>Kind of the issue.</returns>
        public static ResultKind Kind(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            if (!issue.Priority.HasValue)
            {
                return ResultKind.None;
            }

            switch (issue.Priority)
            {
                case (int)IssuePriority.Suggestion:
                case (int)IssuePriority.Hint:
                    return ResultKind.Informational;
                case (int)IssuePriority.Warning:
                case (int)IssuePriority.Error:
                    return ResultKind.Fail;
                default:
                    return ResultKind.NotApplicable;
            }
        }

        /// <summary>
        /// Returns the level of the issue.
        /// </summary>
        /// <param name="issue">Issue for which the level should be returned.</param>
        /// <returns>Level of the issue.</returns>
        public static FailureLevel Level(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            if (!issue.Priority.HasValue)
            {
                return FailureLevel.None;
            }

            switch (issue.Priority)
            {
                case (int)IssuePriority.Suggestion:
                case (int)IssuePriority.Hint:
                    return FailureLevel.Note;
                case (int)IssuePriority.Warning:
                    return FailureLevel.Warning;
                case (int)IssuePriority.Error:
                    return FailureLevel.Error;
                default:
                    return FailureLevel.None;
            }
        }

        /// <summary>
        /// Returns the location of the issue.
        /// </summary>
        /// <param name="issue">Issue for which the location should be returned.</param>
        /// <param name="repositoryRoot">Root path to the directory.</param>
        /// <returns>Location of the issue.</returns>
        public static Location Location(this IIssue issue, DirectoryPath repositoryRoot)
        {
            issue.NotNull(nameof(issue));
            issue.NotNull(nameof(repositoryRoot));

            if (issue.AffectedFileRelativePath == null && !issue.Line.HasValue)
            {
                return null;
            }

            var result = new Location
            {
                PhysicalLocation = new PhysicalLocation(),
            };

            if (issue.AffectedFileRelativePath != null)
            {
                result.PhysicalLocation.ArtifactLocation =
                    new ArtifactLocation
                    {
                        UriBaseId = SarifIssueReportGenerator.RepoRootUriBaseId,
                        Uri = new Uri(issue.FilePath(), UriKind.RelativeOrAbsolute),
                    };
            }

            if (issue.Line.HasValue)
            {
                result.PhysicalLocation.Region =
                    new Region
                    {
                        StartLine = issue.Line.Value,
                    };

                if (issue.EndLine.HasValue)
                {
                    result.PhysicalLocation.Region.EndLine = issue.EndLine.Value;
                }
            }

            return result;
        }
    }
}
