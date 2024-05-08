namespace Cake.Issues.Tests
{
    using System.Text;

    public sealed class ByteArrayExtensionsTests
    {
        public sealed class TheRemovePreambleExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const byte[] value = null;

                // When
                var result = Record.Exception(() => value.RemovePreamble());

                // Then
                result.IsArgumentNullException("value");
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Remove_Preamble(string value)
            {
                // Given
                var encoding = Encoding.UTF8;
                var preamble = encoding.GetPreamble();
                var valueWithoutPreamble = value.ToByteArray(encoding);
                var valueWithPreamble = preamble.Concat(valueWithoutPreamble).ToArray();

                // When
                var result = valueWithPreamble.RemovePreamble();

                // Then
                result.ShouldBe(valueWithoutPreamble);
            }
        }

        public sealed class TheRemovePreambleWithEncodingExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const byte[] value = null;
                var encoding = Encoding.UTF32;

                // When
                var result = Record.Exception(() => value.RemovePreamble(encoding));

                // Then
                result.IsArgumentNullException("value");
            }

            [Fact]
            public void Should_Throw_If_Encoding_Is_Null()
            {
                // Given
                var encoding = Encoding.UTF32;
                const string stringValue = "foo🐱bar";
                var byteArrayValue = stringValue.ToByteArray(encoding);

                // When
                var result = Record.Exception(() => byteArrayValue.RemovePreamble(null));

                // Then
                result.IsArgumentNullException("encoding");
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Remove_Preamble(string value)
            {
                // Given
                var encoding = Encoding.UTF32;
                var preamble = encoding.GetPreamble();
                var valueWithoutPreamble = value.ToByteArray(encoding);
                var valueWithPreamble = preamble.Concat(valueWithoutPreamble).ToArray();

                // When
                var result = valueWithPreamble.RemovePreamble(encoding);

                // Then
                result.ShouldBe(valueWithoutPreamble);
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Return_Correct_Value_If_Encoding_Has_Preamble_But_Value_Not(string value)
            {
                // Given
                var encoding = Encoding.UTF32;
                var valueWithoutPreamble = value.ToByteArray(encoding);

                // When
                var result = valueWithoutPreamble.RemovePreamble(encoding);

                // Then
                result.ShouldBe(valueWithoutPreamble);
            }

            [Theory]
            [InlineData("foo")]
            [InlineData("")]
            public void Should_Return_Correct_Value_If_Encoding_Does_Not_Have_Preamble(string value)
            {
                // Given
                var encoding = Encoding.ASCII;
                var preamble = encoding.GetPreamble();
                var valueWithoutPreamble = value.ToByteArray(encoding);
                var valueWithPreamble = preamble.Concat(valueWithoutPreamble).ToArray();

                // When
                var result = valueWithPreamble.RemovePreamble(encoding);

                // Then
                result.ShouldBe(valueWithoutPreamble);
            }
        }

        public sealed class TheToStringUsingEncodingExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const byte[] value = null;

                // When
                var result = Record.Exception(() => value.ToStringUsingEncoding());

                // Then
                result.IsArgumentNullException("value");
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Return_Correct_Value_Without_Preamble(string value)
            {
                // Given
                var byteArrayValue = value.ToByteArray();

                // When
                var result = byteArrayValue.ToStringUsingEncoding();

                // Then
                result.ShouldBe(value);
            }
        }

        public sealed class TheToStringUsingEncodingWithSkipPreambleExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const byte[] value = null;

                // When
                var result = Record.Exception(() => value.ToStringUsingEncoding(true));

                // Then
                result.IsArgumentNullException("value");
            }

            [Fact]
            public void Should_Ignore_If_Preamble_Should_Be_Skipped_But_No_Preamble_Passed()
            {
                // Given
                var stringValue = "foo🐱bar";
                var byteArrayValue = stringValue.ToByteArray();

                // When
                var result = byteArrayValue.ToStringUsingEncoding(true);

                // Then
                result.ShouldBe(stringValue);
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Return_Correct_Value_Without_Preamble(string value)
            {
                // Given
                var byteArrayValue = value.ToByteArray();

                // When
                var result = byteArrayValue.ToStringUsingEncoding(false);

                // Then
                result.ShouldBe(value);
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Return_Correct_Value_With_Preamble(string value)
            {
                // Given
                var preamble = Encoding.UTF8.GetPreamble();
                var byteArrayValue = preamble.Concat(value.ToByteArray()).ToArray();

                // When
                var result = byteArrayValue.ToStringUsingEncoding(true);

                // Then
                result.ShouldBe(value);
            }

            [Fact]
            public void Should_Return_Correct_Value_For_Empty_Byte_Array()
            {
                // Given
                var byteArrayValue = Array.Empty<byte>();

                // When
                var result = byteArrayValue.ToStringUsingEncoding(true);

                // Then
                result.ShouldBe(string.Empty);
            }
        }

        public sealed class TheToStringUsingEncodingWithEncodingAndSkipPreambleExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const byte[] value = null;
                var encoding = Encoding.UTF32;

                // When
                var result = Record.Exception(() => value.ToStringUsingEncoding(encoding, true));

                // Then
                result.IsArgumentNullException("value");
            }

            [Fact]
            public void Should_Ignore_If_Preamble_Should_Be_Skipped_But_No_Preamble_Passed()
            {
                // Given
                var encoding = Encoding.UTF32;
                var stringValue = "foo🐱bar";
                var byteArrayValue = stringValue.ToByteArray(encoding);

                // When
                var result = byteArrayValue.ToStringUsingEncoding(encoding, true);

                // Then
                result.ShouldBe(stringValue);
            }

            [Theory]
            [InlineData("foo")]
            [InlineData("")]
            public void Should_Return_Value_If_Preamble_Should_Be_Skipped_But_Encoding_Does_Not_Have_Preamble(string value)
            {
                // Given
                var byteArrayValue = value.ToByteArray();
                var encoding = Encoding.ASCII;

                // When
                var result = byteArrayValue.ToStringUsingEncoding(encoding, true);

                // Then
                result.ShouldBe(value);
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Return_Correct_Value_Without_Preamble(string value)
            {
                // Given
                var encoding = Encoding.UTF32;
                var byteArrayValue = value.ToByteArray(encoding);

                // When
                var result = byteArrayValue.ToStringUsingEncoding(encoding, false);

                // Then
                result.ShouldBe(value);
            }

            [Theory]
            [InlineData("foo🐱bar")]
            [InlineData("")]
            public void Should_Return_Correct_Value_With_Preamble(string value)
            {
                // Given
                var encoding = Encoding.UTF32;
                var preamble = encoding.GetPreamble();
                var byteArrayValue = preamble.Concat(value.ToByteArray(encoding)).ToArray();

                // When
                var result = byteArrayValue.ToStringUsingEncoding(encoding, true);

                // Then
                result.ShouldBe(value);
            }
        }

        public sealed class TheToByteArrayExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const string value = null;

                // When
                var result = Record.Exception(() => value.ToByteArray());

                // Then
                result.IsArgumentNullException("value");
            }

            [Theory]
            [InlineData("", new byte[] { })]
            [InlineData(" ", new byte[] { 32 })]
            [InlineData("foo", new byte[] { 102, 111, 111 })]
            [InlineData("foo🐱bar", new byte[] { 102, 111, 111, 240, 159, 144, 177, 98, 97, 114 })]
            public void Should_Return_Correct_Value(string value, byte[] expected)
            {
                // When
                var result = value.ToByteArray();

                // Then
                result.ShouldBe(expected);
            }
        }

        public sealed class TheToByteArrayWithEncodingExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const string value = null;
                var encoding = Encoding.UTF8;

                // When
                var result = Record.Exception(() => value.ToByteArray(encoding));

                // Then
                result.IsArgumentNullException("value");
            }

            [Fact]
            public void Should_Throw_If_Encoding_Is_Null()
            {
                // Given
                const string value = "foo";
                const Encoding encoding = null;

                // When
                var result = Record.Exception(() => value.ToByteArray(encoding));

                // Then
                result.IsArgumentNullException("encoding");
            }

            [Theory]
            [InlineData("", new byte[] { })]
            [InlineData(" ", new byte[] { 32 })]
            [InlineData("foo", new byte[] { 102, 111, 111 })]
            [InlineData("foo🐱bar", new byte[] { 102, 111, 111, 240, 159, 144, 177, 98, 97, 114 })]
            public void Should_Return_Correct_Value_With_UTF8_Encoding(string value, byte[] expected)
            {
                // When
                var result = value.ToByteArray(Encoding.UTF8);

                // Then
                result.ShouldBe(expected);
            }

            [Theory]
            [InlineData("", new byte[] { })]
            [InlineData(" ", new byte[] { 32 })]
            [InlineData("foo", new byte[] { 102, 111, 111 })]
            [InlineData("foo🐱bar", new byte[] { 102, 111, 111, 63, 63, 98, 97, 114 })]
            public void Should_Return_Correct_Value_With_ASCII_Encoding(string value, byte[] expected)
            {
                // When
                var result = value.ToByteArray(Encoding.ASCII);

                // Then
                result.ShouldBe(expected);
            }
        }
    }
}
