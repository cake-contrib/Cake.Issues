---
title: New GitHub Actions addin
category: Release Notes
---

A new [Cake.Issues.PullRequest.GitHubActions addin] has been released which brings integration with GitHub Actions and GitHub pull requests.

<!--excerpt-->

[Cake.Issues.PullRequest.GitHubActions addin] creates annotations from issues when running on GitHub Actions:

![Annotations](../docs/pull-request-systems/github-actions/githubactions-annotations.png "Annotations")

These annotations will also be shown in pull requests on the related file / position,
bringing first class integration for GitHub pull requests to Cake.Issues:

![Pull request integration](../docs/pull-request-systems/github-actions/githubactions-pullrequest-integration.png "Pull request integration")

This integration was [first released in Cake.Issues.Recipe 0.4.2] and has now been moved to its own addin, which can also be used outside of
Cake.Issues.Recipe.

[Cake.Issues.PullRequest.GitHubActions addin]: ../docs/pull-request-systems/github-actions/
[first released in Cake.Issues.Recipe 0.4.2]: cake-issues-recipe-v0.4.2-released
