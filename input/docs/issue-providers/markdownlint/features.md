---
Order: 20
Title: Features
Description: Features of the Cake.Issues.Markdownlint addin.
---
The [Cake.Issues.Markdownlint addin] provides the following features:

* Reads warnings from [Markdownlint] output generated with `options.resultVersion` set to 1.
* Supported comment formats:
  * Plain text
* Provides URLs for all issues.
* Supported `IIssue` properties:
  * [ProviderType]
  * [AffectedFileRelativePath]
  * [Line]
  * [Message]
  * [Rule]
  * [RuleUrl]

[Cake.Issues.Markdownlint addin]: https://www.nuget.org/packages/Cake.Issues.Markdownlint
[Markdownlint]: https://github.com/DavidAnson/markdownlint
[ProviderType]: ../../api/Cake.Issues/IIssue/D5A24C72
[AffectedFileRelativePath]: ../../api/Cake.Issues/IIssue/BF0CD6F1
[Line]: ../../api/Cake.Issues/IIssue/F2A42E89
[Message]: ../../api/Cake.Issues/IIssue/18537A3D
[Rule]: ../../api/Cake.Issues/IIssue/C8BCE21E
[RuleUrl]: ../../api/Cake.Issues/IIssue/48A6F355