namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class HtmlDxDataGridColumnDescriptionTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Id_Is_Null()
            {
                // Given
                string id = null;
                object valueRetriever(IIssue issue)
                {
                    return true;
                }

                // When
                var result = Record.Exception(() => new HtmlDxDataGridColumnDescription(id, valueRetriever));

                // Then
                result.IsArgumentNullException("id");
            }

            [Fact]
            public void Should_Throw_If_Id_Is_Empty()
            {
                // Given
                var id = string.Empty;
                object valueRetriever(IIssue issue)
                {
                    return true;
                }

                // When
                var result = Record.Exception(() => new HtmlDxDataGridColumnDescription(id, valueRetriever));

                // Then
                result.IsArgumentOutOfRangeException("id");
            }

            [Fact]
            public void Should_Throw_If_Id_Is_Whitespace()
            {
                // Given
                var id = " ";
                object valueRetriever(IIssue issue)
                {
                    return true;
                }

                // When
                var result = Record.Exception(() => new HtmlDxDataGridColumnDescription(id, valueRetriever));

                // Then
                result.IsArgumentOutOfRangeException("id");
            }

            [Fact]
            public void Should_Throw_If_ValueRetriever_Is_Null()
            {
                // Given
                var id = "foo";
                Func<IIssue, object> valueRetriever = null;

                // When
                var result = Record.Exception(() => new HtmlDxDataGridColumnDescription(id, valueRetriever));

                // Then
                result.IsArgumentNullException("valueRetriever");
            }

            [Fact]
            public void Should_Assign_Id()
            {
                // Given
                var id = "foo";
                object valueRetriever(IIssue issue)
                {
                    return true;
                }

                // When
                var result = new HtmlDxDataGridColumnDescription(id, valueRetriever);

                // Then
                result.Id.ShouldBe(id);
            }

            [Fact]
            public void Should_Assign_ValueRetriever()
            {
                // Given
                var id = "foo";
                object valueRetriever(IIssue issue)
                {
                    return true;
                }

                // When
                var result = new HtmlDxDataGridColumnDescription(id, valueRetriever);

                // Then
                result.ValueRetriever.ShouldBe(valueRetriever);
            }
        }
    }
}
