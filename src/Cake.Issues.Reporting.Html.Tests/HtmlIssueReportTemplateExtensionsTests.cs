namespace Cake.Issues.Reporting.Html.Tests
{
    using Shouldly;
    using Xunit;

    public sealed class HtmlIssueReportTemplateExtensionsTests
    {
        public sealed class TheGetTemplateResourceNameMethod
        {
            [Fact]
            public void Should_Return_ResourceName()
            {
                // Given
                var template = HtmlIssueReportTemplate.Diagnostic;

                // When
                var result = template.GetTemplateResourceName();

                // Then
                result.ShouldBe("Diagnostic.cshtml");
            }
        }
    }
}
