namespace Cake.Issues.MsBuild.Tests.LogFileFormat
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Cake.Core.Diagnostics;
    using Cake.Issues.MsBuild.LogFileFormat;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class BinaryLogFileFormatTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;

                // When
                var result = Record.Exception(() => new BinaryLogFileFormat(log));

                // Then
                result.IsArgumentNullException("log");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [SkippableFact]
            public void Should_Read_Full_Log_Correct()
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given
                var fixture = new MsBuildIssuesProviderFixture<BinaryLogFileFormat>("FullLog.binlog")
                {
                    ReadIssuesSettings = new ReadIssuesSettings(@"C:\projects\cake-issues-demo\"),
                };

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(19);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "The variable 'foo' is assigned but its value is never used",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 13, 17)
                        .OfRule("CS0219")
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[1],
                    IssueBuilder.NewIssue(
                        "Enable XML documentation output",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 1, 1)
                        .OfRule("SA1652", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1652.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[2],
                    IssueBuilder.NewIssue(
                        "The file header is missing or not located at the top of the file.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 1, 1)
                        .OfRule("SA1633", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1633.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[3],
                    IssueBuilder.NewIssue(
                        "Enable XML documentation output",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 1, 1)
                        .OfRule("SA1652", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1652.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[4],
                    IssueBuilder.NewIssue(
                        "The file header is missing or not located at the top of the file.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 1, 1)
                        .OfRule("SA1633", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1633.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[5],
                    IssueBuilder.NewIssue(
                        "Code must not contain trailing whitespace",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 5, 77)
                        .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[6],
                    IssueBuilder.NewIssue(
                        "Code must not contain trailing whitespace",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 17, 76)
                        .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[7],
                    IssueBuilder.NewIssue(
                        "Code must not contain trailing whitespace",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 18, 74)
                        .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[8],
                    IssueBuilder.NewIssue(
                        "Code must not contain trailing whitespace",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 28, 22)
                        .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[9],
                    IssueBuilder.NewIssue(
                        "Code must not contain trailing whitespace",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 32, 84)
                        .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[10],
                    IssueBuilder.NewIssue(
                        "Using directive must appear within a namespace declaration",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 1, 1)
                        .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[11],
                    IssueBuilder.NewIssue(
                        "Using directive must appear within a namespace declaration",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 2, 1)
                        .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[12],
                    IssueBuilder.NewIssue(
                        "Using directive must appear within a namespace declaration",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 3, 1)
                        .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[13],
                    IssueBuilder.NewIssue(
                        "Using directive must appear within a namespace declaration",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 4, 1)
                        .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[14],
                    IssueBuilder.NewIssue(
                        "Using directive must appear within a namespace declaration",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 5, 1)
                        .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[15],
                    IssueBuilder.NewIssue(
                        "Microsoft.Design : Sign 'ClassLibrary1.dll' with a strong name key.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .OfRule("CA2210", new Uri("https://www.google.com/search?q=\"CA2210:\"+site:learn.microsoft.com"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[16],
                    IssueBuilder.NewIssue(
                        "Microsoft.Design : Mark 'ClassLibrary1.dll' with CLSCompliant(true) because it exposes externally visible types.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .OfRule("CA1014", new Uri("https://www.google.com/search?q=\"CA1014:\"+site:learn.microsoft.com"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[17],
                    IssueBuilder.NewIssue(
                        "Microsoft.Performance : The 'this' parameter (or 'Me' in Visual Basic) of 'Class1.Foo()' is never used. Mark the member as static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 12)
                        .OfRule("CA1822", new Uri("https://www.google.com/search?q=\"CA1822:\"+site:learn.microsoft.com"))
                        .WithPriority(IssuePriority.Warning));
                IssueChecker.Check(
                    issues[18],
                    IssueBuilder.NewIssue(
                        "Microsoft.Performance : 'Class1.Foo()' declares a variable, 'foo', of type 'string', which is never used or is only assigned to. Use this variable or remove it.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                        .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                        .InFile(@"src\ClassLibrary1\Class1.cs", 13)
                        .OfRule("CA1804", new Uri("https://www.google.com/search?q=\"CA1804:\"+site:learn.microsoft.com"))
                        .WithPriority(IssuePriority.Warning));
            }
        }
    }
}
