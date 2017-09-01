namespace Cake.Issues.MsBuild.Tests
{
    using System;
    using Shouldly;
    using Testing;
    using Xunit;

    public class MsBuildRuleUrlResolverTests
    {
        public sealed class TheResolveRuleUrlMethod
        {
            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(null));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(" "));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Theory]
            [InlineData("CA2201", "https://www.google.im/search?q=\"CA2201:\"+site:msdn.microsoft.com")]
            [InlineData("SA1652", "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1652.md")]
            public void Should_Resolve_Url(string rule, string expectedUrl)
            {
                // Given
                var urlResolver = MsBuildRuleUrlResolver.Instance;

                // When
                var ruleUrl = urlResolver.ResolveRuleUrl(rule);

                // Then
                ruleUrl.ToString().ShouldBe(expectedUrl);
            }

            [Theory]
            [InlineData("CA")]
            [InlineData("2201")]
            [InlineData("CA2201Foo")]
            [InlineData("CS0219")]
            public void Should_Return_Null_For_Unknown_Rules(string rule)
            {
                // Given
                var urlResolver = MsBuildRuleUrlResolver.Instance;

                // When
                var ruleUrl = urlResolver.ResolveRuleUrl(rule);

                // Then
                ruleUrl.ShouldBeNull();
            }

            [Fact]
            public void Should_Resolve_Url_From_Custom_Resolvers()
            {
                // Given
                const string foo = "FOO123";
                const string fooUrl = "http://foo.com/";
                const string bar = "BAR123";
                const string barUrl = "http://bar.com/";
                var urlResolver = MsBuildRuleUrlResolver.Instance;
                urlResolver.AddUrlResolver(x => x.Rule == foo ? new Uri(fooUrl) : null);
                urlResolver.AddUrlResolver(x => x.Rule == bar ? new Uri(barUrl) : null);

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
