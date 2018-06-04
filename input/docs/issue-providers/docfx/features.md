---
Order: 20
Title: Features
Description: Features of the Cake.Issues.DocFx addin.
---
The [Cake.Issues.DocFx addin] provides the following features:

* Reads warnings from [DocFx] log files. [DocFx] can be run with [Cake.DocFx] addin.
* Supported comment formats:
  * Plain text
* Supported `IIssue` properties:
  * [ProviderType]
  * [ProviderName]
  * [AffectedFileRelativePath]
  * [Line]
  * [Message]
  * [Priority] (Always [IssuePriority.Warning])
  * [PriorityName] (Always [IssuePriority.Warning])
  * [Rule]

[Cake.Issues.DocFx addin]: https://www.nuget.org/packages/Cake.Issues.DocFx
[DocFx]: https://dotnet.github.io/docfx/
[Cake.DocFx]: https://www.nuget.org/packages/Cake.DocFx
[ProviderType]: ../../../api/Cake.Issues/IIssue/D5A24C72
[ProviderName]: ../../../api/Cake.Issues/IIssue/FA8BB1A0
[AffectedFileRelativePath]: ../../../api/Cake.Issues/IIssue/BF0CD6F1
[Line]: ../../../api/Cake.Issues/IIssue/F2A42E89
[Message]: ../../../api/Cake.Issues/IIssue/18537A3D
[Priority]: ../../../api/Cake.Issues/IIssue/BFEFFBB1
[PriorityName]: ../../../api/Cake.Issues/IIssue/05A39052
[Rule]: ../../../api/Cake.Issues/IIssue/C8BCE21E
[IssuePriority.Warning]: ../../../api/Cake.Issues/IssuePriority/7A0CE07F