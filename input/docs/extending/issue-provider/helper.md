---
Order: 50
Title: Helper
Description: Overview of different helper classes for implementing issue providers.
---
The following helpers are provider by `Cake.Issues` for simplifying implementation of issue providers:

| Helper                   | Description                                                                    |
|--------------------------|--------------------------------------------------------------------------------|
| [StringPathExtensions]   | Extensions for string for handling paths.                                      |
| [ByteArrayExtensions]    | Extensions for converting between strings an byte arrays.                      |

# File linking

Cake.Issues provides infrastructure to get links to files on source code hosts like GitHub or Azure Repos.
This infrastructure can be used inside issue providers to generate file links which can be used inside the issue messages:

```csharp
protected override IEnumerable<IIssue> InternalReadIssues()
{
    var result = new List<IIssue>();

    var filePath = "foo.cs";
    var line = 10;

    var fileLink = 
        this.Settings.FileLinkSettings.GetFileLink(
            IssueBuilder
                .NewIssue("Issue for creating file link", this)
                .InFile(filePath, line)
                .Create()
        );

    var htmlMessage =
        $"This is an issues in the file <a href=\"{fileLink}\">{filePath}</a>";

    var issue =
        IssueBuilder
            .NewIssue("MyMessage", this)
            .WithMessageInHtmlFormat(htmlMessage)
            .InFile(filePath, line)
            .Create();

    return result;
}
```

[StringPathExtensions]: ../../../api/Cake.Issues/StringPathExtensions/
[ByteArrayExtensions]: ../../../api/Cake.Issues/ByteArrayExtensions/