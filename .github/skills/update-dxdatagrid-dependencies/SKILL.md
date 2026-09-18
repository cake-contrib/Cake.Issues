---
name: update-dxdatagrid-dependencies
description: Complete Renovate dependency updates found in generated DxDataGrid gallery HTML by updating the Razor template defaults and option documentation, then regenerating the gallery. Use this for Renovate PRs that change CDN dependency versions in htmldxdatagrid-demo-*.html files.
---

Use this skill when Renovate updates a dependency URL in a generated
`docs/input/documentation/report-formats/generic/templates/htmldxdatagrid-demo-*.html`
file. The generated HTML is not the source of truth: make the equivalent
version change in the DxDataGrid Razor template and its public option
documentation, then invoke the `update-dxdatagrid-theme-docs` skill to
regenerate the gallery.

See [copilot-instructions](../../copilot-instructions.md) for general repository
guidance and
[update-dxdatagrid-theme-docs](../update-dxdatagrid-theme-docs/SKILL.md) for the
generation workflow.

## Dependency mapping

For the dependency and target version shown in the Renovate diff, update both
entries in the matching row:

| Renovate dependency / URL fragment | Razor default in `DxDataGrid.cshtml` | Option documentation in `HtmlDxDataGridOption.cs` |
|---|---|---|
| `jquery` / `ajax/jquery/jquery-<version>.min.js` | `ViewBag.JQueryVersion` | `JQueryVersion` |
| `devextreme` / `jslib/<version>` | `ViewBag.DevExtremeVersion` | `DevExtremeVersion` |
| `exceljs` / `ajax/libs/exceljs/<version>` | `ViewBag.ExcelJsVersion` | `ExcelJsVersion` |
| `file-saver` or `FileSaver.js` / `ajax/libs/FileSaver.js/<version>` | `ViewBag.FileSaverJsVersion` | `FileSaverJsVersion` |
| `jspdf` / `ajax/libs/jspdf/<version>` | `ViewBag.JsPdfVersion` | `JsPdfVersion` |
| `jspdf-autotable` / `ajax/libs/jspdf-autotable/<version>` | `ViewBag.JsPdfAutoTableVersion` | `JsPdfAutotableVersion` |

The source files are:

- [DxDataGrid.cshtml](../../../src/ReportFormats/Generic/Cake.Issues.Reporting.Generic/Templates/DxDataGrid.cshtml)
- [HtmlDxDataGridOption.cs](../../../src/ReportFormats/Generic/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption.cs)

## Workflow

1. Inspect the current diff and identify the dependency, old version, target
   version, and every changed `htmldxdatagrid-demo-*.html` file. Use the exact
   target version chosen by Renovate; do not independently select a newer
   version.
2. Update the matching `ViewBagHelper.ValueOrDefault` version string in
   `DxDataGrid.cshtml`.
3. Update the matching `Default value is <c>...</c>.` XML documentation in
   `HtmlDxDataGridOption.cs` to the same version.
4. Search both source files for the old and target versions. Confirm the old
   default is gone from the matching dependency and that the Razor default and
   option documentation agree exactly.
5. Invoke the `update-dxdatagrid-theme-docs` skill. Follow its complete
   package-build, gallery-generation, and verification workflow rather than
   reproducing those steps here.
6. Inspect the final diff. The Renovate-edited generated file must now be
   reproducible from the updated Razor template. Keep all additional generated
   `htmldxdatagrid-demo-*.html` changes produced by the generator when they use
   the same default dependency.
7. When updating an existing pull request, query the pull request metadata
   before fetching or pushing:
   ```powershell
   gh api "repos/<owner>/<repo>/pulls/<number>" --jq '{
     baseRepository: .base.repo.full_name,
     baseRefName: .base.ref,
     headRepository: .head.repo.full_name,
     headRefName: .head.ref
   }'
   ```
   Identify the Git remotes whose repositories match `baseRepository` and
   `headRepository`; do not infer them from remote names such as `origin` or
   `upstream`. Remote names and ownership differ between local clones, forks,
   worktrees, and hosted agent environments. If no configured remote matches
   either repository, add or use an explicit remote for the reported
   repository.
8. Update the pull request branch from the reported `baseRefName`, integrate
   using the matching base-repository remote, then integrate the source and
   generated changes and push to the reported `headRefName` on the matching
   head-repository remote. After pushing, verify the pull request itself rather
   than relying on the push output:
   ```powershell
   gh pr view <number> --repo <owner/repo> --json headRefOid,files `
     --jq '{headRefOid: .headRefOid, files: [.files[].path]}'
   ```
   The pull request must list the Razor template, option documentation, and
   expected regenerated HTML. A successful push to a same-named branch in a
   different fork does not update the pull request.

## Guardrails

- Do not hand-edit generated `htmldxdatagrid-demo-*.html` files, including the
  file initially changed by Renovate.
- Do not update only the generated HTML. A completed dependency update always
  includes `DxDataGrid.cshtml`, `HtmlDxDataGridOption.cs`, and regenerated
  gallery output.
- Do not change CDN locations, option names, unrelated dependency versions, or
  explicit per-demo overrides unless the Renovate update specifically requires
  them.
- Some dependencies are emitted only when exporting is enabled or for one
  export format, while jQuery and DevExtreme occur in most gallery files. Judge
  the expected generated file set from actual regenerated output rather than
  assuming every dependency must change every gallery file.
- If the generated output does not contain Renovate's target version, stop and
  fix the source/default mapping. Do not restore the generated-only Renovate
  edit as a workaround.
- Do not assume `origin` is the pull request head repository or that `upstream`
  is the base repository. Resolve both from pull request metadata and remote
  repository identity for the current environment.
- Do not report the pull request as updated until its `headRefOid` and file
  list have been checked after the push.

## Completion criteria

- The Razor default and public option documentation contain the same target
  version.
- The existing gallery-generation skill completed successfully.
- The generated HTML contains the target version wherever that default is
  exercised.
- No generated HTML was manually patched and no unrelated dependency version
  changed.
- If an existing pull request was updated, its verified file list includes
  `DxDataGrid.cshtml`, `HtmlDxDataGridOption.cs`, and the expected regenerated
  gallery output.
