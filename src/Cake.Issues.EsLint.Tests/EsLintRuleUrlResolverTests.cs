namespace Cake.Issues.EsLint.Tests
{
    using System;
    using Shouldly;
    using Xunit;

    public sealed class EsLintRuleUrlResolverTests
    {
        public sealed class TheResolveRuleUrlMethod
        {
            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => EsLintRuleUrlResolver.Instance.ResolveRuleUrl(null));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => EsLintRuleUrlResolver.Instance.ResolveRuleUrl(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => EsLintRuleUrlResolver.Instance.ResolveRuleUrl(" "));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Theory]
            [InlineData("no-unused-vars", "http://eslint.org/docs/rules/no-unused-vars")]
            [InlineData("no-await-in-loop", "http://eslint.org/docs/rules/no-await-in-loop")]
            public void Should_Resolve_Url(string rule, string expectedUrl)
            {
                // Given
                var urlResolver = EsLintRuleUrlResolver.Instance;

                // When
                var ruleUrl = urlResolver.ResolveRuleUrl(rule);

                // Then
                ruleUrl.ToString().ShouldBe(expectedUrl);
            }

            [Fact]
            public void Should_Resolve_Url_From_Custom_Resolvers()
            {
                // Given
                const string foo = "FOO123";
                const string fooUrl = "http://foo.com/";
                const string bar = "BAR123";
                const string barUrl = "http://bar.com/";
                var urlResolver = EsLintRuleUrlResolver.Instance;
                urlResolver.AddUrlResolver(x => x.Rule == foo ? new Uri(fooUrl) : null, 1);
                urlResolver.AddUrlResolver(x => x.Rule == bar ? new Uri(barUrl) : null, 1);

                // When
                var fooRuleUrl = urlResolver.ResolveRuleUrl(foo);
                var barRuleUrl = urlResolver.ResolveRuleUrl(bar);

                // Then
                fooRuleUrl.ToString().ShouldBe(fooUrl);
                barRuleUrl.ToString().ShouldBe(barUrl);
            }
        }
    }
}
