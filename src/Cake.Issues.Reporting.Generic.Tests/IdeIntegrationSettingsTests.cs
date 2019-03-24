namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IdeIntegrationSettingsTests
    {
        public sealed class TheGetOpenInIdeCallMethod
        {
            [Fact]
            public void Should_Throw_If_FilePathExpression_Is_Null()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                string filePathExpression = null;
                var lineExpression = "line";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression));

                // Then
                result.IsArgumentNullException("filePathExpression");
            }

            [Fact]
            public void Should_Throw_If_FilePathExpression_Is_Empty()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = string.Empty;
                var lineExpression = "line";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression));

                // Then
                result.IsArgumentOutOfRangeException("filePathExpression");
            }

            [Fact]
            public void Should_Throw_If_FilePathExpression_Is_WhiteSpace()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = " ";
                var lineExpression = "line";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression));

                // Then
                result.IsArgumentOutOfRangeException("filePathExpression");
            }

            [Fact]
            public void Should_Throw_If_LineExpression_Is_Null()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                string lineExpression = null;

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression));

                // Then
                result.IsArgumentNullException("lineExpression");
            }

            [Fact]
            public void Should_Throw_If_LineExpression_Is_Empty()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = string.Empty;

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression));

                // Then
                result.IsArgumentOutOfRangeException("lineExpression");
            }

            [Fact]
            public void Should_Throw_If_LineExpression_Is_WhiteSpace()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = " ";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression));

                // Then
                result.IsArgumentOutOfRangeException("lineExpression");
            }

            [Fact]
            public void Should_Return_Null_If_OpenInIdeCall_Is_Not_Set()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Replace_FilePath_Token()
            {
                // Given
                var ideIntegrationSettings =
                    new IdeIntegrationSettings
                    {
                        OpenInIdeCall = "Foo{FilePath}Bar"
                    };
                var filePathExpression = "file";
                var lineExpression = "line";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression);

                // Then
                result.ShouldBe("FoofileBar");
            }

            [Fact]
            public void Should_Replace_Line_Token()
            {
                // Given
                var ideIntegrationSettings =
                    new IdeIntegrationSettings
                    {
                        OpenInIdeCall = "Foo{Line}Bar"
                    };
                var filePathExpression = "file";
                var lineExpression = "line";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression);

                // Then
                result.ShouldBe("FoolineBar");
            }
        }
    }
}
