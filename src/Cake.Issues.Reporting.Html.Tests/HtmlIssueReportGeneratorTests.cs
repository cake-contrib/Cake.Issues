namespace Cake.Issues.Reporting.Html.Tests
{
    using System.Collections.Generic;
    using Cake.Testing;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class HtmlIssueReportGeneratorTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new HtmlIssueReportGenerator(
                        null,
                        HtmlIssueReportFormatSettings.FromContent("Foo")));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new HtmlIssueReportGenerator(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheInternalCreateReportMethod
        {
            [Fact]
            public void Should_Generate_Report()
            {
                // Given
                var fixture = new HtmlIssueReportFixture("<ul>@foreach(var issue in Model){<li>@issue.Message</li>}</ul>");
                var issues =
                    new List<IIssue>
                    {
                        new Issue(
                            @"src\Cake.Issues.Reporting.Html.Tests\Foo.cs",
                            10,
                            "Foo",
                            0,
                            "Foo",
                            "Foo"),
                        new Issue(
                            @"src\Cake.Issues.Reporting.Html.Tests\Foo.cs",
                            12,
                            "Bar",
                            0,
                            "Bar",
                            "Bar")
                        };
                var expectedResult =
                    @"<ul><li>Foo</li><li>Bar</li></ul>";

                // When
                var result = fixture.CreateReport(issues);

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
