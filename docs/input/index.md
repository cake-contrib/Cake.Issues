---
hide:
  - navigation
  - toc
template: home.html
search:
  exclude: true
title: Home
---

## How Cake Issues works

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="mdx-steps" markdown>

1.  ### Read issues

    Import one or more [issue provider] addins to read warnings and errors from your linters and
    static analyzers into a common [IIssue](https://cakebuild.net/api/Cake.Issues/IIssue/) format.

2.  ### Create reports

    Turn the collected issues into feature rich HTML reports, SARIF standard compatible reports or
    console output using a [report format] addin.

3.  ### Report to pull requests & builds

    Post the issues as comments to your pull requests or annotate your build runs, with out of the
    box support for [Azure DevOps](documentation/pull-request-systems/azure-devops/index.md),
    [GitHub Actions](documentation/build-servers/github-actions/index.md) and
    [AppVeyor](documentation/build-servers/appveyor/index.md).

<!-- markdownlint-restore -->
</div>

[issue provider]: documentation/issue-providers/index.md
[report format]: documentation/report-formats/index.md

## Why use Cake Issues?

<!-- markdownlint-disable MD030 -->
<!-- markdownlint-disable-next-line MD033 -->
<div class="grid cards mdx-why" markdown>

*   :material-globe-model:{ .lg .middle } **Rich ecosystem**

    ---

    Unlike other Cake addins, Cake Issues consists of over 15 different addins,
    working together and providing you with over 75 aliases which you can use in your Cake
    build scripts to work with issues.

    [:octicons-arrow-right-24: Reference](https://cakebuild.net/extensions/cake-issues/)

*   :material-wrench:{ .lg .middle } **One format for every tool**

    ---

    Every supported analyzer, linter or tool is read into the same issue format.
    Filtering, reporting and pull request integration work the same way, no matter
    which tools your project uses.

    [:octicons-arrow-right-24: Supported Tools](documentation/supported-tools.md)

*   :material-table:{ .lg .middle } **Extensible**

    ---

    The addins are built in a modular architecture and are providing different extension points which
    allow you to easily enhance it for supporting additional analyzers, linters, report formats and
    pull request systems.

    [:octicons-arrow-right-24: Documentation](documentation/extending/index.md)

*   :material-scale-balance:{ .lg .middle } **Open-Source**

    ---

    Cake Issues is free to use, improve, contribute and distribute.
    Source code is available on [GitHub](https://github.com/cake-contrib/Cake.Issues) under MIT license.

    [:octicons-arrow-right-24: Source code & license](https://github.com/cake-contrib/Cake.Issues)

</div>
<!-- markdownlint-restore -->

<!-- markdownlint-disable-next-line MD033 -->
<div class="mdx-cta" markdown>

## Ready to get started?

Add issue management to your Cake build in a few minutes with the ready-to-use
[Recipe packages](documentation/usage/recipe/index.md), or pick individual addins for full control.

[Get Started](documentation/usage/index.md){ .md-button .md-button--primary }
[View on GitHub](https://github.com/cake-contrib/Cake.Issues){ .md-button }

</div>
