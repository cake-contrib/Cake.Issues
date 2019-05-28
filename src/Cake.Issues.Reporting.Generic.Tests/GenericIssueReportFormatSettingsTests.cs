namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class GenericIssueReportFormatSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_TemplatePath_Is_Null()
            {
                // Given
                FilePath templatePath = null;

                // When
                var result = Record.Exception(() =>
                    GenericIssueReportFormatSettings.FromFilePath(templatePath));

                // Then
                result.IsArgumentNullException("templatePath");
            }

            [Fact]
            public void Should_Throw_If_TemplateContent_Is_Null()
            {
                // Given
                string templateContent = null;

                // When
                var result = Record.Exception(() =>
                    GenericIssueReportFormatSettings.FromContent(templateContent));

                // Then
                result.IsArgumentNullException("templateContent");
            }

            [Fact]
            public void Should_Throw_If_TemplateContent_Is_Empty()
            {
                // Given
                var templateContent = string.Empty;

                // When
                var result = Record.Exception(() =>
                    GenericIssueReportFormatSettings.FromContent(templateContent));

                // Then
                result.IsArgumentOutOfRangeException("templateContent");
            }

            [Fact]
            public void Should_Throw_If_TemplateContent_Is_WhiteSpace()
            {
                // Given
                var templateContent = " ";

                // When
                var result = Record.Exception(() =>
                    GenericIssueReportFormatSettings.FromContent(templateContent));

                // Then
                result.IsArgumentOutOfRangeException("templateContent");
            }

            [Fact]
            public void Should_Set_Template()
            {
                // Given
                var templateContent = "foo";

                // When
                var settings = GenericIssueReportFormatSettings.FromContent(templateContent);

                // Then
                settings.Template.ShouldBe(templateContent);
            }

            [Fact]
            public void Should_Set_Embedded_Template()
            {
                // Given
                var template = GenericIssueReportTemplate.HtmlDiagnostic;

                // When
                var settings = GenericIssueReportFormatSettings.FromEmbeddedTemplate(template);

                // Then
                settings.Template.ShouldNotBeNullOrWhiteSpace();
            }

            [Fact]
            public void Should_Read_Template_From_Disk()
            {
                var fileName = System.IO.Path.GetTempFileName();
                try
                {
                    // Given
                    string expected;
                    using (var ms = new MemoryStream())
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Reporting.Generic.Tests.Templates.TestTemplate.cshtml"))
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
                        GenericIssueReportFormatSettings.FromFilePath(fileName);

                    // Then
                    settings.Template.ShouldBe(expected);
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