﻿namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class PullRequestIssueResultTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_ReportedIssues_Is_Null()
            {
                // Given
                IEnumerable<IIssue> reportedIssues = null;
                IEnumerable<IIssue> postedIssues = new List<IIssue>();

                // When
                var result = Record.Exception(() => new PullRequestIssueResult(reportedIssues, postedIssues));

                // Then
                result.IsArgumentNullException("reportedIssues");
            }

            [Fact]
            public void Should_Throw_If_PostedIssues_Is_Null()
            {
                // Given
                IEnumerable<IIssue> reportedIssues = new List<IIssue>();
                IEnumerable<IIssue> postedIssues = null;

                // When
                var result = Record.Exception(() => new PullRequestIssueResult(reportedIssues, postedIssues));

                // Then
                result.IsArgumentNullException("postedIssues");
            }

            [Fact]
            public void Should_Set_ReportedIssues()
            {
                // Given
                IEnumerable<IIssue> reportedIssues = new List<IIssue>();
                IEnumerable<IIssue> postedIssues = new List<IIssue>();

                // When
                var result = new PullRequestIssueResult(reportedIssues, postedIssues);

                // Then
                result.ReportedIssues.ShouldBe(reportedIssues);
            }

            [Fact]
            public void Should_Set_PostedIssues()
            {
                // Given
                IEnumerable<IIssue> reportedIssues = new List<IIssue>();
                IEnumerable<IIssue> postedIssues = new List<IIssue>();

                // When
                var result = new PullRequestIssueResult(reportedIssues, postedIssues);

                // Then
                result.PostedIssues.ShouldBe(postedIssues);
            }
        }
    }
}
