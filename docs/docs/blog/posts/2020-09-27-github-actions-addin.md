---
title: New GitHub Actions addin
date: 2020-09-27
categories:
  - New Addin
search:
  boost: 0.5
---

A new [Cake.Issues.PullRequest.GitHubActions addin] has been released which brings integration with GitHub Actions and GitHub pull requests.

<!-- more -->

[Cake.Issues.PullRequest.GitHubActions addin] creates annotations from issues when running on GitHub Actions:

![Annotations](../../documentation/pull-request-systems/github-actions/githubactions-annotations.png "Annotations")

These annotations will also be shown in pull requests on the related file / position,
bringing first class integration for GitHub pull requests to Cake.Issues:

![Pull request integration](../../documentation/pull-request-systems/github-actions/githubactions-pullrequest-integration.png "Pull request integration")

This integration was [first released in Cake.Issues.Recipe 0.4.2]{target="_blank"} and has now been moved to its own addin, which can also be used outside of
Cake.Issues.Recipe.

[Cake.Issues.PullRequest.GitHubActions addin]: ../../documentation/pull-request-systems/github-actions/index.md
[first released in Cake.Issues.Recipe 0.4.2]: 2020-09-24-cake-issues-recipe-v0.4.2-released.md
