---
hide:
  - navigation
  - toc
layout: default
search:
  exclude: true
title: Home
---

<!-- <div class="jumbotron">
    <div class="container">
        <h1 class="display-4">Cake Issues</h1>
        <p>
            Issue Management for the Cake Build System<br/>
        </p>
        <a class="btn btn-primary btn-lg" href="docs" role="button">Learn more</a>
        <a class="btn btn-primary btn-lg" href="addins" role="button">Addins</a>
        <a class="btn btn-primary btn-lg" href="dsl" role="button">Aliases</a>
    </div>
</div> -->

<div class="grid cards" markdown>

-   :material-scale-balance:{ .lg .middle } __Open-Source__

    ---

    Cake Issues is free to use, improve, contribute and distribute.
    Source code is available on [GitHub](https://github.com/cake-contrib/Cake.Issues) under MIT license.

    [:octicons-arrow-right-24: Source code & license](https://github.com/cake-contrib/Cake.Issues)

-   :material-globe-model:{ .lg .middle } __Rich ecosystem__

    ---

    Unlike other Cake addins, Cake Issues consists of over 15 different <a href="addins">addins</a>,
    working together and providing you with over 75 <a href="dsl">aliases</a> which you can use in your Cake
    build scripts to work with issues.

    [:octicons-arrow-right-24: Reference](#)

-   :material-wrench:{ .lg .middle } __Supports your tooling__

    ---

    Read issues from different analyzers, linters or tools.
    The growing range of out-of-the-box supported tools include
    <a href="documentation/issue-providers/msbuild">MSBuild</a>,
    <a href="documentation/issue-providers/inspectcode">JetBrains InspectCode (ReSharper)</a>,
    <a href="documentation/issue-providers/eslint">ESLint</a>,
    <a href="documentation/issue-providers/markdownlint">Markdownlint</a>,
    <a href="documentation/issue-providers/docfx">DocFX</a>.

    [:octicons-arrow-right-24: Customization](#)

-   :material-monitor-dashboard:{ .lg .middle } __Reporting__

    ---

    Cake Issues provides aliases to create reports from the parsed issues.
    There's a <a href="docs/report-formats/generic/">generic reporting addin</a> which allows to create reports using out-of-the-box or custom Razor templates
    and <a href="docs/report-formats/sarif/">an addin for creating SARIF compatible files</a>.

    [:octicons-arrow-right-24: License](#)

-   :material-eye:{ .lg .middle } __Pull request and build workflow integration__

    ---

    Issues found on a feature branch can be reported to pull requests or build runs giving developers instant and direct feedback.
    There's out of the box support for <a href="docs/pull-request-systems/azure-devops/">Azure DevOps</a>,
    <a href="docs/pull-request-systems/github-actions/">GitHub Actions</a> and
    <a href="docs/pull-request-systems/appveyor/">AppVeyor</a>.

    [:octicons-arrow-right-24: License](#)

-   :material-table:{ .lg .middle } __Extensible__

    ---

    The addins are built in a modular architecture and are providing different <a href="docs/extending">extension points</a>
    which allow you to easily enhance it for supporting additional analyzers, linters, report formats and pull request systems.

    [:octicons-arrow-right-24: License](#)
</div>