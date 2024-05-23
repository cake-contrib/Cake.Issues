namespace Cake.Issues.Tests.FileLinking;

using Cake.Issues.FileLinking;

public sealed class IDictionaryExtensionsTests
{
    public sealed class TheGetValueOrDefaultExtension
    {
        [Fact]
        public void Should_Throw_If_Dictionary_Is_Null()
        {
            // Given
            const System.Collections.Generic.IDictionary<string, string> dictionary = null;

            // When
            var result = Record.Exception(() => dictionary.GetValueOrDefault("foo", null));

            // Then
            result.IsArgumentNullException("dictionary");
        }

        [Fact]
        public void Should_Return_Value_If_Exists()
        {
            // Given
            const string key = "foo";
            const string value = "bar";
            var dictionary = new System.Collections.Generic.Dictionary<string, string> { { key, value } };

            // When
            var result = dictionary.GetValueOrDefault(key, null);

            // Then
            result.ShouldBe(value);
        }

        [Fact]
        public void Should_Return_DefaultValue_If_Value_Does_Not_Exist()
        {
            // Given
            const string defaultValue = "defaultValue";
            var dictionary = new System.Collections.Generic.Dictionary<string, string> { { "foo", "bar" } };

            // When
            var result = dictionary.GetValueOrDefault("bar", defaultValue);

            // Then
            result.ShouldBe(defaultValue);
        }
    }
}
