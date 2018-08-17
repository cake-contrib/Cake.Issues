namespace Cake.Issues.EsLint.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.EsLint.LogFileFormat;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class EsLintIssuesSettingsTests
    {
        public sealed class TheEsLintIssuesSettingsCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;
                var format = new JsonLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new EsLintIssuesSettings(logFilePath, format));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_Format_For_LogFilePath_Is_Null()
            {
                // Given
                BaseEsLintLogFileFormat format = null;

                using (var tempFile = new ResourceTempFile("Cake.Issues.EsLint.Tests.Testfiles.JsonLogFileFormat.jsonFormatWindows.json"))
                {
                    // When
                    var result = Record.Exception(() =>
                        new EsLintIssuesSettings(tempFile.FileName, format));

                    // Then
                    result.IsArgumentNullException("format");
                }
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Null()
            {
                // Given
                byte[] logFileContent = null;
                var format = new JsonLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new EsLintIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();
                var format = new JsonLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new EsLintIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_Format_For_LogFileContent_Is_Null()
            {
                // Given
                var logFileContent = "foo".ToByteArray();
                BaseEsLintLogFileFormat format = null;

                // When
                var result = Record.Exception(() =>
                    new EsLintIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentNullException("format");
            }

            [Fact]
            public void Should_Set_LogContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();
                var format = new JsonLogFileFormat(new FakeLog());

                // When
                var settings = new EsLintIssuesSettings(logFileContent, format);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogContent_From_LogFilePath()
            {
                // Given
                var format = new JsonLogFileFormat(new FakeLog());
                using (var tempFile = new ResourceTempFile("Cake.Issues.EsLint.Tests.Testfiles.JsonLogFileFormat.jsonFormatWindows.json"))
                {
                    // When
                    var settings = new EsLintIssuesSettings(tempFile.FileName, format);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }
        }
    }
}