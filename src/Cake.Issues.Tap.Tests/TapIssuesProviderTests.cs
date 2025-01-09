namespace Cake.Issues.Tap.Tests;

using Cake.Core.Diagnostics;
using Cake.Issues.Tap.LogFileFormat;

public sealed class TapIssuesProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            ICakeLog log = null;

            // When
            var result = Record.Exception(() =>
                new TapIssuesProvider(
                    log,
                    new TapIssuesSettings("Foo".ToByteArray(), new GenericLogFileFormat(new FakeLog()))));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_IssueProviderSettings_Are_Null()
        {
            var result = Record.Exception(() =>
                new TapIssuesProvider(
                    new FakeLog(),
                    null));

            // Then
            result.IsArgumentNullException("issueProviderSettings");
        }
    }
}
