namespace Cake.Issues.Sarif.Tests
{
    using System;
    using System.Linq;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class SarifIssuesProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SarifIssuesProvider(
                        null,
                        new SarifIssuesSettings("Foo".ToByteArray())));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_IssueProviderSettings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() => new SarifIssuesProvider(new FakeLog(), null));

                // Then
                result.IsArgumentNullException("issueProviderSettings");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Issue_Correct()
            {
                // Given
                var fixture = new SarifIssuesProviderFixture("minimal.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                // TODO Update after implementation
                issues.Count.ShouldBe(0);
            }
        }
    }
}
