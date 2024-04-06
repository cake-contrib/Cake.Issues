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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentOutOfRangeException("endLineExpression");
            }

            [Fact]
            public void Should_Throw_If_ColumnExpression_Is_Null()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                string columnExpression = null;
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentNullException("columnExpression");
            }

            [Fact]
            public void Should_Throw_If_ColumnExpression_Is_Empty()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = string.Empty;
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentOutOfRangeException("columnExpression");
            }

            [Fact]
            public void Should_Throw_If_ColumnExpression_Is_WhiteSpace()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = " ";
                var endColumnExpression = "endColumn";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentOutOfRangeException("columnExpression");
            }

            [Fact]
            public void Should_Throw_If_EndColumnExpression_Is_Null()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = "column";
                string endColumnExpression = null;

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentNullException("endColumnExpression");
            }

            [Fact]
            public void Should_Throw_If_EndColumnExpression_Is_Empty()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = "column";
                var endColumnExpression = string.Empty;

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentOutOfRangeException("endColumnExpression");
            }

            [Fact]
            public void Should_Throw_If_EndColumnExpression_Is_WhiteSpace()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = "column";
                var endColumnExpression = " ";

                // When
                var result = Record.Exception(() =>
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression));

                // Then
                result.IsArgumentOutOfRangeException("endColumnExpression");
            }

            [Fact]
            public void Should_Return_Null_If_OpenInIdeCall_Is_Not_Set()
            {
                // Given
                var ideIntegrationSettings = new IdeIntegrationSettings();
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result =
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression);

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result =
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression);

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result =
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression);

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
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result =
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression);

                // Then
                result.ShouldBe("FooendLineBar");
            }

            [Fact]
            public void Should_Replace_Column_Token()
            {
                // Given
                var ideIntegrationSettings =
                    new IdeIntegrationSettings
                    {
                        OpenInIdeCall = "Foo{Column}Bar",
                    };
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result =
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression);

                // Then
                result.ShouldBe("FoocolumnBar");
            }

            [Fact]
            public void Should_Replace_EndColumn_Token()
            {
                // Given
                var ideIntegrationSettings =
                    new IdeIntegrationSettings
                    {
                        OpenInIdeCall = "Foo{EndColumn}Bar",
                    };
                var filePathExpression = "file";
                var lineExpression = "line";
                var endLineExpression = "endLine";
                var columnExpression = "column";
                var endColumnExpression = "endColumn";

                // When
                var result =
                    ideIntegrationSettings.GetOpenInIdeCall(
                        filePathExpression,
                        lineExpression,
                        endLineExpression,
                        columnExpression,
                        endColumnExpression);

                // Then
                result.ShouldBe("FooendColumnBar");
            }
        }

        public sealed class TheForVisualStudioUsingTeamCityAddinMethod
        {
            [Fact]
            public void Should_Return_Settings()
            {
                // Given

                // When
                var settings = IdeIntegrationSettings.ForVisualStudioUsingTeamCityAddin();

                // Then
                settings.ShouldNotBeNull();
            }
        }
    }
}