namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class UriExtensionsTests
    {
        public sealed class TheAppendMethod
        {
            [Fact]
            public void Should_Throw_If_Uri_Is_Null()
            {
                // Given
                Uri uri = null;
                var path = "foo";

                // When
                var result = Record.Exception(() =>
                    uri.Append(path));

                // Then
                result.IsArgumentNullException("uri");
            }

            [Theory]
            [InlineData("https://google.com/foo/bar", "https://google.com", "foo", "bar")]
            [InlineData("https://google.com/foo/bar", "https://google.com/", "foo", "bar")]
            [InlineData("https://google.com/foo/bar", "https://google.com", "/foo", "bar")]
            [InlineData("https://google.com/foo/bar", "https://google.com", "foo/", "bar")]
            [InlineData("https://google.com/foo/bar", "https://google.com", "foo", "/bar")]
            [InlineData("https://google.com/foo/bar/", "https://google.com", "foo", "bar/")]
            [InlineData("https://google.com/foo/bar", "https://google.com", "foo/", "/bar")]
            [InlineData("https://google.com/bar", "https://google.com", null, "bar")]
            [InlineData("https://google.com/bar", "https://google.com", "", "bar")]
            [InlineData("https://google.com/bar", "https://google.com", " ", "bar")]
            [InlineData("https://google.com/bar", "https://google.com", "/", "/bar")]
            public void Should_Append_Paths(string expectedPath, string uri, params string[] paths)
            {
                // Given

                // When
                var result = new Uri(uri).Append(paths);

                // Then
                result.ToString().ShouldBe(expectedPath);
            }
        }
    }
}
