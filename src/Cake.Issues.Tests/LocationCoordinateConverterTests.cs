namespace Cake.Issues.Tests;

using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

public sealed class LocationCoordinateConverterTests
{
    public sealed class TheLineColumnToOffsetMethod
    {
        [Fact]
        public void Should_Return_Null_For_Null_Content()
        {
            // Given
            string content = null;
            int line = 1;
            int column = 1;

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Return_Null_For_Empty_Content()
        {
            // Given
            var content = string.Empty;
            int line = 1;
            int column = 1;

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Return_Null_For_Invalid_Line()
        {
            // Given
            var content = "line1\nline2\nline3";
            int? line = null;
            int column = 1;

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Return_Zero_For_First_Line_First_Column()
        {
            // Given
            var content = "Hello World";
            int line = 1;
            int column = 1;

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBe(0);
        }

        [Fact]
        public void Should_Calculate_Offset_For_Single_Line()
        {
            // Given
            var content = "Hello World";
            int line = 1;
            int column = 7; // 'W' in "World"

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBe(6);
        }

        [Fact]
        public void Should_Calculate_Offset_For_Multiple_Lines()
        {
            // Given
            var content = "line1\nline2\nline3";
            int line = 2;
            int column = 3; // 'n' in "line2"

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBe(8); // 6 chars in "line1\n" + 2 for "li"
        }

        [Fact]
        public void Should_Handle_Windows_Line_Endings()
        {
            // Given
            var content = "line1\r\nline2\r\nline3";
            int line = 2;
            int column = 1;

            // When
            var result = LocationCoordinateConverter.LineColumnToOffset(content, line, column);

            // Then
            result.ShouldBe(7); // "line1\r\n" = 5 + 2 = 7
        }
    }

    public sealed class TheOffsetToLineColumnMethod
    {
        [Fact]
        public void Should_Return_Null_For_Null_Content()
        {
            // Given
            string content = null;
            int offset = 5;

            // When
            var result = LocationCoordinateConverter.OffsetToLineColumn(content, offset);

            // Then
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Return_Null_For_Empty_Content()
        {
            // Given
            var content = string.Empty;
            int offset = 5;

            // When
            var result = LocationCoordinateConverter.OffsetToLineColumn(content, offset);

            // Then
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Return_Null_For_Invalid_Offset()
        {
            // Given
            var content = "Hello World";
            int? offset = null;

            // When
            var result = LocationCoordinateConverter.OffsetToLineColumn(content, offset);

            // Then
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Return_First_Line_First_Column_For_Zero_Offset()
        {
            // Given
            var content = "Hello World";
            int offset = 0;

            // When
            var result = LocationCoordinateConverter.OffsetToLineColumn(content, offset);

            // Then
            result.ShouldNotBeNull();
            result.Value.Line.ShouldBe(1);
            result.Value.Column.ShouldBe(1);
        }

        [Fact]
        public void Should_Calculate_Line_Column_For_Single_Line()
        {
            // Given
            var content = "Hello World";
            int offset = 6; // 'W' in "World"

            // When
            var result = LocationCoordinateConverter.OffsetToLineColumn(content, offset);

            // Then
            result.ShouldNotBeNull();
            result.Value.Line.ShouldBe(1);
            result.Value.Column.ShouldBe(7);
        }

        [Fact]
        public void Should_Calculate_Line_Column_For_Multiple_Lines()
        {
            // Given
            var content = "line1\nline2\nline3";
            int offset = 8; // 'n' in "line2"

            // When
            var result = LocationCoordinateConverter.OffsetToLineColumn(content, offset);

            // Then
            result.ShouldNotBeNull();
            result.Value.Line.ShouldBe(2);
            result.Value.Column.ShouldBe(3);
        }
    }
}