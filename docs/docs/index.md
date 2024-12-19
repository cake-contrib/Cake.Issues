---
hide:
  - navigation
  - toc
layout: default
search:
  exclude: true
title: Home
---

<div class="grid cards" markdown>

-   :material-scale-balance:{ .lg .middle } __Open-Source__

    ---

    Cake Issues is free to use, improve, contribute and distribute.
    Source code is available on [GitHub](https://github.com/cake-contrib/Cake.Issues){target="_blank"} under MIT license.

    [:octicons-arrow-right-24: Source code & license](https://github.com/cake-contrib/Cake.Issues){target="_blank"}

-   :material-globe-model:{ .lg .middle } __Rich ecosystem__

    ---

    Unlike other Cake addins, Cake Issues consists of over 15 different addins,
    working together and providing you with over 75 [aliases](https://cakebuild.net/extensions/cake-issues/){target="_blank"} which you can use in your Cake
    build scripts to work with issues.

    [:octicons-arrow-right-24: Reference](https://cakebuild.net/extensions/cake-issues/){target="_blank"}

-   :material-wrench:{ .lg .middle } __Supports your tooling__

    ---

    Read issues from different analyzers, linters or tools.
    The growing range of out-of-the-box supported tools include
    [MSBuild](documentation/issue-providers/msbuild/index.md),
    [JetBrains InspectCode (ReSharper)](documentation/issue-providers/inspectcode/index.md),
    [ESLint](documentation/issue-providers/eslint/index.md),
    [Markdownlint](documentation/issue-providers/markdownlint/index.md),
    [DocFX](documentation/issue-providers/docfx/index.md).

    [:octicons-arrow-right-24: Issue Providers](documentation/issue-providers/index.md)

-   :material-monitor-dashboard:{ .lg .middle } __Reporting__

    ---

    Cake Issues provides aliases to create reports from the parsed issues.
    There's a [generic reporting addin](documentation/report-formats/generic/index.md) which allows to create reports using out-of-the-box or custom Razor templates
    and [an addin for creating SARIF compatible files](documentation/report-formats/sarif/index.md).

    [:octicons-arrow-right-24: Report Formats](documentation/report-formats/index.md)

-   :material-eye:{ .lg .middle } __Pull request and build workflow integration__

    ---

    Issues found on a feature branch can be reported to pull requests or build runs giving developers instant and direct feedback.
    There's out of the box support for [Azure DevOps](documentation/pull-request-systems/azure-devops/index.md),
    [GitHub Actions](documentation/pull-request-systems/github-actions/index.md) and
    [AppVeyor](documentation/pull-request-systems/appveyor/index.md).

    [:octicons-arrow-right-24: Pull Request Systems](documentation/pull-request-systems/index.md)

-   :material-table:{ .lg .middle } __Extensible__

    ---

    The addins are built in a modular architecture and are providing different [extension points](documentation/extending/index.md)
    which allow you to easily enhance it for supporting additional analyzers, linters, report formats and pull request systems.

    [:octicons-arrow-right-24: Documentation](documentation/extending/index.md)
</div>