---
title: Multiple log file formats
description: Instructions how to implement an issue provider with support for multiple log file formats.
---

A single issue provider might support reading issues from multiple different log file formats.
For these cases the `Cake.Issue` addin provides the [BaseMultiFormatIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseMultiFormatIssueProvider_2/){target="_blank"},
[BaseMultiFormatIssueProviderSettings](https://cakebuild.net/api/Cake.Issues/BaseMultiFormatIssueProviderSettings_2/){target="_blank"}
and [BaseLogFileFormat](https://cakebuild.net/api/Cake.Issues/BaseLogFileFormat_2/){target="_blank"}
classes for simplifying implementation in the issue provider addin.

## Implementing issue provider

A concrete class inheriting from [BaseMultiFormatIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseMultiFormatIssueProvider_2/){target="_blank"}
needs to be implemented defining the concrete types.

```csharp
--8<-- "snippets/extending/issue-provider/logfile-format/IssuesProvider.cs"
```

Also a concrete class inheriting from [BaseMultiFormatIssueProviderSettings](https://cakebuild.net/api/Cake.Issues/BaseMultiFormatIssueProviderSettings_2/){target="_blank"}
needs to be implemented defining the concrete types.
Based on the capabilities of the log file formats the appropriate constructors for reading from the file system
or memory can be made public:

```csharp
--8<-- "snippets/extending/issue-provider/logfile-format/Settings.cs"
```

## Implementing log file format infrastructure

An abstract class inheriting from [BaseLogFileFormat](https://cakebuild.net/api/Cake.Issues/BaseLogFileFormat_2/){target="_blank"}
needs to be implemented defining the concrete types for the issue provider:

```csharp
--8<-- "snippets/extending/issue-provider/logfile-format/BaseLogFileFormat.cs"
```

## Implementing log file format

The different log file formats of an issue provider need to be inherited from the abstract log file format class:

```csharp
--8<-- "snippets/extending/issue-provider/logfile-format/ConcreteLogFileFormat.cs"
```

## Aliases

For each concrete log file format a Cake property alias should be provided.
Additionally an alias for reading issues with a specific format should be provided.
For convenience of the user and based on the capabilities of the issue provider additional aliases for reading
from the file system or from memory can be added.
Finally an additional property alias for returning the provider type name should be defined.

=== "Alias for log file format"

    ```csharp hl_lines="7-20"
    --8<-- "snippets/extending/issue-provider/logfile-format/Aliases.cs"
    ```

=== "Alias for reading issues"

    ```csharp hl_lines="22-54"
    --8<-- "snippets/extending/issue-provider/logfile-format/Aliases.cs"
    ```

=== "Additional convenience aliases"

    ```csharp hl_lines="56-129"
    --8<-- "snippets/extending/issue-provider/logfile-format/Aliases.cs"
    ```

=== "Alias for property type name"

    ```csharp hl_lines="131-146"
    --8<-- "snippets/extending/issue-provider/logfile-format/Aliases.cs"
    ```
