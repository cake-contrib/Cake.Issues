---
title: Overview
description: Overview how to implement support for a build server.
---

Build server support needs to implement the [IPullRequestSystem](https://cakebuild.net/api/Cake.Issues.PullRequests/IPullRequestSystem/){target="_blank"}
interface.

## BaseClasses

For simplifying implementation there exists base classes from which concrete implementation can be inherited.
[BasePullRequestSystem](https://cakebuild.net/api/Cake.Issues.PullRequests/BasePullRequestSystem/){target="_blank"}
is the main base class with the required functionality for implementing support for a build server.

| Base Class                                                                                                          | Use case                                         |
|---------------------------------------------------------------------------------------------------------------------|--------------------------------------------------|
| [BasePullRequestSystem](https://cakebuild.net/api/Cake.Issues.PullRequests/BasePullRequestSystem/){target="_blank"} | Base class for all build server implementations. |
