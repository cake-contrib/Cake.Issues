---
title: New addin for reading Test Anything Protocol (TAP) files
date: 2025-01-03
categories:
  - New Addin
search:
  boost: 0.5
---

A new [Cake.Issues.Tap addin] has been released which adds support for reading issues in [Test Anything Protocol (TAP)]{target="_blank"} format.

<!-- more -->

[Test Anything Protocol (TAP)]{target="_blank"} is a protocol for communicating between test logic in a language-agnostic way.
There are several linting tools which can output their result in a TAP compatible format.

The [Cake.Issues.Tap addin] supports multiple log file formats.
Details, like file, line / column or rule information, are not standardized in Test Anything Protocol (TAP).
The `GenericLogFileFormat` will therefore only return issues containing the description, which might be the file name for some tools.
To retrieve detailed information a tool specific log file format needs to be used which can parse the non-standardized data provided by the tool for every issue.

There are additional log file formats for the following tools available:

* [stylelint](https://stylelint.io/){target="_blank"}
* [Textlint](https://textlint.github.io/){target="_blank"}

See [Supported Tools] for an updated list of supported tools.

The addins is available in a version for Cake .NET Tool ([Cake.Issues.Tap]{target="_blank"})
and Cake Frosting ([Cake.Frosting.Issues.Tap]{target="_blank"}).

[Cake.Issues.Tap addin]: ../../documentation/issue-providers/tap/index.md
[Test Anything Protocol (TAP)]: https://testanything.org/
[Supported Tools]: ../../documentation/supported-tools.md
[Cake.Issues.Tap]: https://www.nuget.org/packages/Cake.Issues.Tap
[Cake.Frosting.Issues.Tap]: https://www.nuget.org/packages/Cake.Frosting.Issues.Tap
