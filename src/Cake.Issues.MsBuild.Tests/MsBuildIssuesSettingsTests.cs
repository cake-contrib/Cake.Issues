namespace Cake.Issues.MsBuild.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Testing;
    using Testing;
    using Xunit;

    public sealed class MsBuildIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;
                var format = new XmlFileLoggerFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new MsBuildIssuesSettings(logFilePath, format));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Null()
            {
                // Given
                byte[] logFileContent = null;
                var format = new XmlFileLoggerFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new MsBuildIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();
                var format = new XmlFileLoggerFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new MsBuildIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentException("logFileContent");
            }
        }
    }
}
