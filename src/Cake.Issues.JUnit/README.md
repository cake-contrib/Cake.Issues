# Cake.Issues.JUnit

This provider supports reading issues from JUnit XML format, which is used by various linters and tools.

## Supported Tools

The JUnit issue provider can read issues from any tool that outputs JUnit XML format, including:

- **cpplint**: C++ linter that can output JUnit format
- **commitlint-format-junit**: Commit message linter with JUnit output
- **kubeconform**: Kubernetes manifest validator with JUnit format
- **htmlhint**: HTML linter with JUnit format support
- Many other tools that support JUnit XML output

## Features

- Parses both single `testsuite` and `testsuites` root elements
- Extracts file paths from classname attributes or failure messages
- Supports multiple file path patterns:
  - `file:line:column` (e.g., `src/file.cpp:15:5`)
  - `file(line,column)` (e.g., `index.html(12,5)`)
  - `file line number` (e.g., `about.html line 8`)
- Maps test failures and errors to Cake.Issues format
- Handles system-out sections for additional context

## Usage

```csharp
#addin nuget:?package=Cake.Issues&version=x.x.x
#addin nuget:?package=Cake.Issues.JUnit&version=x.x.x

Task("ReadIssues")
    .Does(() =>
{
    var issues = ReadIssues(
        JUnitIssuesFromFilePath(@"c:\build\junit-results.xml"),
        @"c:\repo");
        
    Information($"{issues.Count()} issues found");
});
```

## JUnit XML Format

The provider expects standard JUnit XML format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<testsuite name="tool-name" tests="2" failures="1" errors="0" time="0.123">
  <testcase classname="src/file.cpp" name="rule-name" time="0.001">
    <failure message="Issue description" type="error-type">
      Additional failure details with file:line:column information
    </failure>
  </testcase>
</testsuite>
```

The provider will extract:
- File path from `classname` attribute or failure message
- Line/column numbers from failure message patterns
- Issue description from `message` attribute and failure content
- Rule name from failure `type` attribute or test `name`
- Priority based on failure vs error elements