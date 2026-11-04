namespace Cake.Issues.BuildServer.AppVeyor;

using System;
using System.Collections.Generic;
using Cake.Common.Build;
using Cake.Core;

/// <summary>
/// Class for posting issues to AppVeyor.
/// </summary>
public class AppVeyorBuildServer : BaseBuildServer
{
    private readonly ICakeContext context;
    private readonly AppVeyorBuildSettings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppVeyorBuildServer"/> class.
    /// </summary>
    /// <param name="context">The Cake context.</param>
    /// <param name="settings">Settings for writing issues to AppVeyor.</param>
    public AppVeyorBuildServer(ICakeContext context, AppVeyorBuildSettings settings)
        : base(context?.Log ?? throw new ArgumentNullException(nameof(context)))
    {
        settings.NotNull();

        this.context = context;
        this.settings = settings;
    }

    /// <inheritdoc />
    protected override void InternalPostIssues(IEnumerable<IIssue> issues)
    {
        foreach (var issue in issues)
        {
            this.context.AppVeyor().AddMessage(
                this.settings.MessagePattern.ReplaceIssuePattern(issue),
                issue.Priority.ToAppVeyorMessageCategoryType(),
                this.settings.DetailsPattern.ReplaceIssuePattern(issue));
        }
    }
}
