﻿namespace Cake.Issues.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Xunit;

    public sealed class IIssueComparerTests
    {
        public sealed class TheCtorWithCompareOnlyPersistentPropertiesSetToFalse
        {
            [Fact]
            public void Should_Return_False_If_First_Issue_Is_Null()
            {
                // Given
                IIssue issue1 = null;
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_Second_Issue_Is_Null()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                IIssue issue2 = null;

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_ProjectFileRelativePath_Is_Different(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_ProjectName_Is_Different(string projectName1, string projectName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_AffectedFileRelativePath_Is_Different(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData(1, 2)]
            [InlineData(1, null)]
            [InlineData(null, 1)]
            [InlineData(int.MaxValue, 1)]
            [InlineData(1, int.MaxValue)]
            public void Should_Return_False_If_Line_Is_Different(int? line1, int? line2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_MessageText_Is_Different()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message1", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message2", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_MessageHtml_Is_Different(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_MessageMarkdown_Is_Different(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData(1, 2)]
            [InlineData(1, null)]
            [InlineData(null, 1)]
            [InlineData(int.MinValue, 0)]
            [InlineData(int.MaxValue, 0)]
            public void Should_Return_False_If_Priority_Is_Different(int? priority1, int? priority2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority1, "Foo")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority2, "Foo")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_PriorityName_Is_Different(string priorityName1, string priorityName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_Rule_Is_Different(string rule1, string rule2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("http://foo", "http://bar")]
            [InlineData("http://foo", null)]
            [InlineData(null, "http://foo")]
            public void Should_Return_False_If_RuleUlr_Is_Different(string ruleUrl1, string ruleUrl2)
            {
                // Given
                var issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl1))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl1));
                }

                var issue1 = issueBuilder.Create();

                issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl2))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl2));
                }

                var issue2 = issueBuilder.Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_ProviderType_Is_Different()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType1", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType2", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_ProviderName_Is_Different()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName1")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName2")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_True_If_Same_Reference()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 = issue1;

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_Both_Are_Null()
            {
                // Given
                IIssue issue1 = null;
                IIssue issue2 = null;

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_Properties_Are_The_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("foo", "foo/")]
            [InlineData("foo/", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_ProjectFileRelativePath_Is_Same(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_ProjectName_Is_Same(string projectName1, string projectName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("foo", "foo/")]
            [InlineData("foo/", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_AffectedFileRelativePath_Is_Same(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData(1, 1)]
            [InlineData(null, null)]
            [InlineData(int.MaxValue, int.MaxValue)]
            public void Should_Return_True_If_Line_Is_Same(int? line1, int? line2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_MessageText_Is_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("messageText", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("messageText", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_MessageHtml_Is_Same(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_MessageMarkdown_Is_Same(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData(1, 1)]
            [InlineData(null, null)]
            [InlineData(0, 0)]
            [InlineData(int.MinValue, int.MinValue)]
            [InlineData(int.MaxValue, int.MaxValue)]
            public void Should_Return_True_If_Priority_Is_Same(int? priority1, int? priority2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority1, "Foo")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority2, "Foo")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_PriorityName_Is_Same(string priorityName1, string priorityName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_Rule_Is_Same(string rule1, string rule2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("http://foo", "http://foo")]
            [InlineData("http://foo", "http://Foo")]
            [InlineData(null, null)]
            public void Should_Return_True_If_RuleUlr_Is_Same(string ruleUrl1, string ruleUrl2)
            {
                // Given
                var issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl1))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl1));
                }

                var issue1 = issueBuilder.Create();

                issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl2))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl2));
                }

                var issue2 = issueBuilder.Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_ProviderType_Is_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_ProviderName_Is_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Remove_Identical_Issues_From_List_Of_Issues()
            {
                // Given
                var issue1_1 =
                    IssueBuilder
                        .NewIssue("message1", "providerType1", "providerName1")
                        .Create();
                var issue1_2 =
                    IssueBuilder
                        .NewIssue("message1", "providerType1", "providerName1")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message2", "providerType2", "providerName2")
                        .Create();
                var issue3 =
                    IssueBuilder
                        .NewIssue("message3", "providerType3", "providerName3")
                        .Create();
                var issues1 = new List<IIssue> { issue1_1, issue2 };
                var issues2 = new List<IIssue> { issue1_2, issue3 };
                var comparer = new IIssueComparer();

                // When
                var result = issues1.Except(issues2, comparer);

                // Then
                result.Count().ShouldBe(1);
                result.ShouldContain(issue2);
            }

            private static void CompareIssues(IIssue issue1, IIssue issue2, bool expectedToBeEqual)
            {
                var comparer = new IIssueComparer(false);

                comparer.Equals(issue1, issue2).ShouldBe(expectedToBeEqual);

                if (expectedToBeEqual)
                {
                    comparer.GetHashCode(issue1).ShouldBe(comparer.GetHashCode(issue2));
                }
                else
                {
                    comparer.GetHashCode(issue1).ShouldNotBe(comparer.GetHashCode(issue2));
                }
            }
        }

        public sealed class TheCtorWithCompareOnlyPersistentPropertiesSetToTrue
        {
            [Fact]
            public void Should_Return_False_If_First_Issue_Is_Null()
            {
                // Given
                IIssue issue1 = null;
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_Second_Issue_Is_Null()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                IIssue issue2 = null;

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_ProjectName_Is_Different(string projectName1, string projectName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_MessageText_Is_Different()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message1", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message2", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_MessageHtml_Is_Different(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_MessageMarkdown_Is_Different(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData(1, 2)]
            [InlineData(1, null)]
            [InlineData(null, 1)]
            [InlineData(int.MinValue, 0)]
            [InlineData(int.MaxValue, 0)]
            public void Should_Return_False_If_Priority_Is_Different(int? priority1, int? priority2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority1, "Foo")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority2, "Foo")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_PriorityName_Is_Different(string priorityName1, string priorityName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_False_If_Rule_Is_Different(string rule1, string rule2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Theory]
            [InlineData("http://foo", "http://bar")]
            [InlineData("http://foo", null)]
            [InlineData(null, "http://foo")]
            public void Should_Return_False_If_RuleUlr_Is_Different(string ruleUrl1, string ruleUrl2)
            {
                // Given
                var issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl1))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl1));
                }

                var issue1 = issueBuilder.Create();

                issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl2))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl2));
                }

                var issue2 = issueBuilder.Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_ProviderType_Is_Different()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType1", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType2", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_False_If_ProviderName_Is_Different()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName1")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName2")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, false);
            }

            [Fact]
            public void Should_Return_True_If_Same_Reference()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 = issue1;

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_Both_Are_Null()
            {
                // Given
                IIssue issue1 = null;
                IIssue issue2 = null;

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_Properties_Are_The_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_True_If_ProjectFileRelativePath_Is_Different(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("foo", "foo/")]
            [InlineData("foo/", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_ProjectFileRelativePath_Is_Same(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_ProjectName_Is_Same(string projectName1, string projectName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InProjectOfName(projectName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "bar")]
            [InlineData("foo", "Foo")]
            [InlineData("foo", null)]
            [InlineData(null, "foo")]
            public void Should_Return_True_If_AffectedFileRelativePath_Is_Different(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("foo", "foo/")]
            [InlineData("foo/", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_AffectedFileRelativePath_Is_Same(string path1, string path2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(path2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData(1, 2)]
            [InlineData(1, null)]
            [InlineData(null, 1)]
            [InlineData(int.MaxValue, 1)]
            [InlineData(1, int.MaxValue)]
            public void Should_Return_True_If_Line_Is_Different(int? line1, int? line2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData(1, 1)]
            [InlineData(null, null)]
            [InlineData(int.MaxValue, int.MaxValue)]
            public void Should_Return_True_If_Line_Is_Same(int? line1, int? line2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("foo", line2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_MessageText_Is_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("messageText", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("messageText", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_MessageHtml_Is_Same(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInHtmlFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_MessageMarkdown_Is_Same(string message1, string message2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithMessageInMarkdownFormat(message2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData(1, 1)]
            [InlineData(null, null)]
            [InlineData(0, 0)]
            [InlineData(int.MinValue, int.MinValue)]
            [InlineData(int.MaxValue, int.MaxValue)]
            public void Should_Return_True_If_Priority_Is_Same(int? priority1, int? priority2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority1, "Foo")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority2, "Foo")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_PriorityName_Is_Same(string priorityName1, string priorityName2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(42, priorityName2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Should_Return_True_If_Rule_Is_Same(string rule1, string rule2)
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule1)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(rule2)
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Theory]
            [InlineData("http://foo", "http://foo")]
            [InlineData("http://foo", "http://Foo")]
            [InlineData(null, null)]
            public void Should_Return_True_If_RuleUlr_Is_Same(string ruleUrl1, string ruleUrl2)
            {
                // Given
                var issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl1))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl1));
                }

                var issue1 = issueBuilder.Create();

                issueBuilder =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName");
                if (!string.IsNullOrEmpty(ruleUrl2))
                {
                    issueBuilder =
                        issueBuilder
                            .OfRule("foo", new Uri(ruleUrl2));
                }

                var issue2 = issueBuilder.Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_ProviderType_Is_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Return_True_If_ProviderName_Is_Same()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .Create();

                // When / Then
                CompareIssues(issue1, issue2, true);
            }

            [Fact]
            public void Should_Remove_Identical_Issues_From_List_Of_Issues()
            {
                // Given
                var issue1_1 =
                    IssueBuilder
                        .NewIssue("message1", "providerType1", "providerName1")
                        .InFile("foo.cs", 10)
                        .Create();
                var issue1_2 =
                    IssueBuilder
                        .NewIssue("message1", "providerType1", "providerName1")
                        .InFile("foo.cs", 20)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("message2", "providerType2", "providerName2")
                        .Create();
                var issue3 =
                    IssueBuilder
                        .NewIssue("message3", "providerType3", "providerName3")
                        .Create();
                var issues1 = new List<IIssue> { issue1_1, issue2 };
                var issues2 = new List<IIssue> { issue1_2, issue3 };
                var comparer = new IIssueComparer(true);

                // When
                var result = issues1.Except(issues2, comparer);

                // Then
                result.Count().ShouldBe(1);
                result.ShouldContain(issue2);
            }

            private static void CompareIssues(IIssue issue1, IIssue issue2, bool expectedToBeEqual)
            {
                var comparer = new IIssueComparer(true);

                comparer.Equals(issue1, issue2).ShouldBe(expectedToBeEqual);

                if (expectedToBeEqual)
                {
                    comparer.GetHashCode(issue1).ShouldBe(comparer.GetHashCode(issue2));
                }
                else
                {
                    comparer.GetHashCode(issue1).ShouldNotBe(comparer.GetHashCode(issue2));
                }
            }
        }
    }
}