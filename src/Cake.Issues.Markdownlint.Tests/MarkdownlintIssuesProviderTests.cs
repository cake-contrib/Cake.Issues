namespace Cake.Issues.Markdownlint.Tests.Markdownlint
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.Markdownlint;
    using Cake.Testing;
    using Testing;
    using Xunit;

    public sealed class MarkdownlintIssuesProviderTests
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
                    new MarkdownlintIssuesProvider(
                        log,
                        MarkdownlintIssuesSettings.FromContent("Foo", new MarkdownlintLogFileFormat(new FakeLog()))));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new MarkdownlintIssuesProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
