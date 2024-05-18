namespace Cake.Issues.Tests
{
    using Cake.Core.Diagnostics;

    public sealed class BaseConfigurableIssueProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;
                var settings = new IssueProviderSettings("Foo".ToByteArray());

                // When
                var result = Record.Exception(() => new FakeConfigurableIssueProvider(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_IssueProviderSettings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                const IssueProviderSettings settings = null;

                // When
                var result = Record.Exception(() => new FakeConfigurableIssueProvider(log, settings));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();
                var settings = new IssueProviderSettings("Foo".ToByteArray());

                // When
                var result = new FakeConfigurableIssueProvider(log, settings);

                // Then
                result.Log.ShouldBe(log);
            }

            [Fact]
            public void Should_Set_IssueProviderSettings()
            {
                // Given
                var log = new FakeLog();
                var settings = new IssueProviderSettings("Foo".ToByteArray());

                // When
                var result = new FakeConfigurableIssueProvider(log, settings);

                // Then
                result.IssueProviderSettings.ShouldBe(settings);
            }
        }
    }
}
