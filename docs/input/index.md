---
hide:
  - navigation
  - toc
template: home.html
search:
  exclude: true
title: Home
---

# Bring actionable code quality into every Cake build

Cake Issues turns analyzer, linter and build output into a consistent workflow for reading, filtering, reporting and publishing issues from your Cake scripts.

<div class="hero-actions" markdown>

[Get started](documentation/usage/index.md){ .md-button .md-button--primary }
[Explore supported tools](documentation/supported-tools.md){ .md-button }

</div>

<div class="grid cards home-highlights" markdown>

-   :material-lightning-bolt:{ .lg .middle } **Ship feedback where developers need it**

    ---

    Read issues from analyzers, linters and build tools, enrich them with metadata, and surface them directly in pull requests, build output, reports or release workflows.

-   :material-layers-triple:{ .lg .middle } **Compose the workflow you need**

    ---

    Start with a single addin or combine providers, report formats, pull request integrations and build server support into a workflow that matches your pipeline.

-   :material-rocket-launch:{ .lg .middle } **Scale from one repo to many**

    ---

    Reuse the same issue handling approach across application, library and infrastructure repositories without rewriting custom parsing or reporting logic.

</div>

## Why teams choose Cake Issues

<div class="grid cards" markdown>

*   :material-toy-brick-plus:{ .lg .middle } **Modular addin ecosystem**

    ---

    Use the core addin with specialized packages for issue providers, report formats, build servers and pull request systems. Adopt only the pieces you need and extend the rest over time.

    [:octicons-arrow-right-24: How Cake Issues works](documentation/how-cake-issues-works/index.md)

*   :material-tools:{ .lg .middle } **Support for the tools you already run**

    ---

    Parse results from .NET, JavaScript, infrastructure, documentation and security tooling through a shared issue model instead of maintaining bespoke integrations.

    [:octicons-arrow-right-24: Supported tools](documentation/supported-tools.md)

*   :material-source-pull:{ .lg .middle } **Native pull request and build integration**

    ---

    Publish feedback into GitHub Actions, Azure DevOps and AppVeyor so contributors see issues in the same systems that run the build.

    [:octicons-arrow-right-24: Pull request systems](documentation/pull-request-systems/index.md)
    [:octicons-arrow-right-24: Build servers](documentation/build-servers/index.md)

*   :material-file-chart:{ .lg .middle } **Multiple reporting options**

    ---

    Generate console output, rich HTML reports and SARIF artifacts for local diagnostics, CI logs and downstream tooling.

    [:octicons-arrow-right-24: Report formats](documentation/report-formats/index.md)

*   :material-puzzle:{ .lg .middle } **Built to be extended**

    ---

    Add support for new analyzers, report formats and integration targets through documented extension points instead of forking the project.

    [:octicons-arrow-right-24: Extending Cake Issues](documentation/extending/index.md)

*   :material-github:{ .lg .middle } **Open source and community maintained**

    ---

    Cake Issues is developed in the open, versioned as NuGet packages and backed by contributors focused on build automation and actionable code quality.

    [:octicons-arrow-right-24: Source code on GitHub](https://github.com/cake-contrib/Cake.Issues)

</div>

## Common journeys

<div class="grid cards home-journeys" markdown>

-   **I want to start quickly**

    Follow the usage guide to install the addins, read issues from your tools and create your first report.

    [:octicons-arrow-right-24: Start with the user guide](documentation/usage/index.md)

-   **I need pull request annotations**

    Connect Cake Issues to your CI system and publish findings directly to pull requests and build runs.

    [:octicons-arrow-right-24: Set up pull request reporting](documentation/usage/reporting-issues-to-pull-requests/index.md)

-   **I want to extend the ecosystem**

    Use the extension documentation to add new providers, report formats or build integrations.

    [:octicons-arrow-right-24: Explore extension points](documentation/extending/index.md)

</div>
