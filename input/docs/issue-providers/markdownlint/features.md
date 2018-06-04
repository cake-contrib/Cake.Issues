---
Order: 20
Title: Features
Description: Features of the Cake.Issues.Markdownlint addin.
---
The [Cake.Issues.Markdownlint addin] provides the following features:

* Reads warnings from [Markdownlint] output generated with `options.resultVersion` set to 1.
* Reads warnings from [markdownlint-cli] log files. [markdownlint-cli] can be run with [Cake.Markdownlint] addin.
* Supported comment formats:
  * Plain text
* Provides URLs for all issues.
* Supported `IIssue` properties:
  * [ProviderType]
  * [ProviderName]
  * [AffectedFileRelativePath]
  * [Line]
  * [Message]
  * [Priority] (Always [IssuePriority.Warning])
  * [PriorityName] (Always [IssuePriority.Warning])
  * [Rule]
  * [RuleUrl]

[Cake.Issues.Markdownlint addin]: https://www.nuget.org/packages/Cake.Issues.Markdownlint
[Markdownlint]: https://github.com/DavidAnson/markdownlint
[markdownlint-cli]: https://github.com/igorshubovych/markdownlint-cli
[Cake.Markdownlint]: https://www.nuget.org/packages/Cake.Markdownlint/
[ProviderType]: ../../../api/Cake.Issues/IIssue/D5A24C72
[ProviderName]: ../../../api/Cake.Issues/IIssue/FA8BB1A0
[AffectedFileRelativePath]: ../../../api/Cake.Issues/IIssue/BF0CD6F1
[Line]: ../../../api/Cake.Issues/IIssue/F2A42E89
[Message]: ../../../api/Cake.Issues/IIssue/18537A3D
[Priority]: ../../../api/Cake.Issues/IIssue/BFEFFBB1
[PriorityName]: ../../../api/Cake.Issues/IIssue/05A39052
[Rule]: ../../../api/Cake.Issues/IIssue/C8BCE21E
[RuleUrl]: ../../../api/Cake.Issues/IIssue/48A6F355
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F