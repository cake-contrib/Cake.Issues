---
title: Introduction
description: Introduction
---

The Cake.Issues addins for the [Cake build automation system](https://cakebuild.net){target="_blank"}
offer an extensive and flexible solution for reading linting issues.

Cake.Issues redefines issue management within the Cake build system by offering a comprehensive, universal, and extensible solution.
The unique capabilities of the addins empower development teams to enforce coding standards, generate insightful reports,
seamlessly incorporate various linting tools, and streamlining the integration with pull requests.
With its [modular architecture] and extensive [set of aliases](https://cakebuild.net/extensions/cake-issues/){target="_blank"},
Cake.Issues provides a future-proof infrastructure for issue management in Cake builds,
fostering a more efficient and adaptable development process.

## Unique Problem Solving

Some examples how Cake.Issues can help development teams to improve code quality.

### Break build on linting issues

Cake.Issues provides a seamless integration, allowing you to enforce coding standards by breaking builds when linting issues are detected.

[:octicons-arrow-right-24: Breaking builds](usage/breaking-builds/breaking-builds.md)

### Pull Requests integration

Ensure linting issues are promptly addressed by having them reported as comments on pull requests.
Cake.Issues bridges the gap between linting tools and version control systems, fostering efficient collaboration during code reviews.

[:octicons-arrow-right-24: Integrate with pull request systems](pull-request-systems/index.md)

### Reports

Craft detailed and visually appealing reports for linting issues directly within your Cake build.
The addins facilitates easy identification and resolution of linting concerns, enhancing the overall code quality.

[:octicons-arrow-right-24: Creating Reports](report-formats/index.md)

## Universal Compatibility

### Diverse Linting Tool Support

Regardless of the linting tools you use, Cake.Issues ensures that you're not left out.
Cake.Issues supports a variety of analyzers and linters, allowing you to incorporate new tools effortlessly
while maintaining integration with existing ones.

[:octicons-arrow-right-24: Supported Tools](supported-tools.md)

### Build System Agnosticism

Embrace the freedom to choose the build system that best suit your needs.
If your current build system lacks tasks for reporting issues in pull requests, Cake.Issues steps in to fill that void seamlessly.
In the case of using multiple CI services, Cake.Issues guarantees a consistent feature set across all of them.

[:octicons-arrow-right-24: Supported Build and pull request systems](pull-request-systems/index.md)

## Unprecedented Extensibility

### Modular Architecture

The Cake.Issues addin breaks away from the norm by offering a modular architecture.
Comprising over 15 distinct addins, it presents a cohesive solution through more than 75 aliases for Cake builds,
providing unparalleled flexibility.

[:octicons-arrow-right-24: Architecture](how-cake-issues-works.md)

### Extensible Infrastructure

Designed with extensibility in mind, Cake.Issues provides extension points for supporting additional
analyzers, linters,report formats, and code review systems.
This adaptability ensures that your build scripts can evolve with the ever-changing landscape of development tools.

[:octicons-arrow-right-24: Documentation](extending/index.md)

<!-- Do you want to break your build on JetBrains InspectCode issues?
Do you want to create nice reports for StyleCop issues in your code?
Do you want to have ESLint issues reported as comments to pull requests?
The Cake.Issues addins allows you to do this and much more.
Read issues from different analyzer or linters, create reports or add them as comments to pull requests.

While some linting tools provide nice reporting capabilities, others don't.
Some build systems provide tasks to report issues into pull requests.
But if you're using another build system your out of luck.
Maybe you found a new nice linting tool which does exactly what you need,
but probably it won't integrate well with your existing other linting tools.

The Cake.Issues addins tries to solve this, by providing a common and extensible infrastructure
for issue management in Cake build scripts.

Unlike other Cake addins, the Cake Issues addin consists of over 15 different addins,
working together and providing over 75 aliases which can be used in Cake build scripts to work with issues.
The addins are built in a [modular architecture] and are providing different [extension points] which allows to easily
enhance them for supporting additional analyzers, linters, report formats and code review systems.

!!! tip
    To get started you can use one of the [Cake.Issues recipes], which bring support for different linters and
    integration with build and pull request systems out-of-the box in a single and easy to use NuGet package.

[:fontawesome-solid-video: Presentation](https://gitpitch.com/pascalberger/Cake.Issues-Presentation){ .md-button }

[:fontawesome-solid-graduation-cap: Tutorials](../usage/index.md){ .md-button } -->

[modular architecture]: how-cake-issues-works.md
<!-- [extension points]: ../extending/index.md
[Cake.Issues recipes]: ../recipe/overview.md -->
