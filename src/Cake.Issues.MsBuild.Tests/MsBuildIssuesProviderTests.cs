namespace Cake.Issues.MsBuild.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.MsBuild.LogFileFormat;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Xunit;

    public sealed class MsBuildIssuesProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;
                var settings =
                    new MsBuildIssuesSettings(
                        "Foo".ToByteArray(),
                        new XmlFileLoggerLogFileFormat(new FakeLog()));

                // When
                var result = Record.Exception(() => new MsBuildIssuesProvider(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_IssueProviderSettings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                const MsBuildIssuesSettings settings = null;

                // When
                var result = Record.Exception(() => new MsBuildIssuesProvider(log, settings));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }
        }
    }
}
