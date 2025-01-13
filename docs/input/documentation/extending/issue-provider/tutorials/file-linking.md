---
title: File links in message
description: Instructions how to implement add file links to issue messages.
---

Cake Issues provides infrastructure to [get links to files] on source code hosts like GitHub or Azure Repos.

This infrastructure can also be used inside an issue providers `InternalReadIssues` method to generate file links
which can be used inside the issue messages:

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

[get links to files]: ../../../usage/reading-issues/file-linking.md
