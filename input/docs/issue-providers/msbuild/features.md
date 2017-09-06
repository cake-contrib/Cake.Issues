---
Order: 20
Title: Features
Description: Features of the Cake.Issues.MsBuild addin.
---
The [Cake.Issues.MsBuild addin] provides the following features:

* Reads warnings from MSBuild log files.
* Supported log file formats:
  * [MSBuild Extension Pack XmlFileLogger]
* Provides URLs for all code analysis (`CA*`) and StyleCop (`SA*`) warnings.
* Support for custom URL resolving using the [MsBuildAddRuleUrlResolver] alias.

[Cake.Issues.MsBuild addin]: https://www.nuget.org/packages/Cake.Issues.MsBuild
[MSBuild Extension Pack XmlFileLogger]: http://www.msbuildextensionpack.com/help/4.0.5.0/html/242ab4fd-c2e2-f6aa-325b-7588725aed24.htm
[MsBuildAddRuleUrlResolver]: ../../../api/Cake.Issues.MsBuild/MsBuildIssuesAliases/93C21487