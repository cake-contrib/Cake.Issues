---
title: Overview
description: Overview how to implement support for an analyzer or linter.
---

Issue providers need to implement the [IIssueProvider](https://cakebuild.net/api/Cake.Issues/IIssueProvider/)
interface.

## Base classes

For simplifying implementation there exists base classes from which concrete implementation can be inherited.

| Base Class                                                                                                               | Use case                                                               | Tutorial                            |
|--------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------|-------------------------------------|
| [BaseIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseIssueProvider/)                           | Base class for a simple issue provider implementation.                 | [Simple provider]                   |
| [BaseConfigurableIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseConfigurableIssueProvider_1/) | Base class for a issue provider with issue provider specific settings. | [Provider settings]                 |
| [BaseMultiFormatIssueProvider](https://cakebuild.net/api/Cake.Issues/BaseMultiFormatIssueProvider_2/)   | Base class for issue providers supporting multiple log formats.        | [Multiple log file formats support] |

[Simple provider]: tutorials/simple.md
[Provider settings]: tutorials/settings.md
[Multiple log file formats support]: tutorials/logfile-format.md
