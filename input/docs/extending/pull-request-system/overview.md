---
Order: 20
Title: Overview
Description: Overview how to implement support for a pull request system.
---
Pull Request Systems need to implement the [IPullRequestSystem] interface.

# BaseClasses

For simplifying implementation there exists base classes from which concrete implementation can be inherited.
[BasePullRequestSystem] is the main base class with the required functionality for a pull request system implementation.
Additionally there exists several classes which can be implemented to support additional optional capabilities
in a pull request system implementation.

| Base Class                               | Use case                                                                                               | Tutorial                            |
|------------------------------------------|--------------------------------------------------------------------------------------------------------|-------------------------------------|
| [BasePullRequestSystem]                  | Base class for all pull request system implementations.                                                |                                     |
| [BaseCheckingCommitIdCapability]         | Base class for capability to post issues only if pull request is for a specific commit.                |                                     |
| [BaseDiscussionThreadsCapability]        | Base class for capability to read, resolve and reopen discussion threads.                              |                                     |
| [BaseFilteringByModifiedFilesCapability] | Base class for capability to filter issues to only those affecting files modified in the pull request. |                                     |

[IPullRequestSystem]: ../../../api/Cake.Issues.PullRequests/IPullRequestSystem
[BasePullRequestSystem]: ../../../api/Cake.Issues.PullRequests/BasePullRequestSystem
[BaseCheckingCommitIdCapability]: ../../../api/Cake.Issues.PullRequests/BaseCheckingCommitIdCapability_1
[BaseDiscussionThreadsCapability]: ../../../api/Cake.Issues.PullRequests/BaseDiscussionThreadsCapability_1
[BaseFilteringByModifiedFilesCapability]: ../../../api/Cake.Issues.PullRequests/BaseFilteringByModifiedFilesCapability_1