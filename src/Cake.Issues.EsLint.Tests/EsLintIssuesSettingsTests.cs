namespace Cake.Issues.EsLint.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.EsLint.LogFileFormat;
    using Cake.Issues.Testing;
    using Cake.Testing;
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
        }
    }
}