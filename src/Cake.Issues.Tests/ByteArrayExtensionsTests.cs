namespace Cake.Issues.Tests
{
    using System;
    using System.Linq;
    using System.Text;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class ByteArrayExtensionsTests
    {
        public sealed class TheUtf8ToStringExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                byte[] value = null;

                // When
                var result = Record.Exception(() => value.Utf8ToString(true));

                // Then
                result.IsArgumentNullException("value");
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Empty()
            {
                // Given
                byte[] value = Array.Empty<byte>();

                // When
                var result = Record.Exception(() => value.Utf8ToString(true));

                // Then
                result.IsArgumentException("value");
            }

            [Fact]
            public void Should_Throw_If_BOM_Should_Be_Skipped_But_No_BOM_Passed()
            {
                // Given
                var stringValue = "foo🐱bar";
                var byteArrayValue = stringValue.ToByteArray();

                // When
                var result = Record.Exception(() => byteArrayValue.Utf8ToString(true));

                // Then
                result.IsArgumentException("value");
            }

            [Fact]
            public void Should_Return_Correct_Value_Without_BOM()
            {
                // Given
                var stringValue = "foo🐱bar";
                var byteArrayValue = stringValue.ToByteArray();

                // When
                var result = byteArrayValue.Utf8ToString(false);

                // Then
                result.ShouldBe(stringValue);
            }

            [Fact]
            public void Should_Return_Correct_Value_With_BOM()
            {
                // Given
                var stringValue = "foo🐱bar";
                var preamble = Encoding.UTF8.GetPreamble();
                var byteArrayValue = preamble.Concat(stringValue.ToByteArray()).ToArray();

                // When
                var result = byteArrayValue.Utf8ToString(true);

                // Then
                result.ShouldBe(stringValue);
            }
        }

        public sealed class TheToByteArrayExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                string value = null;

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
                string value = null;
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
                var value = "foo";
                Encoding encoding = null;

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
