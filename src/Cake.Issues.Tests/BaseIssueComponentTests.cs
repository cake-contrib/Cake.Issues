namespace Cake.Issues.Tests
{
    public sealed class BaseIssueComponentTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new FakeIssueComponent(null));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();

                // When
                var provider = new FakeIssueComponent(log);

                // Then
                provider.Log.ShouldBe(log);
            }
        }

        public sealed class TheInitializeMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var provider = new FakeIssueComponent(new FakeLog());

                // When
                var result = Record.Exception(() => provider.Initialize(null));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Set_Settings()
            {
                // Given
                var provider = new FakeIssueComponent(new FakeLog());
                var settings = new RepositorySettings(@"c:\foo");

                // When
                _ = provider.Initialize(settings);

                // Then
                provider.Settings.ShouldBe(settings);
            }

            [Fact]
            public void Should_Return_True()
            {
                // Given
                var provider = new FakeIssueComponent(new FakeLog());
                var settings = new RepositorySettings(@"c:\foo");

                // When
                var result = provider.Initialize(settings);

                // Then
                result.ShouldBeTrue();
            }
        }

        public sealed class TheAssertInitializedMethod
        {
            [Fact]
            public void Should_Throw_If_Initialize_Was_Not_Called()
            {
                // Given
                var provider = new FakeIssueComponent(new FakeLog());

                // When
                var result = Record.Exception(provider.AssertInitialized);

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }

            [Fact]
            public void Should_Not_Throw_If_Initialize_Was_Called()
            {
                // Given
                var provider = new FakeIssueComponent(new FakeLog());
                var settings = new RepositorySettings(@"c:\foo");
                _ = provider.Initialize(settings);

                // When
                provider.AssertInitialized();

                // Then
            }
        }
    }
}
