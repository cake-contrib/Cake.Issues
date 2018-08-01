namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using Shouldly;
    using Xunit;

    public sealed class HtmlDxDataGridTemplateTests
    {
        public sealed class TheTitleOption
        {
            [Fact]
            public void Should_Set_Title()
            {
                // Given
                var title = "Foo";
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);
                fixture.GenericIssueReportFormatSettings
                    .WithOption(HtmlDxDataGridOption.Title, title);

                // When
                var result = fixture.CreateReport(new List<IIssue>());

                // Then
                var doc = new HtmlDocument();
                doc.LoadHtml(result);
                var titleElements = doc.DocumentNode.Descendants("title");
                titleElements.ShouldHaveSingleItem();
                titleElements.Single().InnerText.ShouldBe(title);
            }

            [Fact]
            public void Should_Set_Heading()
            {
                // Given
                var title = "Foo";
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);
                fixture.GenericIssueReportFormatSettings
                    .WithOption(HtmlDxDataGridOption.Title, title);

                // When
                var result = fixture.CreateReport(new List<IIssue>());

                // Then
                var doc = new HtmlDocument();
                doc.LoadHtml(result);
                var headingElements = doc.DocumentNode.Descendants("h1");
                headingElements.ShouldHaveSingleItem();
                headingElements.Single().InnerText.ShouldBe(title);
            }
        }

        public sealed class TheThemeOption
        {
            [Theory]
            [InlineData(DevExtremeTheme.Light)]
            [InlineData(DevExtremeTheme.Dark)]
            [InlineData(DevExtremeTheme.Contrast)]
            [InlineData(DevExtremeTheme.Carmine)]
            [InlineData(DevExtremeTheme.DarkMoon)]
            [InlineData(DevExtremeTheme.SoftBlue)]
            [InlineData(DevExtremeTheme.DarkViolet)]
            [InlineData(DevExtremeTheme.GreenMist)]
            [InlineData(DevExtremeTheme.LightCompact)]
            [InlineData(DevExtremeTheme.DarkCompact)]
            [InlineData(DevExtremeTheme.ContrastCompact)]
            [InlineData(DevExtremeTheme.MaterialBlueLight)]
            [InlineData(DevExtremeTheme.MaterialLimeLight)]
            [InlineData(DevExtremeTheme.MaterialOrangeLight)]
            [InlineData(DevExtremeTheme.MaterialPurpleLight)]
            [InlineData(DevExtremeTheme.MaterialTealLight)]
            public void Should_Set_Theme(DevExtremeTheme theme)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);
                fixture.GenericIssueReportFormatSettings
                    .WithOption(HtmlDxDataGridOption.Theme, theme);

                // When
                var result = fixture.CreateReport(new List<IIssue>());

                // Then
                var doc = new HtmlDocument();
                doc.LoadHtml(result);
                var stylesheetElements = doc.DocumentNode.SelectNodes("//link[@rel='stylesheet']");
                stylesheetElements.Count().ShouldBe(2);
                stylesheetElements.ShouldContain(x => x.Attributes["href"].Value.EndsWith(DevExtremeThemeExtensions.GetCssFileName(theme)));
            }
        }

        public sealed class TheShowHeaderOption
        {
            [Fact]
            public void Should_Show_Header_If_True()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);
                fixture.GenericIssueReportFormatSettings
                    .WithOption(HtmlDxDataGridOption.ShowHeader, true);

                // When
                var result = fixture.CreateReport(new List<IIssue>());

                // Then
                var doc = new HtmlDocument();
                doc.LoadHtml(result);
                var headingElements = doc.DocumentNode.Descendants("h1");
                headingElements.ShouldHaveSingleItem();
            }

            [Fact]
            public void Should_Not_Show_Header_If_False()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);
                fixture.GenericIssueReportFormatSettings
                    .WithOption(HtmlDxDataGridOption.ShowHeader, false);

                // When
                var result = fixture.CreateReport(new List<IIssue>());

                // Then
                var doc = new HtmlDocument();
                doc.LoadHtml(result);
                var headingElements = doc.DocumentNode.Descendants("h1");
                headingElements.ShouldBeEmpty();
            }
        }

        public sealed class TheAdditionalColumnsOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);
                fixture.GenericIssueReportFormatSettings
                    .WithOption(
                        HtmlDxDataGridOption.AdditionalColumns,
                        new List<HtmlDxDataGridColumnDescription>
                        {
                            new HtmlDxDataGridColumnDescription("MyCustomColumn", x => { return "Foo"; })
                            {
                                Caption = "Custom Value",
                            }
                        });

                // When
                var result =
                    fixture.CreateReport(
                        new List<IIssue>
                        {
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        });

                // Then
                // Currently only checks if genertions failed or not without checking actual output.
                result.ShouldNotBeNull();
            }
        }
    }
}
