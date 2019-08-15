---
Order: 30
Title: Supported tools
Description: Tools supported by Cake.Issues.Recipe.
---

Cake.Issues.Recipe supports reading issues from different tools and integrates with different build and pull request systems.

# Tools

Cake.Issues.Recipe supports reading issues from output of the following tools:

| Tool                              | Format                                 | Property                                                      |
|-----------------------------------|----------------------------------------|---------------------------------------------------------------|
| MsBuild                           | [MSBuild Extension Pack XmlFileLogger] | `IssuesParameters.InputFiles.MsBuildXmlFileLoggerLogFilePath` |
| MsBuild                           | Binary Log File                        | `IssuesParameters.InputFiles.MsBuildBinaryLogFilePath`        |
| JetBrains InspectCode (ReSharper) |                                        | `IssuesParameters.InputFiles.InspectCodeLogFilePath`          |

[MSBuild Extension Pack XmlFileLogger]: http://www.msbuildextensionpack.com/help/4.0.5.0/html/242ab4fd-c2e2-f6aa-325b-7588725aed24.htm

# Build systems

Cake.Issues.Recipe integrates with the following build systems:

| Build System                   | Issues summary                                                   | Full issues report                                               |
|--------------------------------|------------------------------------------------------------------|------------------------------------------------------------------|
| Azure Pipelines (Azure DevOps) | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span> |

# Pull request systems

Cake.Issues.Recipe integrates with the following pull request systems:

| Pull Request System        | Write issues to pull requests                                    | Set pull request status                                          |
|----------------------------|------------------------------------------------------------------|------------------------------------------------------------------|
| Azure Repos (Azure DevOps) | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span> |
