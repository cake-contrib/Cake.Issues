namespace Cake.Issues.DocFx.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Core.IO;
    using DocFx;
    using Shouldly;
    using Testing;
    using Xunit;

    public class DocFxSettingsTests
    {
        public sealed class TheDocFxSettingsCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    DocFxIssuesSettings.FromFilePath(null, @"c:\Source\Cake.Issues\docs"));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    DocFxIssuesSettings.FromContent(null, @"c:\Source\Cake.Issues\docs"));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() =>
                    DocFxIssuesSettings.FromContent(string.Empty, @"c:\Source\Cake.Issues\docs"));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() =>
                    DocFxIssuesSettings.FromContent(" ", @"c:\Source\Cake.Issues\docs"));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogFileContent()
            {
                // Given
                const string logFileContent = "foo";

                // When
                var settings =
                    DocFxIssuesSettings.FromContent(
                        logFileContent,
                        @"c:\Source\Cake.Issues\docs");

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_DocRootPath()
            {
                // Given
                DirectoryPath docRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var settings =
                    DocFxIssuesSettings.FromContent(
                        "foo",
                        docRootPath);

                // Then
                settings.DocRootPath.ShouldBe(docRootPath);
            }

            [Fact]
            public void Should_Read_File_From_Disk()
            {
                var fileName = System.IO.Path.GetTempFileName();
                try
                {
                    // Given
                    string expected;
                    using (var ms = new MemoryStream())
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.DocFx.Tests.Testfiles.docfx.json"))
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
                        DocFxIssuesSettings.FromFilePath(fileName, @"c:\Source\Cake.Issues\docs");

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
