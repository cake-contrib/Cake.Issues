namespace Cake.Issues.MsBuild.Tests
{
    using System.Text;
    using Cake.Core.Diagnostics;
    using Cake.Testing;
    using Testing;
    using Xunit;

    public sealed class MsBuildIssuesProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var settings =
                    new MsBuildIssuesSettings(
                        Encoding.UTF8.GetBytes("Foo"),
                        new XmlFileLoggerFormat(new FakeLog()));

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
                MsBuildIssuesSettings settings = null;

                // When
                var result = Record.Exception(() => new MsBuildIssuesProvider(log, settings));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }
        }
    }
}
