namespace Cake.Issues.Markdownlint.Tests
{
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class MarkdownlintRuleUrlResolverTests
    {
        public sealed class TheResolveRuleUrlMethod
        {
            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(null));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(" "));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Theory]
            [InlineData("MD001", "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md001")]
            [InlineData("MD002", "https://github.com/DavidAnson/markdownlint/blob/master/doc/Rules.md#md002")]
            public void Should_Resolve_Url(string rule, string expectedUrl)
            {
                // Given
                var urlResolver = MarkdownlintRuleUrlResolver.Instance;

                // When
                var ruleUrl = urlResolver.ResolveRuleUrl(rule);

                // Then
                ruleUrl.ToString().ShouldBe(expectedUrl);
            }
        }
    }
}
