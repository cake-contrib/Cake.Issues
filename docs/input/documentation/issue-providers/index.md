---
title: Issue Providers
description: Documentation of the different issue provider addins.
---

Issue provider addins are responsible for providing the output of an analyzer or linter to the Cake Issues addin.

<div class="grid cards" markdown>

- :material-layers-plus: __[DocFx]__ – Issue provider for reading DocFx warnings
- :material-layers-plus: __[ESLint]__ – Issue provider for reading ESLint issues
- :material-layers-plus: __[Git Repository]__ – Issue provider for analyzing Git repositories
- :material-layers-plus: __[Inspect Code]__ – Issue provider for reading JetBrains Inspect Code  / ReSharper issues
- :material-layers-plus: __[JUnit]__ – Issue provider for reading JUnit XML format
- :material-layers-plus: __[Markdownlint]__ – Issue provider for reading issues from markdownlint
- :material-layers-plus: __[MsBuild]__ – Issue provider for reading MsBuild errors and warnings
- :material-layers-plus: __[Sarif]__ – Issue provider for reading SARIF reports
- :material-layers-plus: __[Test Anything Protocol (TAP)]__ – Issue provider for reading TAP reports
- :material-layers-plus: __[Terraform]__ – Issue provider for reading Terraform validation output

</div>

[DocFx]: docfx/index.md
[ESLint]: eslint/index.md
[Git Repository]: gitrepository/index.md
[Inspect Code]: inspectcode/index.md
[JUnit]: junit/index.md
[Markdownlint]: markdownlint/index.md
[MsBuild]: msbuild/index.md
[Sarif]: sarif/index.md
[Test Anything Protocol (TAP)]: tap/index.md
[Terraform]: terraform/index.md

!!! tip
    See [How to implement issue providers] for instruction on how to implement support for additional issue providers.

[How to implement issue providers]: ../extending/issue-provider/overview.md
