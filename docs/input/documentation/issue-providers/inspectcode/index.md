---
title: InspectCode issue provider
description: Issue provider which allows you to read issues logged by JetBrains Inspect Code.
---

Support for reading issues reported by [JetBrains InspectCode] in XML format
is implemented in the [Cake.Issues.InspectCode addin].

!!! note
    Starting from version 2024.1, the default output format of [JetBrains InspectCode] is Static Analysis Results Interchange Format (SARIF).
    The XML format, which was the default in previous versions, will soon be deprecated.
    Results in the XML format are still available with the `-f="xml"` parameter.

    This issue provider is only for the deprecated XML format.
    For the new default SARIF format [Cake.Issues.Sarif] can be used.

<div class="grid cards" markdown>

- :material-creation-outline: [Features](features.md)
- :material-test-tube: [Examples](examples.md)
- :material-api: [API](https://cakebuild.net/extensions/cake-issues-inspectcode)

</div>

[JetBrains InspectCode]: https://www.jetbrains.com/help/resharper/InspectCode.html
[Cake.Issues.InspectCode addin]: https://cakebuild.net/extensions/cake-issues-inspectcode/
[Cake.Issues.Sarif]: ../sarif/index.md
