namespace Cake.Issues.Tests
{
    public class BaseRuleUrlResolverTests
    {
        public sealed class TheClass
        {
            private class FakeRuleUrlResolverForBaseRuleDescription : BaseRuleUrlResolver<BaseRuleDescription>
            {
                /// <inheritdoc/>
                protected override bool TryGetRuleDescription(string rule, BaseRuleDescription ruleDescription)
                {
                    return true;
                }
            }
        }

        public sealed class TheAddUrlResolverMethod
        {
            [Fact]
            public void Should_Throw_If_Resolver_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new FakeRuleUrlResolver().AddUrlResolver(null));

                // Then
                result.IsArgumentNullException("resolver");
            }
        }

        public sealed class TheResolveRuleUrlMethod
        {
            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new FakeRuleUrlResolver().ResolveRuleUrl(null));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new FakeRuleUrlResolver().ResolveRuleUrl(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new FakeRuleUrlResolver().ResolveRuleUrl(" "));

                // Then
                result.IsArgumentOutOfRangeException("rule");
            }

            [Fact]
            public void Should_Resolve_Url()
            {
                // Given
                const string url = "http://google.com/";
                var urlResolver = new FakeRuleUrlResolver();
                urlResolver.AddUrlResolver(_ => new Uri(url));

                // When
                var ruleUrl = urlResolver.ResolveRuleUrl("foo");

                // Then
                ruleUrl.ToString().ShouldBe(url);
            }

            [Fact]
            public void Should_Return_Null_If_No_Resolver_Was_Found()
            {
                // Given
                const string foo = "foo";
                const string fooUrl = "http://foo.com/";
                var urlResolver = new FakeRuleUrlResolver();
                urlResolver.AddUrlResolver(x => x.Rule == foo ? new Uri(fooUrl) : null);

                // When
                var result = urlResolver.ResolveRuleUrl("bar");

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Resolve_Url_With_Multiple_Resolvers()
            {
                // Given
                const string foo = "foo";
                const string fooUrl = "http://foo.com/";
                const string bar = "bar";
                const string barUrl = "http://bar.com/";
                var urlResolver = new FakeRuleUrlResolver();
                urlResolver.AddUrlResolver(x => x.Rule == foo ? new Uri(fooUrl) : null);
                urlResolver.AddUrlResolver(x => x.Rule == bar ? new Uri(barUrl) : null);

                // When
                var fooRuleUrl = urlResolver.ResolveRuleUrl(foo);
                var barRuleUrl = urlResolver.ResolveRuleUrl(bar);

                // Then
                fooRuleUrl.ToString().ShouldBe(fooUrl);
                barRuleUrl.ToString().ShouldBe(barUrl);
            }

            [Theory]
            [InlineData(0, "http://foo.com/")]
            [InlineData(-1, "http://foo.com/")]
            [InlineData(1, "http://bar.com/")]
            public void Should_Take_Priority_Into_Account(int secondPriority, string expectedUrl)
            {
                // Given
                const string fooUrl = "http://foo.com/";
                const string barUrl = "http://bar.com/";
                var urlResolver = new FakeRuleUrlResolver();
                urlResolver.AddUrlResolver(_ => new Uri(fooUrl), 0);
                urlResolver.AddUrlResolver(_ => new Uri(barUrl), secondPriority);

                // When
                var result = urlResolver.ResolveRuleUrl("foo");

                // Then
                result.ToString().ShouldBe(expectedUrl);
            }
        }
    }
}
