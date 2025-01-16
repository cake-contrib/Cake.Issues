---
title: Simple issue provider
description: Instructions how to implement a simple issue provider.
---

This tutorial explains how to implement a simple issue provider using the
[BaseIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseIssueProvider/){target="_blank"}
class from the `Cake.Issue` addin.

## Implementing issue provider

A concrete class inheriting from [BaseIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseIssueProvider/){target="_blank"}
needs to be implemented:

```csharp
--8<-- "snippets/extending/issue-provider/simple/IssuesProvider.cs"
```

## Aliases

An alias for reading issues with the provider and a property alias for returning
the provider type name should be defined:

=== "Alias for reading issues"

    ```csharp hl_lines="7-28"
    --8<-- "snippets/extending/issue-provider/simple/Aliases.cs"
    ```

=== "Alias for property type name"

    ```csharp hl_lines="30-45"
    --8<-- "snippets/extending/issue-provider/simple/Aliases.cs"
    ```
