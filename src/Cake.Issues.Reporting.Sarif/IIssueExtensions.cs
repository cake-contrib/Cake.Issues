namespace Cake.Issues.Reporting.Sarif
{
    using System;
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

            return issue.Priority switch
            {
                (int)IssuePriority.Suggestion or (int)IssuePriority.Hint => ResultKind.Informational,
                (int)IssuePriority.Warning or (int)IssuePriority.Error => ResultKind.Fail,
                _ => ResultKind.NotApplicable,
            };
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

            return issue.Priority switch
            {
                (int)IssuePriority.Suggestion or (int)IssuePriority.Hint => FailureLevel.Note,
                (int)IssuePriority.Warning => FailureLevel.Warning,
                (int)IssuePriority.Error => FailureLevel.Error,
                _ => FailureLevel.None,
            };
        }

        /// <summary>
        /// Returns the location of the issue.
        /// </summary>
        /// <param name="issue">Issue for which the location should be returned.</param>
        /// <returns>Location of the issue.</returns>
        public static Location Location(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

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

                if (issue.Column.HasValue)
                {
                    result.PhysicalLocation.Region.StartColumn = issue.Column.Value;
                }

                if (issue.EndColumn.HasValue)
                {
                    result.PhysicalLocation.Region.EndColumn = issue.EndColumn.Value;
                }
            }

            return result;
        }
    }
}
