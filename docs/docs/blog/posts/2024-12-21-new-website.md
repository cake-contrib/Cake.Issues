---
title: New website
date: 2024-12-21
categories:
  - Announcements
search:
  boost: 0.5
---

Today a new version of the Cake Issues website has been published.

<!-- more -->

The Cake Issues website was introduced 2017 as a static website built using [Wyam](https://github.com/Wyamio/Wyam){target="_blank"},
like the [Cake website](https://cakebuild.net/){target="_blank"}.
Using [Wyam](https://github.com/Wyamio/Wyam){target="_blank"} allowed to add full API documentation and other features like list of addins or overview of open issues across all addins.

[Wyam](https://github.com/Wyamio/Wyam){target="_blank"} is no longer maintained,
with [Statiq](https://www.statiq.dev/){target="_blank"} as its successor.
Unfortunately also the deployment pipeline for the Cake Issues website stopped working in 2023, resulting in the website no longer receiving updates.

Since the introduction of the Cake Issues website seven years ago, the [Cake website](https://cakebuild.net/){target="_blank"} also has been improved,
among other things, with a dedicated page for each addin listing the aliases the addin provides.
These improvements made a full API documentation on Cake Issues website somehow redundant.

With .NET API documentation no longer being a requirement there are much more tooling options available.
The choice was made to use [Material for MkDocs](https://squidfunk.github.io/mkdocs-material/){target="_blank"},
which comes with a lot of features for writing technical documentation.

One of the features of the new website is support for multiple versions using [mike](https://github.com/jimporter/mike){target="_blank"}.
`https://cakeissues.net/latest/` will always point to the documentation of the latest released version.
`https://cakeissues.net/develop/` points to the documentation of the current development branch.
Beside that there's now a version selector in the header to show documentation for any old version.
