namespace Cake.Issues.Build.Tests;

using System.Collections.Generic;
using Cake.Core.Diagnostics;
using NSubstitute;
using Shouldly;
using Xunit;

public sealed class BuildServerIntegrationTests
{
    public sealed class TheBasicWorkflow
    {
        [Fact]
        public void Should_Work_With_Build_Server_Implementation()
        {
            // Given
            var log = Substitute.For<ICakeLog>();
            var buildServer = new TestBuildServerSystem(log);
            var settings = new ReportIssuesToBuildServerSettings(@"C:\repo");

            var issues = new List<IIssue>
            {
                IssueBuilder.NewIssue("Test issue", "TestProvider", "TestProviderName")
                    .Create()
            };

            // When
            buildServer.Initialize(settings);
            buildServer.PostIssues(issues);

            // Then
            buildServer.PostedIssues.ShouldHaveSingleItem();
            buildServer.PostedIssues[0].MessageText.ShouldBe("Test issue");
        }
    }

    /// <summary>
    /// Simple test implementation of a build server system.
    /// </summary>
    private class TestBuildServerSystem : BaseBuildServerSystem
    {
        public TestBuildServerSystem(ICakeLog log) : base(log)
        {
            this.PostedIssues = new List<IIssue>();
        }

        public List<IIssue> PostedIssues { get; }

        protected override void InternalPostIssues(IEnumerable<IIssue> issues)
        {
            this.PostedIssues.AddRange(issues);
        }
    }
}