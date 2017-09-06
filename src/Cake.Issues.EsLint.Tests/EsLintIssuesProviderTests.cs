namespace Cake.Issues.EsLint.Tests
{
    using Cake.Testing;
    using Xunit;

    public class EsLintIssuesProviderTests
    {
        public sealed class TheEsLintIssuesProviderCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new EsLintIssuesProvider(
                        null,
                        EsLintIssuesSettings.FromContent(
                            "Foo",
                            new JsonFormat(new FakeLog()))));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new EsLintIssuesProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
