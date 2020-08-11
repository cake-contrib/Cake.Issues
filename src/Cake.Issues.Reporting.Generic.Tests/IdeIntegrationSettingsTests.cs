namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
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
                var endLineExpression = "endLine";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

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
                var endLineExpression = "endLine";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

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
                var endLineExpression = "endLine";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

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
                var endLineExpression = "endLine";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

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
                var endLineExpression = "endLine";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

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
                var endLineExpression = "endLine";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

                // Then
                result.IsArgumentOutOfRangeException("lineExpression");
            }

            [Fact]
            public void Should_Throw_If_EndLineExpression_Is_Null()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                string endLineExpression = null;

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

                // Then
                result.IsArgumentNullException("endLineExpression");
            }

            [Fact]
            public void Should_Throw_If_EndLineExpression_Is_Empty()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = string.Empty;

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

                // Then
                result.IsArgumentOutOfRangeException("endLineExpression");
            }

            [Fact]
            public void Should_Throw_If_EndLineExpression_Is_WhiteSpace()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = " ";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression));

                // Then
                result.IsArgumentOutOfRangeException("endLineExpression");
            }

            [Fact]
            public void Should_Return_Null_If_OpenInIdeCall_Is_Not_Set()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression);

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
                        OpenInIdeCall = "Foo{FilePath}Bar",
                    };
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression);

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
                        OpenInIdeCall = "Foo{Line}Bar",
                    };
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression);

                // Then
                result.ShouldBe("FoolineBar");
            }

            [Fact]
            public void Should_Replace_EndLine_Token()
            {
                // Given
                var ideIntegrationSettings =
                    new IdeIntegrationSettings
                    {
                        OpenInIdeCall = "Foo{EndLine}Bar",
                    };
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";

                // When
                var result = ideIntegrationSettings.GetOpenInIdeCall(filePathExpression, lineExpression, endLineExpression);

                // Then
                result.ShouldBe("FooendLineBar");
            }
        }
    }
}
