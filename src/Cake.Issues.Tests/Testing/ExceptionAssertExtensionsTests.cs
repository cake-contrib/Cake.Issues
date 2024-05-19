namespace Cake.Issues.Tests.Testing
{
    public sealed class ExceptionAssertExtensionsTests
    {
        public sealed class TheIsArgumentExceptionMethod
        {
            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Null()
            {
                // Given
                const Exception exception = null;

                // When
                var result = Record.Exception(() => exception.IsArgumentException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.ArgumentException' but no exception was thrown.");
            }

            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Not_ArgumentException()
            {
                // Given
                var exception = new Exception("Foo");

                // When
                var result = Record.Exception(() => exception.IsArgumentException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.ArgumentException' but was 'System.Exception'.");
            }

            [Fact]
            public void Should_Not_Throw_If_Exception_Type_Is_ArgumentException()
            {
                // Given
                var exception = new ArgumentException("Message", "Foo");

                // When
                exception.IsArgumentException("Foo");

                // Then
            }

            [Theory]
            [InlineData("Foo", "Bar")]
            [InlineData(null, "Foo")]
            [InlineData("Foo", null)]
            public void Should_Throw_If_ParamName_Is_Different(string actualParamName, string expectedParamName)
            {
                // Given
                var exception = new ArgumentException("Foo", actualParamName);

                // When
                var result = Record.Exception(() => exception.IsArgumentException(expectedParamName));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe($"Expected parameter name to be '{expectedParamName}' but was '{actualParamName}'.");
            }

            [Theory]
            [InlineData("Foo", "Foo")]
            [InlineData(null, null)]
            [InlineData("", "")]
            public void Should_Not_Throw_If_ParamName_Is_The_Same(string actualParamName, string expectedParamName)
            {
                // Given
                var exception = new ArgumentException("Foo", actualParamName);

                // When
                exception.IsArgumentException(expectedParamName);

                // Then
            }
        }

        public sealed class TheIsArgumentNullExceptionMethod
        {
            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Null()
            {
                // Given
                const Exception exception = null;

                // When
                var result = Record.Exception(() => exception.IsArgumentNullException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.ArgumentNullException' but no exception was thrown.");
            }

            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Not_ArgumentNullException()
            {
                // Given
                var exception = new Exception("Foo");

                // When
                var result = Record.Exception(() => exception.IsArgumentNullException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.ArgumentNullException' but was 'System.Exception'.");
            }

            [Fact]
            public void Should_Not_Throw_If_Exception_Type_Is_ArgumentNullException()
            {
                // Given
                var exception = new ArgumentNullException("Foo");

                // When
                exception.IsArgumentNullException("Foo");

                // Then
            }

            [Theory]
            [InlineData("Foo", "Bar")]
            [InlineData(null, "Foo")]
            [InlineData("Foo", null)]
            public void Should_Throw_If_ParamName_Is_Different(string actualParamName, string expectedParamName)
            {
                // Given
                var exception = new ArgumentNullException(actualParamName);

                // When
                var result = Record.Exception(() => exception.IsArgumentNullException(expectedParamName));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe($"Expected parameter name to be '{expectedParamName}' but was '{actualParamName}'.");
            }

            [Theory]
            [InlineData("Foo", "Foo")]
            [InlineData(null, null)]
            [InlineData("", "")]
            public void Should_Not_Throw_If_ParamName_Is_The_Same(string actualParamName, string expectedParamName)
            {
                // Given
                var exception = new ArgumentNullException(actualParamName);

                // When
                exception.IsArgumentNullException(expectedParamName);

                // Then
            }
        }

        public sealed class TheIsArgumentOutOfRangeExceptionMethod
        {
            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Null()
            {
                // Given
                const Exception exception = null;

                // When
                var result = Record.Exception(() => exception.IsArgumentOutOfRangeException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.ArgumentOutOfRangeException' but no exception was thrown.");
            }

            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Not_ArgumentOutOfRangeException()
            {
                // Given
                var exception = new Exception("Foo");

                // When
                var result = Record.Exception(() => exception.IsArgumentOutOfRangeException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.ArgumentOutOfRangeException' but was 'System.Exception'.");
            }

            [Fact]
            public void Should_Not_Throw_If_Exception_Type_Is_ArgumentOutOfRangeException()
            {
                // Given
                var exception = new ArgumentOutOfRangeException("Foo");

                // When
                exception.IsArgumentOutOfRangeException("Foo");

                // Then
            }

            [Theory]
            [InlineData("Foo", "Bar")]
            [InlineData(null, "Foo")]
            [InlineData("Foo", null)]
            public void Should_Throw_If_ParamName_Is_Different(string actualParamName, string expectedParamName)
            {
                // Given
                var exception = new ArgumentOutOfRangeException(actualParamName);

                // When
                var result = Record.Exception(() => exception.IsArgumentOutOfRangeException(expectedParamName));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe($"Expected parameter name to be '{expectedParamName}' but was '{actualParamName}'.");
            }

            [Theory]
            [InlineData("Foo", "Foo")]
            [InlineData(null, null)]
            [InlineData("", "")]
            public void Should_Not_Throw_If_ParamName_Is_The_Same(string actualParamName, string expectedParamName)
            {
                // Given
                var exception = new ArgumentOutOfRangeException(actualParamName);

                // When
                exception.IsArgumentOutOfRangeException(expectedParamName);

                // Then
            }
        }

        public sealed class TheIsInvalidOperationExceptionMethod
        {
            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Null()
            {
                // Given
                const Exception exception = null;

                // When
                var result = Record.Exception(() => exception.IsInvalidOperationException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.InvalidOperationException' but no exception was thrown.");
            }

            [Fact]
            public void Should_Throw_If_Exception_Type_Is_Not_InvalidOperationException()
            {
                // Given
                var exception = new Exception("Foo");

                // When
                var result = Record.Exception(() => exception.IsInvalidOperationException("Foo"));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe("Expected exception of type 'System.InvalidOperationException' but was 'System.Exception'.");
            }

            [Fact]
            public void Should_Not_Throw_If_Exception_Type_Is_InvalidOperationException()
            {
                // Given
                var exception = new InvalidOperationException("Foo");

                // When
                exception.IsInvalidOperationException("Foo");

                // Then
            }

            [Theory]
            [InlineData("Foo", "Bar")]
            [InlineData("Foo", null)]
            public void Should_Throw_If_Message_Is_Different(string actualMessage, string expectedMessage)
            {
                // Given
                var exception = new InvalidOperationException(actualMessage);

                // When
                var result = Record.Exception(() => exception.IsInvalidOperationException(expectedMessage));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldBe($"Expected exception message to be '{expectedMessage}' but was '{actualMessage}'.");
            }

            [Theory]
            [InlineData("Foo", "Foo")]
            [InlineData("", "")]
            public void Should_Not_Throw_If_Message_Is_The_Same(string actualMessage, string expectedMessage)
            {
                // Given
                var exception = new InvalidOperationException(actualMessage);

                // When
                exception.IsInvalidOperationException(expectedMessage);

                // Then
            }
        }
    }
}