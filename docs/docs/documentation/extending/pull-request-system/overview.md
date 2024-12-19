---
title: Overview
description: Overview how to implement support for a pull request system.
---

Pull Request Systems need to implement the [IPullRequestSystem](https://cakebuild.net/api/Cake.Issues.PullRequests/IPullRequestSystem/){target="_blank"}
interface.

## BaseClasses

For simplifying implementation there exists base classes from which concrete implementation can be inherited.
[BasePullRequestSystem](https://cakebuild.net/api/Cake.Issues.PullRequests/BasePullRequestSystem/){target="_blank"}
is the main base class with the required functionality for a pull request system implementation.
Additionally there exists several classes which can be implemented to support additional optional capabilities
in a pull request system implementation.

| Base Class                                                                                                                                              | Use case                                                                                               | Tutorial                            |
|---------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------|-------------------------------------|
| [BasePullRequestSystem](https://cakebuild.net/api/Cake.Issues.PullRequests/BasePullRequestSystem/){target="_blank"}                                     | Base class for all pull request system implementations.                                                |                                     |
| [BaseCheckingCommitIdCapability](https://cakebuild.net/api/Cake.Issues.PullRequests/BaseCheckingCommitIdCapability_1/){target="_blank"}                 | Base class for capability to post issues only if pull request is for a specific commit.                |                                     |
| [BaseDiscussionThreadsCapability](https://cakebuild.net/api/Cake.Issues.PullRequests/BaseDiscussionThreadsCapability_1/){target="_blank"}               | Base class for capability to read, resolve and reopen discussion threads.                              |                                     |
| [BaseFilteringByModifiedFilesCapability](https://cakebuild.net/api/Cake.Issues.PullRequests/BaseFilteringByModifiedFilesCapability_1/){target="_blank"} | Base class for capability to filter issues to only those affecting files modified in the pull request. |                                     |
