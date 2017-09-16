namespace Cake.Issues.InspectCode.Tests
{
    using System.IO;
    using System.Text;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class InspectCodeIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => InspectCodeIssuesSettings.FromFilePath(null));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => InspectCodeIssuesSettings.FromContent(null));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => InspectCodeIssuesSettings.FromContent(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => InspectCodeIssuesSettings.FromContent(" "));

                // Then
                result.IsArgumentOutOfRangeException("logFileContent");
            }

            [Fact]
            public void Should_Set_Property_Values_Passed_To_Constructor()
            {
                // Given
                const string logFileContent = "foo";

                // When
                var settings = InspectCodeIssuesSettings.FromContent(logFileContent);

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
                    string expected;
                    using (var ms = new MemoryStream())
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.InspectCode.Tests.Testfiles.inspectcode.xml"))
                    {
                        stream.CopyTo(ms);
                        var data = ms.ToArray();

                        using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            file.Write(data, 0, data.Length);
                        }

                        expected = Encoding.UTF8.GetString(data, 0, data.Length);
                    }

                    // When
                    var settings = InspectCodeIssuesSettings.FromFilePath(new FilePath(fileName));

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
