namespace Cake.Issues.Tests
{
    using System.Text;
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class BaseConfigurableIssueProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var settings = new IssueProviderSettings(Encoding.UTF8.GetBytes("Foo"));

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
                IssueProviderSettings settings = null;

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
                var settings = new IssueProviderSettings(Encoding.UTF8.GetBytes("Foo"));

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
                var settings = new IssueProviderSettings(Encoding.UTF8.GetBytes("Foo"));

                // When
                var result = new FakeConfigurableIssueProvider(log, settings);

                // Then
                result.IssueProviderSettings.ShouldBe(settings);
            }
        }
    }
}
