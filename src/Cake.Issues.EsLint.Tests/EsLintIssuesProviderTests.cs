﻿namespace Cake.Issues.EsLint.Tests
{
    using System.Text;
    using Cake.Core.Diagnostics;
    using Cake.Issues.EsLint.LogFileFormat;
    using Cake.Testing;
    using Xunit;

    public sealed class EsLintIssuesProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var settings =
                    new EsLintIssuesSettings(
                        Encoding.UTF8.GetBytes("Foo"),
                        new JsonLogFileFormat(new FakeLog()));

                // When
                var result = Record.Exception(() => new EsLintIssuesProvider(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_IssueProviderSettings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                EsLintIssuesSettings settings = null;

                // When
                var result = Record.Exception(() => new EsLintIssuesProvider(log, settings));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }
        }
    }
}
