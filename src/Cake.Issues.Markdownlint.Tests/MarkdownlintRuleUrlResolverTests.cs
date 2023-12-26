namespace Cake.Issues.Markdownlint.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
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

            [Theory]
            [InlineData("MD")]
            [InlineData("001")]
            [InlineData("MD001Foo")]
            [InlineData("Foo")]
            public void Should_Return_Null_For_Unknown_Rules(string rule)
            {
                // Given
                var urlResolver = MarkdownlintRuleUrlResolver.Instance;

                // When
                var ruleUrl = urlResolver.ResolveRuleUrl(rule);

                // Then
                ruleUrl.ShouldBeNull();
            }

            [Fact]
            public void Should_Resolve_Url_From_Custom_Resolvers()
            {
                // Given
                const string foo = "MD123";
                const string fooUrl = "http://foo.com/";
                const string bar = "MD456";
                const string barUrl = "http://bar.com/";
                var urlResolver = new MarkdownlintRuleUrlResolver();
                urlResolver.AddUrlResolver(x => x.Rule == foo ? new Uri(fooUrl) : null, 1);
                urlResolver.AddUrlResolver(x => x.Rule == bar ? new Uri(barUrl) : null, 1);

                // When
                var fooRuleUrl = urlResolver.ResolveRuleUrl(foo);
                var barRuleUrl = urlResolver.ResolveRuleUrl(bar);

                // Then
                fooRuleUrl.ToString().ShouldBe(fooUrl);
                barRuleUrl.ToString().ShouldBe(barUrl);
            }

            [Theory]
            [InlineData("MD0001", 1)]
            public void Should_Parse_RuleId(string rule, int expectedRuleId)
            {
                // Given
                int? ruleId = null;
                var urlResolver = new MarkdownlintRuleUrlResolver();
                urlResolver.AddUrlResolver(
                    x =>
                    {
                        ruleId = x.RuleId;
                        return null;
                    },
                    1);

                // When
                urlResolver.ResolveRuleUrl(rule);

                // Then
                ruleId.ShouldNotBeNull();
                ruleId.Value.ShouldBe(expectedRuleId);
            }
        }
    }
}
