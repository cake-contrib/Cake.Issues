namespace Cake.Issues.Tests
{
    public sealed class IssuesArgumentChecksTests
    {
        public sealed class TheNotNullExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const string value = null;

                // When
                var result = Record.Exception(() => value.NotNull("foo"));

                // Then
                result.IsArgumentNullException("foo");
            }

            [Theory]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Set(object value)
            {
                value.NotNull("foo");
            }
        }

        public sealed class TheNotNullOrWhiteSpaceExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const string value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace("foo"));

                // Then
                result.IsArgumentNullException("foo");
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Empty()
            {
                // Given
                var value = string.Empty;

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Fact]
            public void Should_Throw_If_Value_Is_WhiteSpace()
            {
                // Given
                const string value = " ";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Theory]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Valid(string value)
            {
                value.NotNullOrWhiteSpace("foo");
            }
        }

        public sealed class TheNotNegativeExtension
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_Value_Is_Negative(int value)
            {
                // When
                var result = Record.Exception(() => value.NotNegative("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Not_Throw_If_Value_Is_Valid(int value)
            {
                value.NotNegative("foo");
            }
        }

        public sealed class TheNotNegativeOrZeroExtension
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_Value_Is_Negative(int value)
            {
                // When
                var result = Record.Exception(() => value.NotNegativeOrZero("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Zero()
            {
                // Given
                const int value = 0;

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Not_Throw_If_Value_Is_Valid(int value)
            {
                value.NotNegative("foo");
            }
        }

        public sealed class TheNotNullOrEmptyExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const List<int> value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrEmpty("foo"));

                // Then
                result.IsArgumentNullException("foo");
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();

                // When
                var result = Record.Exception(() => value.NotNullOrEmpty("foo"));

                // Then
                result.IsArgumentException("foo");
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Valid(string value)
            {
                // Given
                var values = new List<string> { value };

                // When
                values.NotNullOrEmpty("foo");

                // Then
            }
        }

        public sealed class TheNotNullOrEmptyElementExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const List<int> value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyElement("foo"));

                // Then
                result.IsArgumentNullException("foo");
            }

            [Fact]
            public void Should_Not_Throw_If_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();

                // When
                value.NotNullOrEmptyElement("foo");

                // Then
            }

            [Fact]
            public void Should_Throw_If_Value_Contains_Null()
            {
                // Given
                var value = new List<string> { null };

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyElement("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Theory]
            [InlineData("")]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Valid(string value)
            {
                // Given
                var values = new List<string> { value };

                // When
                values.NotNullOrEmptyElement("foo");

                // Then
            }
        }

        public sealed class TheNotNullOrEmptyOrEmptyElementExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const List<int> value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement("foo"));

                // Then
                result.IsArgumentNullException("foo");
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement("foo"));

                // Then
                result.IsArgumentException("foo");
            }

            [Fact]
            public void Should_Throw_If_Value_Contains_Null()
            {
                // Given
                var value = new List<string> { null };

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement("foo"));

                // Then
                result.IsArgumentOutOfRangeException("foo");
            }

            [Theory]
            [InlineData("")]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Valid(string value)
            {
                // Given
                var values = new List<string> { value };

                // When
                values.NotNullOrEmptyOrEmptyElement("foo");

                // Then
            }
        }
    }
}
