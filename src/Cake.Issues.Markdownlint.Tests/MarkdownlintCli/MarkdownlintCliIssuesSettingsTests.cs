namespace Cake.Issues.Markdownlint.Tests.MarkdownlintCli
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Cake.Issues.Markdownlint.MarkdownlintCli;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class MarkdownlintCliIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    MarkdownlintCliIssuesSettings.FromFilePath(null));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    MarkdownlintCliIssuesSettings.FromContent(null));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() =>
                    MarkdownlintCliIssuesSettings.FromContent(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() =>
                    MarkdownlintCliIssuesSettings.FromContent(" "));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Set_Property_Values_Passed_To_Constructor()
            {
                // Given
                const string logFileContent = "foo";

                // When
                var settings = MarkdownlintCliIssuesSettings.FromContent(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Read_File_From_Disk()
            {
                var fileName = Path.GetTempFileName();
                try
                {
                    // Given
                    string expected;
                    using (var ms = new MemoryStream())
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Markdownlint.Tests.Testfiles.markdownlint-cli-0-8-1.log"))
                    {
                        stream.CopyTo(ms);
                        var data = ms.ToArray();

                        using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            file.Write(data, 0, data.Length);
                        }

                        expected = Encoding.UTF8.GetString(data);
                    }

                    // When
                    var settings =
                        MarkdownlintCliIssuesSettings.FromFilePath(fileName);

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
        }
    }
}
