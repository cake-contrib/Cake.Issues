namespace Cake.Issues.MsBuild.Tests.LogFileFormat
{
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
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
                ICakeLog log = null;

                // When
                var result = Record.Exception(() => new BinaryLogFileFormat(log));

                // Then
                result.IsArgumentNullException("log");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Full_Log_Correct()
            {
                // Given
                var fixture = new MsBuildIssuesProviderFixture<BinaryLogFileFormat>("FullLog.binlog");
                fixture.RepositorySettings = new RepositorySettings(@"C:\projects\cake-issues-demo\");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(19);
                CheckIssue(
                    issues[0],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    13,
                    "CS0219",
                    300,
                    "Warning",
                    @"The variable 'foo' is assigned but its value is never used");
                CheckIssue(
                    issues[1],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    1,
                    "SA1652",
                    300,
                    "Warning",
                    @"Enable XML documentation output");
                CheckIssue(
                    issues[2],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    1,
                    "SA1633",
                    300,
                    "Warning",
                    @"The file header is missing or not located at the top of the file.");
                CheckIssue(
                    issues[3],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    1,
                    "SA1652",
                    300,
                    "Warning",
                    @"Enable XML documentation output");
                CheckIssue(
                    issues[4],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    1,
                    "SA1633",
                    300,
                    "Warning",
                    @"The file header is missing or not located at the top of the file.");
                CheckIssue(
                    issues[5],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    5,
                    "SA1028",
                    300,
                    "Warning",
                    @"Code must not contain trailing whitespace");
                CheckIssue(
                    issues[6],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    17,
                    "SA1028",
                    300,
                    "Warning",
                    @"Code must not contain trailing whitespace");
                CheckIssue(
                    issues[7],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    18,
                    "SA1028",
                    300,
                    "Warning",
                    @"Code must not contain trailing whitespace");
                CheckIssue(
                    issues[8],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    28,
                    "SA1028",
                    300,
                    "Warning",
                    @"Code must not contain trailing whitespace");
                CheckIssue(
                    issues[9],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Properties\AssemblyInfo.cs",
                    32,
                    "SA1028",
                    300,
                    "Warning",
                    @"Code must not contain trailing whitespace");
                CheckIssue(
                    issues[10],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    1,
                    "SA1200",
                    300,
                    "Warning",
                    @"Using directive must appear within a namespace declaration");
                CheckIssue(
                    issues[11],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    2,
                    "SA1200",
                    300,
                    "Warning",
                    @"Using directive must appear within a namespace declaration");
                CheckIssue(
                    issues[12],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    3,
                    "SA1200",
                    300,
                    "Warning",
                    @"Using directive must appear within a namespace declaration");
                CheckIssue(
                    issues[13],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    4,
                    "SA1200",
                    300,
                    "Warning",
                    @"Using directive must appear within a namespace declaration");
                CheckIssue(
                    issues[14],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    5,
                    "SA1200",
                    300,
                    "Warning",
                    @"Using directive must appear within a namespace declaration");
                CheckIssue(
                    issues[15],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    null,
                    null,
                    "CA2210",
                    300,
                    "Warning",
                    @"Microsoft.Design : Sign 'ClassLibrary1.dll' with a strong name key.");
                CheckIssue(
                    issues[16],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    null,
                    null,
                    "CA1014",
                    300,
                    "Warning",
                    @"Microsoft.Design : Mark 'ClassLibrary1.dll' with CLSCompliant(true) because it exposes externally visible types.");
                CheckIssue(
                    issues[17],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    12,
                    "CA1822",
                    300,
                    "Warning",
                    @"Microsoft.Performance : The 'this' parameter (or 'Me' in Visual Basic) of 'Class1.Foo()' is never used. Mark the member as static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.");
                CheckIssue(
                    issues[18],
                    @"src\ClassLibrary1\ClassLibrary1.csproj",
                    "ClassLibrary1",
                    @"src\ClassLibrary1\Class1.cs",
                    13,
                    "CA1804",
                    300,
                    "Warning",
                    @"Microsoft.Performance : 'Class1.Foo()' declares a variable, 'foo', of type 'string', which is never used or is only assigned to. Use this variable or remove it.");
            }

            private static void CheckIssue(
                IIssue issue,
                string projectFileRelativePath,
                string projectName,
                string affectedFileRelativePath,
                int? line,
                string rule,
                int priority,
                string priorityName,
                string message)
            {
                issue.ProviderType.ShouldBe("Cake.Issues.MsBuild.MsBuildIssuesProvider");
                issue.ProviderName.ShouldBe("MSBuild");

                if (issue.ProjectFileRelativePath == null)
                {
                    projectFileRelativePath.ShouldBeNull();
                }
                else
                {
                    issue.ProjectFileRelativePath.ToString().ShouldBe(new FilePath(projectFileRelativePath).ToString());
                    issue.ProjectFileRelativePath.IsRelative.ShouldBe(true, "Issue path is not relative");
                }

                issue.ProjectName.ShouldBe(projectName);

                if (issue.AffectedFileRelativePath == null)
                {
                    affectedFileRelativePath.ShouldBeNull();
                }
                else
                {
                    issue.AffectedFileRelativePath.ToString().ShouldBe(new FilePath(affectedFileRelativePath).ToString());
                    issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "Issue path is not relative");
                }

                issue.Line.ShouldBe(line);
                issue.Rule.ShouldBe(rule);
                issue.Priority.ShouldBe(priority);
                issue.PriorityName.ShouldBe(priorityName);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
