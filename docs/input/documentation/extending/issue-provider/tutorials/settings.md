---
title: Provider settings
description: Instructions how to implement an issue provider with specific settings.
---

Often issue providers require specific settings.
For these cases the `Cake.Issue` addin provides the [BaseConfigurableIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseConfigurableIssueProvider_1/){target="_blank"}
and [IssueProviderSettings](https://cakebuild.net/api/Cake.Issues/IssueProviderSettings/){target="_blank"} classes
for simplifying implementation in the issue provider addin.

## Implementing issue provider

A concrete class inheriting from [BaseConfigurableIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseConfigurableIssueProvider_1/){target="_blank"}
needs to be implemented defining the concrete settings class to use:

```csharp
--8<-- "snippets/extending/issue-provider/settings/IssuesProvider.cs"
```

Also a concrete class inheriting from [IssueProviderSettings](https://cakebuild.net/api/Cake.Issues/IssueProviderSettings/){target="_blank"}
needs to be implemented.
Based on the capabilities of the issue provider the appropriate constructors for reading from the file system
or memory can be made public:

```csharp
--8<-- "snippets/extending/issue-provider/settings/Settings.cs"
```

## Aliases

An alias for reading issues with the provider should be provided.
For convenience of the user and based on the capabilities of the issue provider,
additional aliases for reading from the file system or from memory can be added.
Finally an additional property alias for returning the provider type name should be defined:

=== "Alias for reading issues"

    ```csharp hl_lines="7-32"
    --8<-- "snippets/extending/issue-provider/settings/Aliases.cs"
    ```

=== "Additional convenience aliases"

    ```csharp hl_lines="34-88"
    --8<-- "snippets/extending/issue-provider/settings/Aliases.cs"
    ```

=== "Alias for property type name"

    ```csharp hl_lines="90-105"
    --8<-- "snippets/extending/issue-provider/settings/Aliases.cs"
    ```
