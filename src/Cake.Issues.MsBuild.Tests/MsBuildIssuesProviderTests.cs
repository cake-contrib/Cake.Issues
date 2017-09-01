namespace Cake.Issues.MsBuild.Tests
{
    using Cake.Testing;
    using Testing;
    using Xunit;

    public class MsBuildIssuesProviderTests
    {
        public sealed class TheMsBuildCodeAnalysisProviderCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new MsBuildIssuesProvider(
                        null,
                        MsBuildIssuesSettings.FromContent(
                            "Foo",
                            new XmlFileLoggerFormat(new FakeLog()))));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new MsBuildIssuesProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
