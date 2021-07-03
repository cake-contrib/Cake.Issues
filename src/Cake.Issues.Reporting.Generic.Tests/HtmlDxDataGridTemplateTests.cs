namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using HtmlAgilityPack;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
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
                var titleElements = doc.DocumentNode.Descendants("title").ToList();
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
                var headingElements = doc.DocumentNode.Descendants("h1").ToList();
                headingElements.ShouldHaveSingleItem();
                headingElements.Single().InnerText.ShouldBe(title);
            }
        }

        public sealed class TheThemeOption
        {
            public static IEnumerable<object[]> DevExtremeThemes()
            {
                return
                    from object number in Enum.GetValues(typeof(DevExtremeTheme))
                    select new[] { number };
            }

            [Theory]
            [MemberData(nameof(DevExtremeThemes))]
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
                stylesheetElements.Count.ShouldBe(2);
                stylesheetElements.ShouldContain(x => x.Attributes["href"].Value.EndsWith(theme.GetCssFileName()));
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

        public sealed class TheEnableSearchingOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.EnableSearching, value));
            }
        }

        public sealed class TheEnableGroupingOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.EnableGrouping, value));
            }
        }

        public sealed class TheEnableFilteringOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.EnableFiltering, value));
            }
        }

        public sealed class TheProviderTypeVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProviderTypeVisible, value));
            }
        }

        public sealed class TheProviderTypeSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProviderTypeSortOrder, value));
            }
        }

        public sealed class TheProviderNameVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProviderNameVisible, value));
            }
        }

        public sealed class TheProviderNameSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProviderNameSortOrder, value));
            }
        }

        public sealed class TheRunVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.RunVisible, value));
            }
        }

        public sealed class TheRunSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.RunSortOrder, value));
            }
        }

        public sealed class ThePriorityVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.PriorityVisible, value));
            }
        }

        public sealed class ThePrioritySortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.PrioritySortOrder, value));
            }
        }

        public sealed class ThePriorityNameVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.PriorityNameVisible, value));
            }
        }

        public sealed class ThePriorityNameSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.PriorityNameSortOrder, value));
            }
        }

        public sealed class TheProjectPathVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProjectPathVisible, value));
            }
        }

        public sealed class TheProjectPathSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProjectPathSortOrder, value));
            }
        }

        public sealed class TheProjectNameVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProjectNameVisible, value));
            }
        }

        public sealed class TheProjectNameSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.ProjectNameSortOrder, value));
            }
        }

        public sealed class TheFilePathVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.FilePathVisible, value));
            }
        }

        public sealed class TheFilePathSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.FilePathSortOrder, value));
            }
        }

        public sealed class TheFileDirectoryVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.FileDirectoryVisible, value));
            }
        }

        public sealed class TheFileDirectorySortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.FileDirectorySortOrder, value));
            }
        }

        public sealed class TheFileNameVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.FileNameVisible, value));
            }
        }

        public sealed class TheFileNameSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.FileNameSortOrder, value));
            }
        }

        public sealed class TheLineVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.LineVisible, value));
            }
        }

        public sealed class TheLineSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.LineSortOrder, value));
            }
        }

        public sealed class TheEndLineVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.EndLineVisible, value));
            }
        }

        public sealed class TheEndLineSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.EndLineSortOrder, value));
            }
        }

        public sealed class TheLocationVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.LocationVisible, value));
            }
        }

        public sealed class TheLocationSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.LocationSortOrder, value));
            }
        }

        public sealed class TheLRuleVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.RuleVisible, value));
            }
        }

        public sealed class TheRuleSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.RuleSortOrder, value));
            }
        }

        public sealed class TheLRuleUrlVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.RuleUrlVisible, value));
            }
        }

        public sealed class TheRuleUrlSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.RuleUrlSortOrder, value));
            }
        }

        public sealed class TheMessageVisibleOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.MessageVisible, value));
            }
        }

        public sealed class TheMessageSortOrderOption
        {
            [Theory]
            [InlineData(ColumnSortOrder.Ascending)]
            [InlineData(ColumnSortOrder.Descending)]
            public void Should_Not_Fail_On_Report_Creation(ColumnSortOrder value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(HtmlDxDataGridOption.MessageSortOrder, value));
            }
        }

        public sealed class TheGroupedColumnsOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.GroupedColumns,
                            new List<ReportColumn>
                            {
                                ReportColumn.ProjectName,
                                ReportColumn.FileDirectory,
                                ReportColumn.FileName,
                            }));
            }
        }

        public sealed class TheSortedColumnsOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.SortedColumns,
                            new List<ReportColumn>
                            {
                                ReportColumn.Rule,
                                ReportColumn.Message,
                            }));
            }
        }

        public sealed class TheAdditionalColumnsOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.AdditionalColumns,
                            new List<HtmlDxDataGridColumnDescription>
                            {
                                new HtmlDxDataGridColumnDescription("MyCustomColumn", x => "Foo")
                                {
                                    Caption = "Custom Value",
                                },
                            }));
            }
        }

        public sealed class TheJQueryLocationOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.JQueryLocation,
                            "https://foo/"));
            }
        }

        public sealed class TheJQueryVersionOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.JQueryVersion,
                            "1.0.0"));
            }
        }

        public sealed class TheDevExtremeLocationOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.DevExtremeLocation,
                            "https://foo/"));
            }
        }

        public sealed class TheDevExtremeVersionOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.DevExtremeVersion,
                            "19.1.3"));
            }
        }

        public sealed class TheIdeIntegrationSettingsOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.IdeIntegrationSettings,
                            new IdeIntegrationSettings()));
            }
        }

        public sealed class TheEnableExportingOption
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Not_Fail_On_Report_Creation(bool value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.EnableExporting,
                            value));
            }
        }

        public sealed class TheExportFileNameOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.ExportFileName,
                            "foo"));
            }
        }

        public sealed class TheExportFormatOption
        {
            [Theory]
            [InlineData(HtmlDxDataGridExportFormat.Excel)]
            [InlineData(HtmlDxDataGridExportFormat.Pdf)]
            public void Should_Not_Fail_On_Report_Creation(HtmlDxDataGridExportFormat value)
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings
                            .WithOption(
                                HtmlDxDataGridOption.EnableExporting,
                                true)
                            .WithOption(
                                HtmlDxDataGridOption.ExportFormat,
                                value));
            }
        }

        public sealed class TheExcelJsLocationOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.ExcelJsLocation,
                            "foo"));
            }
        }

        public sealed class TheExcelJsVersionOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.ExcelJsVersion,
                            "foo"));
            }
        }

        public sealed class TheFileSaverJsLocationOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.FileSaverJsLocation,
                            "foo"));
            }
        }

        public sealed class TheFileSaverJsVersionOption
        {
            [Fact]
            public void Should_Not_Fail_On_Report_Creation()
            {
                // Given
                var fixture = new GenericIssueReportFixture(GenericIssueReportTemplate.HtmlDxDataGrid);

                // When / Then
                fixture.TestReportCreation(
                    settings =>
                        settings.WithOption(
                            HtmlDxDataGridOption.FileSaverJsVersion,
                            "foo"));
            }
        }
    }
}