---
Order: 20
Title: Features
Description: Features of the Cake.Issues.MsBuild addin.
---
The [Cake.Issues.MsBuild addin] provides the following features:

* Reads warnings from MSBuild log files.
* Supported log file formats:
  * [MSBuild Extension Pack XmlFileLogger]
* Supported comment formats:
  * Plain text
* Provides URLs for all code analysis (`CA*`) and StyleCop (`SA*`) warnings.
* Support for custom URL resolving using the [MsBuildAddRuleUrlResolver] alias.
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

[Cake.Issues.MsBuild addin]: https://www.nuget.org/packages/Cake.Issues.MsBuild
[MSBuild Extension Pack XmlFileLogger]: http://www.msbuildextensionpack.com/help/4.0.5.0/html/242ab4fd-c2e2-f6aa-325b-7588725aed24.htm
[MsBuildAddRuleUrlResolver]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487
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