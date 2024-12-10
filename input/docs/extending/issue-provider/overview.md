---
Order: 20
Title: Overview
Description: Overview how to implement support for an analyzer or linter.
---
Issue providers need to implement the [IIssueProvider] interface.

# Base classes

For simplifying implementation there exists base classes from which concrete implementation can be inherited.

| Base Class                      | Use case                                                               | Tutorial                            |
|---------------------------------|------------------------------------------------------------------------|-------------------------------------|
| [BaseIssueProvider]             | Base class for a simple issue provider implementation.                 | [Simple provider]                   |
| [BaseConfigurableIssueProvider] | Base class for a issue provider with issue provider specific settings. | [Provider settings]                 |
| [BaseMultiFormatIssueProvider]  | Base class for issue providers supporting multiple log formats.        | [Multiple log file formats support] |

[IIssueProvider]: ../../../api/Cake.Issues/IIssueProvider/
[BaseIssueProvider]: ../../../api/Cake.Issues/BaseIssueProvider
[BaseConfigurableIssueProvider]: ../../../api/Cake.Issues/BaseConfigurableIssueProvider_1
[BaseMultiFormatIssueProvider]: ../../../api/Cake.Issues/BaseMultiFormatIssueProvider_2
[Simple provider]: tutorials/simple
[Provider settings]: tutorials/settings
[Multiple log file formats support]: tutorials/logfile-format