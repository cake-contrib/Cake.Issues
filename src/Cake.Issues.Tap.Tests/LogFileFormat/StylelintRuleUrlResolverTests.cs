namespace Cake.Issues.Tap.Tests.LogFileFormat;

using Cake.Issues.Tap.LogFileFormat;

public sealed class StylelintRuleUrlResolverTests
{
    public sealed class TheResolveRuleUrlMethod
    {
        [Fact]
        public void Should_Throw_If_Rule_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => StylelintRuleUrlResolver.Instance.ResolveRuleUrl(null));

            // Then
            result.IsArgumentNullException("rule");
        }

        [Fact]
        public void Should_Throw_If_Rule_Is_Empty()
        {
            // Given / When
            var result = Record.Exception(() => StylelintRuleUrlResolver.Instance.ResolveRuleUrl(string.Empty));

            // Then
            result.IsArgumentOutOfRangeException("rule");
        }

        [Fact]
        public void Should_Throw_If_Rule_Is_WhiteSpace()
        {
            // Given / When
            var result = Record.Exception(() => StylelintRuleUrlResolver.Instance.ResolveRuleUrl(" "));

            // Then
            result.IsArgumentOutOfRangeException("rule");
        }

        [Theory]
        [InlineData("block-no-empty", "https://stylelint.io/user-guide/rules/block-no-empty")]
        public void Should_Resolve_Url(string rule, string expectedUrl)
        {
            // Given
            var urlResolver = StylelintRuleUrlResolver.Instance;

            // When
            var ruleUrl = urlResolver.ResolveRuleUrl(rule);

            // Then
            ruleUrl.ToString().ShouldBe(expectedUrl);
        }

        [Theory]
        [InlineData("csstools/use-logical")]
        public void Should_Not_Resolve_Url_When_Issue_From_Plugin(string rule)
        {
            // Given
            var urlResolver = StylelintRuleUrlResolver.Instance;

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
            var urlResolver = StylelintRuleUrlResolver.Instance;
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
