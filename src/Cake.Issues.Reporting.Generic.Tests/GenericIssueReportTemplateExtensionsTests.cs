namespace Cake.Issues.Reporting.Generic.Tests
{
    public sealed class GenericIssueReportTemplateExtensionsTests
    {
        public sealed class TheGetTemplateResourceNameMethod
        {
            [Fact]
            public void Should_Return_ResourceName()
            {
                // Given
                var template = GenericIssueReportTemplate.HtmlDiagnostic;

                // When
                var result = template.GetTemplateResourceName();

                // Then
                result.ShouldBe("Diagnostic.cshtml");
            }
        }
    }
}
