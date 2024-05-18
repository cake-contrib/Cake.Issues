namespace Cake.Issues.Tests
{
    public sealed class BaseIssueProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new FakeIssueProvider(null));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();

                // When
                var provider = new FakeIssueProvider(log);

                // Then
                provider.Log.ShouldBe(log);
            }
        }

        public sealed class TheProviderTypeProperty
        {
            [Fact]
            public void Should_Return_Full_Type_Name_Of_Concrete_IssueProvider()
            {
                // Given
                var provider = new FakeIssueProvider(new FakeLog());

                // When
                var result = provider.ProviderType;

                // Then
                result.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var provider = new FakeIssueProvider(new FakeLog());

                // When
                var result = Record.Exception(provider.ReadIssues);

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }
        }
    }
}
