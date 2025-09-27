namespace Cake.Issues.Testing;

using System.Collections.Generic;
using Cake.Core.Diagnostics;

/// <summary>
/// Implementation of a <see cref="BaseIssueProvider"/> that fails during initialization for use in test cases.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public class FakeFailingIssueProvider(ICakeLog log) : BaseIssueProvider(log)
{
    /// <inheritdoc/>
    public override string ProviderName => "Fake Failing Issue Provider";

    /// <inheritdoc/>
    public override bool Initialize(IReadIssuesSettings settings) => false;

    /// <inheritdoc/>
    protected override IEnumerable<IIssue> InternalReadIssues() => [];
}