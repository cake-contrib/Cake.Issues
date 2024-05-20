namespace Cake.Issues.Tests
{
    public sealed class IssuesArgumentChecksTests
    {
        public sealed class TheNotNullExtension
        {
            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed()
            {
                // Given
                const string value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNull(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed()
            {
                // Given
                const string value = null;

                // When
                var result = Record.Exception(() => value.NotNull());

                // Then
                result.IsArgumentNullException(nameof(value));
            }

            [Theory]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Set(object value) => value.NotNull("foo");
        }

        public sealed class TheNotNullOrWhiteSpaceExtension
        {
            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Null()
            {
                // Given
                const string value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Null()
            {
                // Given
                const string value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace());

                // Then
                result.IsArgumentNullException(nameof(value));
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Empty()
            {
                // Given
                var value = string.Empty;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Empty()
            {
                // Given
                var value = string.Empty;

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace());

                // Then
                result.IsArgumentOutOfRangeException(nameof(value));
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_WhiteSpace()
            {
                // Given
                const string value = " ";
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_WhiteSpace()
            {
                // Given
                const string value = " ";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace());

                // Then
                result.IsArgumentOutOfRangeException(nameof(value));
            }

            [Theory]
            [InlineData("foo")]
            public void Should_Not_Throw_If_Value_Is_Valid(string value) => value.NotNullOrWhiteSpace("foo");
        }

        public sealed class TheNotNegativeExtension
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Negative(int value)
            {
                // Given
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNegative(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Negative(int value)
            {
                // When
                var result = Record.Exception(() => value.NotNegative());

                // Then
                result.IsArgumentOutOfRangeException(nameof(value));
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Not_Throw_If_Value_Is_Valid(int value) => value.NotNegative("foo");
        }

        public sealed class TheNotNegativeOrZeroExtension
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Negative(int value)
            {
                // Given
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Negative(int value)
            {
                // When
                var result = Record.Exception(() => value.NotNegativeOrZero());

                // Then
                result.IsArgumentOutOfRangeException(nameof(value));
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Zero()
            {
                // Given
                const int value = 0;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Zero()
            {
                // Given
                const int value = 0;

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero());

                // Then
                result.IsArgumentOutOfRangeException(nameof(value));
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Not_Throw_If_Value_Is_Valid(int value) => value.NotNegative("foo");
        }

        public sealed class TheNotNullOrEmptyExtension
        {
            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Null()
            {
                // Given
                const List<int> value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrEmpty(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Null()
            {
                // Given
                const List<int> value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrEmpty());

                // Then
                result.IsArgumentNullException(nameof(value));
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrEmpty(parameterName));

                // Then
                result.IsArgumentException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();

                // When
                var result = Record.Exception(() => value.NotNullOrEmpty());

                // Then
                result.IsArgumentException(nameof(value));
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
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Null()
            {
                // Given
                const List<int> value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyElement(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Null()
            {
                // Given
                const List<int> value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyElement());

                // Then
                result.IsArgumentNullException(nameof(value));
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
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Contains_Null()
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
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Null()
            {
                // Given
                const List<int> value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Null()
            {
                // Given
                const List<int> value = null;

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement());

                // Then
                result.IsArgumentNullException(nameof(value));
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement(parameterName));

                // Then
                result.IsArgumentException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Is_Empty()
            {
                // Given
                var value = new List<int>();

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement());

                // Then
                result.IsArgumentException(nameof(value));
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Passed_And_Value_Contains_Null()
            {
                // Given
                var value = new List<string> { null };
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_With_Correct_ParameterName_If_ParameterName_Is_Not_Passed_And_Value_Contains_Null()
            {
                // Given
                var value = new List<string> { null };

                // When
                var result = Record.Exception(() => value.NotNullOrEmptyOrEmptyElement());

                // Then
                result.IsArgumentOutOfRangeException(nameof(value));
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
