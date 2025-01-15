---
title: Pull Request System Integration
description: Overview about integration with build server and pull request systems.
---

This page documents how integration with build server and pull request systems work.

!!! note
    In most cases the integration will just work as expected.
    Using Cake Issues does not require an in-depth understanding of these concepts.
    For some advanced scenarios it can be useful to have an deeper understand how the
    pipeline to report issues to build servers and pull requests work.

    Most of the default behavior can be customized through [IReportIssuesToPullRequestSettings]{target="_blank"}.

The pipeline for integration with build servers and pull request systems looks like this:

``` mermaid
graph LR
  existingThreads["`Handle existing
  discussion threads`"]
  filterIssues["`Filter
  issues`"]
  checkCommitId["`Check
  commit ID`"]
  postThreads["`Post
  discussion threads`"]

  existingThreads --> filterIssues;
  filterIssues --> checkCommitId;
  checkCommitId --> postThreads;
```

* [Handle existing discussion threads](#handle-existing-discussion-threads)

    Implementations supporting the `Discussion Threads` capability will automatically
    resolve comments on a pull request if an issue no longer exists on a subsequent run,
    or reopen comments if they are closed but the issue still exists.

* [Filter issues](#filter-issues)

    Issue filtering allows to reduce the set of issues reported to a build server or pull request system.

* [Check commit id](#check-commit-id)

    Check commit id will skip posting of issues if the current commit of the pull request is different to the commit
    for which the issues are reported.

* [Post discussion threads](#post-discussion-threads)

    Post discussion threads will post the issues in the pipeline, which have not been filtered out, to the
    build server or pull request.

## Handle existing discussion threads

!!! Note
    This step will only be executed for implementations supporting the `Discussion Threads` capability.

The step will automatically resolve comments, posted in a previous run to the pull request, if the
related issue no longer exists on a subsequent run.
Comments which are closed, but where the related issue still exists, will be reopened.

The flow for handling existing discussion threads looks like this:

``` mermaid
graph LR
  readThreads["`Read
  existing threads`"]
  forEveryIssue{"`More
  issues?`"}
  getComments["`Get existing
  comments for issue`"]
  resolveComments["`Resolve
  existing comments`"]
  reopenComments["`Reopen
  existing comments`"]
  wfEnd([End])

  readThreads --> forEveryIssue;
  forEveryIssue -->|Yes| getComments;
  getComments --> resolveComments;
  resolveComments --> reopenComments;
  reopenComments --> forEveryIssue;
  forEveryIssue -->|No| wfEnd;
```

The `Resolve existing comments` step will close threads if they fulfill the following conditions:

<div class="annotate" markdown>

* The thread was created by the same logic (1)
* The thread is for the same file
* The thread was created for the same issue
* The thread is active

</div>

1.  Defined by the [IReportIssuesToPullRequestSettings.CommentSource property]{target="_blank"}

The `Reopen existing comments` step will reopen threads if they fulfill the following conditions:

<div class="annotate" markdown>

* The thread was created by the same logic (1)
* The thread is for the same file
* The thread was created for the same issue
* The thread is resolved

</div>

1.  Defined by the [IReportIssuesToPullRequestSettings.CommentSource property]{target="_blank"}

## Filter issues

Issue filtering allows to reduce the set of issues reported to a build server or pull request system
through settings on [IReportIssuesToPullRequestSettings]{target="_blank"}.

The overall filtering process looks like this:

``` mermaid
graph LR
  filterByPath["`Filter issues
  by path`"]
  filterExistingComments["`Filter already
  existing comments`"]
  filterByNumber["`Filter issues
  by number`"]
  customFilters["`Apply custom filters`"]

  filterByPath --> filterExistingComments;
  filterExistingComments --> filterByNumber;
  filterByNumber --> customFilters;
```

* [Filter issues by path](#filter-issues-by-path)

    Filters issues not affecting files changed in the pull request.

* [Filter already existing comments](#filter-already-existing-comments)

    Filters issues, which were already reported in a previous run, so they won't be reported again.

* [Filter issues by number](#filter-issues-by-number)

    Limits the number of issues which should be reported based on different criteria.

* [Apply custom filters](#apply-custom-filters)

    Custom filters allow to to further limit the issues which will be reported.

### Filter issues by path

!!! Note
    This step will only be executed for implementations supporting the `Filtering by modified files` capability.

The step will filter out issues not affecting files changed in the pull request.

The flow looks like this:

``` mermaid
graph LR
  getModifiedFilesInPullRequest["`Get files modified
  in pull request`"]
  removeIssues["`Remove issues not related
  to modified files`"]

  getModifiedFilesInPullRequest --> removeIssues;
```

### Filter already existing comments

!!! Note
    This step will only be executed for implementations supporting the `Discussion Threads` capability.

The step will filter issues, which were already reported in a previous run, so they won't be reported again.

Issues will be filtered if a comment exists which fulfills the following criteria:

* The thread was created for the relevant issue
* The thread is either in status `Active`, `Resolved` or `Won't Fix`

### Filter issues by number

For projects containing legacy code with existing issues, it is possible
to limit the number of issues reported to a pull request through
[IReportIssuesToPullRequestSettings]{target="_blank"} properties.

The flow for filtering issues by number looks like this:

``` mermaid
graph LR
  perIssueProvider["`Apply issue limits
  per issue provider`"]
  perIssueProviderForRun["`Apply issue limits
  per provider
  for this run`"]
  globalIssueLimit["`Apply global
  issue limit`"]
  perIssueProviderAcrossRuns["`Apply issue limits
  per provider
  across multiple runs`"]
  globalIssueLimitAcrossRuns["`Apply global
  issue limit
  across multiple runs`"]

  perIssueProvider --> perIssueProviderForRun;
  perIssueProviderForRun --> globalIssueLimit;
  globalIssueLimit --> perIssueProviderAcrossRuns;
  perIssueProviderAcrossRuns --> globalIssueLimitAcrossRuns;
```

* [Apply issue limits per issue provider](#apply-issue-limits-per-issue-provider)

    Allows to limit the number of issues which should be posted at maximum for each issue provider.

* [Apply issue limits per provider for this run](#apply-issue-limits-per-provider-for-this-run)

    Allows to limit the number of issues which should be posted at maximum for individual issue providers.

* [Apply global issue limit](#apply-global-issue-limit)

    Allows to limit the total number of issues which should be posted.

* [Apply issue limits per provider across multiple runs](#apply-issue-limits-per-provider-across-multiple-runs)

    Allows to limit the number of issues which should be posted at maximum for specific issue providers across multiple runs.

* [Apply global issue limit across multiple runs](#apply-global-issue-limit-across-multiple-runs)

    Allows to limit the total number of issues which should be posted across multiple runs.

The issues are filtered according to the following criteria:

1. `Priority`: Issues with a lower priority are filtered out first,
   issues with a higher priority are reported preferentially.
2. `File path`: Issues not related to a file are filtered out first,
   issues related to files are reported preferentially.

#### Apply issue limits per issue provider

The [IReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider property]{target="_blank"}
can be used to limit the number of issues which should be posted at maximum for each issue provider.

Setting this value will apply to all issue providers.

??? example
    In the following example `228` issues from MsBuild issue provider and
    `134` issues from SARIF issue provider were reported.
    Due to [IReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider property]{target="_blank"}
    setting, issues are limited to `100` for both issues from MsBuild and SARIF issue provider.

    ``` mermaid
    graph TB
      msBuildBefore@{ shape: docs, label: "MsBuild Issues: 228"}
      sarifBefore@{ shape: docs, label: "Sarif Issues: 134"}
      setting[\"MaxIssuesToPostForEachIssueProvider=100"/]
      msBuildAfter@{ shape: docs, label: "MsBuild Issues: 100"}
      sarifAfter@{ shape: docs, label: "Sarif Issues: 100"}

      classDef input stroke:#EF5552
      classDef setting fill:#2094F3;
      classDef output stroke:#8BC34B

      class msBuildBefore,sarifBefore input;
      class setting setting;
      class msBuildAfter,sarifAfter output;

      msBuildBefore --> setting;
      sarifBefore --> setting;
      setting --> msBuildAfter;
      setting --> sarifAfter;
    ```

#### Apply issue limits per provider for this run

The [IReportIssuesToPullRequestSettings.ProviderIssueLimits property]{target="_blank"} with the
[MaxIssuesToPost]{target="_blank"} setting can be used to limit the number of issues which
should be posted at maximum for individual issue providers.

This setting allows to define different values for different issue providers.

??? example
    In the following example `100` issues from MsBuild issue provider and
    `100` issues from SARIF issue provider are in the pipeline.
    Due to [MaxIssuesToPost]{target="_blank"} setting, issues from SARIF issue provider are limited to `50`.

    ``` mermaid
    graph TB
      msBuildBefore@{ shape: docs, label: "MsBuild Issues: 100"}
      sarifBefore@{ shape: docs, label: "Sarif Issues: 100"}
      setting[\"Sarif MaxIssues=50"/]
      msBuildAfter@{ shape: docs, label: "MsBuild Issues: 100"}
      sarifAfter@{ shape: docs, label: "Sarif Issues: 50"}

      classDef input stroke:#EF5552
      classDef setting fill:#2094F3;
      classDef output stroke:#8BC34B

      class msBuildBefore,sarifBefore input;
      class setting setting;
      class msBuildAfter,sarifAfter output;

      msBuildBefore --> setting;
      sarifBefore --> setting;
      setting --> msBuildAfter;
      setting --> sarifAfter;
    ```

#### Apply global issue limit

The [IReportIssuesToPullRequestSettings.MaxIssuesToPost property]{target="_blank"} can be used to
limit the total number of issues which should be posted.

This limit is across all issue provider.

??? example
    In the following example `100` issues from MsBuild issue provider and
    `50` issues from SARIF issue provider are in the pipeline.
    Due to [IReportIssuesToPullRequestSettings.MaxIssuesToPost property]{target="_blank"}
    setting, issues are limited to `75` in total.
    The numbers in the output are examples.
    The actual numbers depend solely on priority and file path of individual issues.

    ``` mermaid
    graph TB
      msBuildBefore@{ shape: docs, label: "MsBuild Issues: 100"}
      sarifBefore@{ shape: docs, label: "Sarif Issues: 50"}
      setting[\"MaxIssuesToPost=75"/]
      msBuildAfter@{ shape: docs, label: "MsBuild Issues: 40"}
      sarifAfter@{ shape: docs, label: "Sarif Issues: 35"}

      classDef input stroke:#EF5552
      classDef setting fill:#2094F3;
      classDef output stroke:#8BC34B

      class msBuildBefore,sarifBefore input;
      class setting setting;
      class msBuildAfter,sarifAfter output;

      msBuildBefore --> setting;
      sarifBefore --> setting;
      setting --> msBuildAfter;
      setting --> sarifAfter;
    ```

#### Apply issue limits per provider across multiple runs

!!! Note
    This step will only be executed for implementations supporting the `Discussion Threads` capability.

The [IReportIssuesToPullRequestSettings.ProviderIssueLimits property]{target="_blank"} with the
[MaxIssuesToPostAcrossRuns]{target="_blank"} setting can be used to limit the number of issues
which should be posted at maximum for specific issue providers across multiple runs

This setting allows to define different values for different issue providers.

??? example
    In the following example in a previous run `20` issues from MsBuild issue provider and
    `50` issues from SARIF issue provider were posted.
    Current run at this point would post `40` issues from MsBuild issue provider and
    `35` issues from SARIF issue provider.
    But since [MaxIssuesToPostAcrossRuns]{target="_blank"} for MsBuild issue provider is set
    to `30`, the list of issues reported from the MsBuild issue provider will be filtered to `10` issues.
    Since no filtering for issues reported by the SARIF issue provider is defined, the `35` issues
    from the SARIF issue provider won't be further filtered in this step.

    ``` mermaid
    graph TB
      subgraph Current run
        direction TB

        msBuildBefore@{ shape: docs, label: "MsBuild Issues: 40"}
        sarifBefore@{ shape: docs, label: "Sarif Issues: 35"}
        setting[\"MsBuild MaxIssuesToPostAcrossRuns=30"/]
        msBuildAfter@{ shape: docs, label: "MsBuild Issues: 10"}
        sarifAfter@{ shape: docs, label: "Sarif Issues: 35"}
  
        classDef input stroke:#EF5552
        classDef setting fill:#2094F3;
        classDef output stroke:#8BC34B
  
        class msBuildBefore,sarifBefore input;
        class setting setting;
        class msBuildAfter,sarifAfter output;
  
        msBuildBefore --> setting;
        sarifBefore --> setting;
        setting --> msBuildAfter;
        setting --> sarifAfter;
      end

      subgraph Previous run
        direction TB

        msBuildAfterPrev@{ shape: docs, label: "MsBuild Issues: 20"}
        sarifAfterPrev@{ shape: docs, label: "Sarif Issues: 50"}

        classDef output stroke:#8BC34B

        class msBuildAfterPrev,sarifAfterPrev output;
      end
    ```

    !!! note
        The above example does not consider if an issue has already be posted or not.
        This will already been taken care of by the [Filter for already existing comments]
        which will be applied previously in the pipeline.

#### Apply global issue limit across multiple runs

!!! Note
    This step will only be executed for implementations supporting the `Discussion Threads` capability.

The [MaxIssuesToPostAcrossRuns](https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/F02390D1){target="_blank"}
setting can be used to limit the total number of issues which should be posted across multiple runs

This limit is across all issue provider.

??? example
    In the following example in a previous run `20` issues from MsBuild issue provider and
    `50` issues from SARIF issue provider were posted.
    Current run at this point would post `10` issues from MsBuild issue provider and
    `35` issues from SARIF issue provider.
    Due to [MaxIssuesToPostAcrossRuns](https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/F02390D1){target="_blank"}
    setting, issues are limited to `30` in total, resulting in `100` issues across runs together with the `70` issues posted to the previous run.
    The numbers in the output are examples.
    The actual numbers depend solely on priority and file path of individual issues.

    ``` mermaid
    graph TB
      subgraph Current run
        direction TB

        msBuildBefore@{ shape: docs, label: "MsBuild Issues: 10"}
        sarifBefore@{ shape: docs, label: "Sarif Issues: 35"}
        setting[\"MaxIssuesToPostAcrossRuns=100"/]
        msBuildAfter@{ shape: docs, label: "MsBuild Issues: 10"}
        sarifAfter@{ shape: docs, label: "Sarif Issues: 35"}
  
        classDef input stroke:#EF5552
        classDef setting fill:#2094F3;
        classDef output stroke:#8BC34B
  
        class msBuildBefore,sarifBefore input;
        class setting setting;
        class msBuildAfter,sarifAfter output;
  
        msBuildBefore --> setting;
        sarifBefore --> setting;
        setting --> msBuildAfter;
        setting --> sarifAfter;
      end

      subgraph Previous run
        direction TB

        msBuildAfterPrev@{ shape: docs, label: "MsBuild Issues: 20"}
        sarifAfterPrev@{ shape: docs, label: "Sarif Issues: 50"}

        classDef output stroke:#8BC34B

        class msBuildAfterPrev,sarifAfterPrev output;
      end
    ```

### Apply custom filters

The step will apply any additional custom filters defined using
[IReportIssuesToPullRequestSettings.IssueFilters property]{target="_blank"}
to further limit the issues which will be reported.

??? example
    See [Custom Issue Filter] for an example how to define custom filters.

## Check commit ID

!!! Note
    This step will only be executed for implementations supporting the `Checking commit ID` capability.

The step will skip posting of issues if the current commit of the pull request is different to the commit
ID passed to [IReportIssuesToPullRequestSettings.CommitId]{target="_blank"}.

## Post discussion threads

This step will post the issues in the pipeline which have not been filtered out, to the build server or pull request.

## Troubleshooting

The build server and pull request system integration writes extensive log entries when running with `Diagnostic` verbosity.

[IReportIssuesToPullRequestSettings]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings
[IReportIssuesToPullRequestSettings.CommentSource property]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/71BCD0B8
[IReportIssuesToPullRequestSettings.IssueFilters property]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/48CB35E4
[IReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider property]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/115BF82C
[IReportIssuesToPullRequestSettings.ProviderIssueLimits property]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/F239BC1F
[IReportIssuesToPullRequestSettings.MaxIssuesToPost property]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/7ED19AA9
[Custom Issue Filter]: ../usage/reporting-issues-to-pull-requests/custom-issue-filter.md
[MaxIssuesToPost]: https://cakebuild.net/api/Cake.Issues.PullRequests/IProviderIssueLimits/4100059F
[MaxIssuesToPostAcrossRuns]: https://cakebuild.net/api/Cake.Issues.PullRequests/IProviderIssueLimits/0A990776
[Filter for already existing comments]: #filter-already-existing-comments
[IReportIssuesToPullRequestSettings.CommitId]: https://cakebuild.net/api/Cake.Issues.PullRequests/IReportIssuesToPullRequestSettings/82F21902
