namespace Cake.Issues.Markdownlint.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Cake.Core.IO;
    using Cake.Issues.Markdownlint;
    using Cake.Testing;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class MarkdownlintIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;
                var format = new MarkdownlintLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    MarkdownlintIssuesSettings.FromFilePath(logFilePath, format));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_Format_For_LogFilePath_Is_Null()
            {
                // Given
                var logFilePath = @"c:\markdownlint.log";
                ILogFileFormat format = null;

                // When
                var result = Record.Exception(() =>
                    MarkdownlintIssuesSettings.FromFilePath(logFilePath, format));

                // Then
                result.IsArgumentNullException("format");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given
                string logFileContent = null;
                var format = new MarkdownlintLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    MarkdownlintIssuesSettings.FromContent(logFileContent, format));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Empty()
            {
                // Given
                var logFileContent = string.Empty;
                var format = new MarkdownlintLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    MarkdownlintIssuesSettings.FromContent(logFileContent, format));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_WhiteSpace()
            {
                // Given
                var logFileContent = " ";
                var format = new MarkdownlintLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    MarkdownlintIssuesSettings.FromContent(logFileContent, format));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_Format_For_LogFileContent_Is_Null()
            {
                // Given
                var logFileContent = "foo";
                ILogFileFormat format = null;

                // When
                var result = Record.Exception(() =>
                    MarkdownlintIssuesSettings.FromContent(logFileContent, format));

                // Then
                result.IsArgumentNullException("format");
            }

            [Fact]
            public void Should_Set_Property_Values_Passed_To_Constructor()
            {
                // Given
                const string logFileContent = "foo";
                var format = new MarkdownlintLogFileFormat(new FakeLog());

                // When
                var settings = MarkdownlintIssuesSettings.FromContent(logFileContent, format);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Read_File_From_Disk()
            {
                var fileName = System.IO.Path.GetTempFileName();
                try
                {
                    // Given
                    var format = new MarkdownlintLogFileFormat(new FakeLog());
                    string expected;
                    using (var ms = new MemoryStream())
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Markdownlint.Tests.Testfiles.markdownlint.json"))
                    {
                        stream.CopyTo(ms);
                        var data = ms.ToArray();

                        using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            file.Write(data, 0, data.Length);
                        }

                        expected = ConvertFromUtf8(data);
                    }

                    // When
                    var settings =
                        MarkdownlintIssuesSettings.FromFilePath(fileName, format);

                    // Then
                    settings.LogFileContent.ShouldBe(expected);
                }
                finally
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }

            private static string ConvertFromUtf8(byte[] bytes)
            {
                var enc = new UTF8Encoding(true);
                var preamble = enc.GetPreamble();

                if (preamble.Where((p, i) => p != bytes[i]).Any())
                {
                    throw new ArgumentException("Not utf8-BOM");
                }

                return enc.GetString(bytes.Skip(preamble.Length).ToArray());
            }
        }
    }
}
