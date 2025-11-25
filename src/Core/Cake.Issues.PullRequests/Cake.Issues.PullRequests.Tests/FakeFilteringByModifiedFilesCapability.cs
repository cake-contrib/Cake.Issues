namespace Cake.Issues.PullRequests.Tests;

using Cake.Core.Diagnostics;
using Cake.Core.IO;

/// <summary>
/// Implementation of a <see cref="BaseFilteringByModifiedFilesCapability{T}"/> for use in test cases.
/// </summary>
public class FakeFilteringByModifiedFilesCapability : BaseFilteringByModifiedFilesCapability<FakePullRequestSystem>
{
    private readonly List<FilePath> modifiedFiles = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeFilteringByModifiedFilesCapability"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
    /// <param name="modifiedFiles">List of modified files which the pull request system capability should return.</param>
    public FakeFilteringByModifiedFilesCapability(
        ICakeLog log,
        FakePullRequestSystem pullRequestSystem,
        IEnumerable<FilePath> modifiedFiles)
        : base(log, pullRequestSystem)
    {
        modifiedFiles.NotNull();

        this.modifiedFiles.AddRange(modifiedFiles);
    }

    /// <summary>
    /// Gets the Cake log context.
    /// </summary>
    public new ICakeLog Log => base.Log;

    /// <summary>
    /// Gets the pull request system to which the capability belongs.
    /// </summary>
    public new FakePullRequestSystem PullRequestSystem => base.PullRequestSystem;

    /// <inheritdoc />
    protected override IEnumerable<FilePath> InternalGetModifiedFilesInPullRequest() => this.modifiedFiles;
}
