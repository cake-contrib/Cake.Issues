---
title: New addin for reading SARIF files
date: 2024-04-14
categories:
  - New Addin
search:
  boost: 0.5
---

In [version 4.2.0] a new [Cake.Issues.Sarif addin] has been released which adds support for reading issues in [SARIF]{target="_blank"} format.

<!-- more -->

[SARIF]{target="_blank"} is an industry standard format for the output of static analysis tools.
With the introduction of [SARIF]{target="_blank"} support through the [Cake.Issues.Sarif addin]
any tool which can output results in [SARIF]{target="_blank"} format can now be used together with Cake Issues.

See [Supported Tools] for an updated list of supported tools.

The addins is available in a version for Cake .NET Tool ([Cake.Issues.Sarif]{target="_blank"})
and Cake Frosting ([Cake.Frosting.Issues.Sarif]{target="_blank"}).

[version 4.2.0]: 2024-04-14-cake-issues-v4.2.0-released.md
[Cake.Issues.Sarif addin]: ../../documentation/issue-providers/sarif/index.md
[SARIF]: https://sarifweb.azurewebsites.net/
[Supported Tools]: ../../documentation/supported-tools.md
[Cake.Issues.Sarif]: https://www.nuget.org/packages/Cake.Issues.Sarif
[Cake.Frosting.Issues.Sarif]: https://www.nuget.org/packages/Cake.Frosting.Issues.Sarif
