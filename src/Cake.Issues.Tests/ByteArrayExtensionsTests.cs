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
                var byteArrayValue = Encoding.UTF8.GetBytes(stringValue);

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
                var byteArrayValue = Encoding.UTF8.GetBytes(stringValue);

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
                var byteArrayValue = preamble.Concat(Encoding.UTF8.GetBytes(stringValue)).ToArray();

                // When
                var result = byteArrayValue.Utf8ToString(true);

                // Then
                result.ShouldBe(stringValue);
            }
        }
    }
}
