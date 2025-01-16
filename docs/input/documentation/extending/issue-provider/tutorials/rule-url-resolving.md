---
title: Rule URL resolving
description: Instructions how to implement rule URL resolving.
---

For cases where additional logic is required to determine the URL for a rule, the `Cake.Issue`
addin provides the [BaseRuleDescription](https://cakebuild.net/api/Cake.Issues/BaseRuleDescription/){target="_blank"}
and [BaseRuleUrlResolver](https://cakebuild.net/api/Cake.Issues/BaseRuleUrlResolver_1/){target="_blank"}
classes for simplifying implementation of providing URLs linking to site providing information about issues.

## Implementing RuleUrlResolver

In the issue provider a concrete class inheriting from [BaseRuleDescription](https://cakebuild.net/api/Cake.Issues/BaseRuleDescription/){target="_blank"}
should be implemented containing all properties required to determine the URL to a rule.

The following class adds two properties `Category` and `RuleId` to the description, to handle rules following a pattern like `ABC123`,
where `ABC` is the `Category`, and `123` is the `RuleId`:

```csharp
--8<-- "snippets/extending/issue-provider/rule-url-resolving/RuleDescription.cs"
```

A class inheriting from [BaseRuleUrlResolver](https://cakebuild.net/api/Cake.Issues/BaseRuleUrlResolver_1/){target="_blank"}
needs to be implemented containing an implementation of
[TryGetRuleDescription](https://cakebuild.net/api/Cake.Issues/BaseRuleUrlResolver_1/D9DB5D44){target="_blank"}
for parsing rule urls to the concrete [BaseRuleDescription](https://cakebuild.net/api/Cake.Issues/BaseRuleDescription/){target="_blank"}
class.
Additionally different resolvers need to be registered which return the actual URL based on the rule description.

=== "Parsing rule"

    ```csharp hl_lines="6-17"
    --8<-- "snippets/extending/issue-provider/rule-url-resolving/RuleUrlResolver.cs"
    ```

=== "Resolver registration"

    ```csharp hl_lines="19-40"
    --8<-- "snippets/extending/issue-provider/rule-url-resolving/RuleUrlResolver.cs"
    ```

To use the URL resolver the [ResolveRuleUrl](https://cakebuild.net/api/Cake.Issues/BaseRuleUrlResolver_1/6B23EC74){target="_blank"}
method can be called from the issue provider:

```csharp
var resolver = new MyRuleUrlResolver();
var url = resolver.ResolveRuleUrl(rule)
```

## Support custom URL resolvers

The [AddUrlResolver](https://cakebuild.net/api/Cake.Issues/BaseRuleUrlResolver_1/AAA4FB20){target="_blank"}
method can also be called from a Cake alias to allow users of the addin to register custom resolvers.
For this the URL resolver class needs to be implemented as a singleton:

=== "Singleton"

    ```csharp hl_lines="6-12"
    --8<-- "snippets/extending/issue-provider/rule-url-resolving/RuleUrlResolverSingleton.cs"
    ```

=== "Parsing rule"

    ```csharp hl_lines="14-25"
    --8<-- "snippets/extending/issue-provider/rule-url-resolving/RuleUrlResolverSingleton.cs"
    ```

=== "Resolver registration"

    ```csharp hl_lines="27-48"
    --8<-- "snippets/extending/issue-provider/rule-url-resolving/RuleUrlResolverSingleton.cs"
    ```
