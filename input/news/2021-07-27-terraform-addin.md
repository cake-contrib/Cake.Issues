---
title: New addin for Terraform support
category: Release Notes
---

A new [Cake.Issues.Terraform addin] has been released which adds support for reading issues from Terraform `validate` command..

<!--excerpt-->

[Cake.Issues.Terraform addin] brings support for Terraform to the Cake.Issues ecosystem.
It allows to read the output of the Terraform `validate` command.
Together with other Cake.Issues addins it is possible to create powerful infrastructure as code pipelines which
ensure quality standards by validating Terraform files before merging or deploying the changes.
When using a pull requests workflow it is also possible to have the issues reported by Terraform validate automatically
reported as comments to pull request.

[Cake.Issues.Terraform addin]: ../docs/issue-providers/terraform/
