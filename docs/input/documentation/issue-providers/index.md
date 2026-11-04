---
title: Issue Providers
description: Documentation of the different issue provider addins.
---

Issue provider addins are responsible for providing the output of an analyzer or linter to the
Cake Issues addin.

<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards" markdown>

* :material-layers-plus: **[DocFx]** – Issue provider for reading DocFx warnings
* :material-layers-plus: **[ESLint]** – Issue provider for reading ESLint issues
* :material-layers-plus: **[Git Repository]** – Issue provider for analyzing Git repositories
* :material-layers-plus: **[Inspect Code]** – Issue provider for reading JetBrains Inspect Code
  / ReSharper issues
* :material-layers-plus: **[Markdownlint]** – Issue provider for reading issues from markdownlint
* :material-layers-plus: **[MsBuild]** – Issue provider for reading MsBuild errors and warnings
* :material-layers-plus: **[Sarif]** – Issue provider for reading SARIF reports
* :material-layers-plus: **[Test Anything Protocol (TAP)]** – Issue provider for reading TAP reports
* :material-layers-plus: **[Terraform]** – Issue provider for reading Terraform validation output

</div>

[DocFx]: docfx/index.md
[ESLint]: eslint/index.md
[Git Repository]: gitrepository/index.md
[Inspect Code]: inspectcode/index.md
[Markdownlint]: markdownlint/index.md
[MsBuild]: msbuild/index.md
[Sarif]: sarif/index.md
[Test Anything Protocol (TAP)]: tap/index.md
[Terraform]: terraform/index.md

!!! tip
    See [How to implement issue providers] for instruction on how to implement support for
    additional issue providers.

[How to implement issue providers]: ../extending/issue-provider/overview.md
