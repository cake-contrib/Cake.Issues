---
name: update-dxdatagrid-theme-docs
description: Regenerate the demo report templates (including the DxDataGrid theme galleries) under docs/input/documentation/report-formats/generic/templates. Use this after changing the DxDataGrid/DataTable/Diagnostic Razor templates, themes, or their generic report format options.
---

Use this skill to refresh the generated HTML template files that back the demo galleries in the Cake.Issues.Reporting.Generic documentation, most notably the DxDataGrid theme gallery (`htmldxdatagrid-demo-theme-*.html`).

See [copilot-instructions](../../copilot-instructions) for general repo build guidance.

## Background

The files under [docs/input/documentation/report-formats/generic/templates](../../../docs/input/documentation/report-formats/generic/templates) are **not** edited by hand. Every `htmldxdatagrid-demo-*.html`, `htmldatatable-demo-*.html`, and `htmldiagnostic-demo-*.html` file is produced by running the `Cake.Issues.Reporting.Generic` integration test build, which calls `CreateIssueReport` for each demo scenario/theme and writes the result directly into that folder.

The relevant Cake tasks live in [tests/Cake.Issues.Reporting.Generic/script-runner/net8.0/build/create-reports](../../../tests/Cake.Issues.Reporting.Generic/script-runner/net8.0/build/create-reports):
- `create-reports-htmldxdatagrid-theme-*.cake` — one file per DxDataGrid theme (e.g. `create-reports-htmldxdatagrid-theme-carmine.cake`), each setting `HtmlDxDataGridOption.Theme` to the matching `DevExtremeTheme` value.
- `create-reports-htmldxdatagrid-*.cake` — one file per DxDataGrid feature demo (grouping, sorting, exporting, column chooser, persistence, etc.).
- `create-reports-htmldatatable*.cake` / `create-reports-htmldiagnostic*.cake` — same pattern for the other two report formats.
- `create-reports.cake` / `create-reports-htmldxdatagrid.cake` aggregate all of the above into the `Create-Reports` / `Create-Reports-HtmlDxDataGrid` tasks.

The `.md` files (e.g. [htmldxdatagrid.md](../../../docs/input/documentation/report-formats/generic/templates/htmldxdatagrid.md)) already link to every generated `.html` file and normally do **not** need changes unless you are adding or removing a demo/theme entirely. The `.png` screenshots in that folder are manually captured and are out of scope for this skill.

## When to use this skill

- After modifying `DxDataGrid.cshtml`, `DataTable.cshtml`, or `Diagnostic.cshtml` in [src/ReportFormats/Generic/Cake.Issues.Reporting.Generic/Templates](../../../src/ReportFormats/Generic/Cake.Issues.Reporting.Generic/Templates).
- After adding/changing a `DevExtremeTheme` value or other `HtmlDxDataGridOption`.
- After changing any generic report format option's default behavior.
- When adding a brand-new theme or demo scenario (this also requires a new `create-reports-*.cake` task file, wiring it into `create-reports-htmldxdatagrid.cake`, and adding a link + heading to the relevant `.md` file — do this before regenerating).

## Steps

1. **Build the packages first.** The integration test scripts consume the addins as NuGet packages (`#addin "Cake.Issues.Reporting.Generic&prerelease"`), so they must exist locally before running the integration test:
   ```powershell
   ./build.sh --target=Create-NuGet-Packages
   ```
   NEVER CANCEL — takes about 2 minutes.

2. **Run the Cake Scripting integration test for `Cake.Issues.Reporting.Generic`.** This regenerates every file in the templates folder in place:
   ```powershell
   cd tests/Cake.Issues.Reporting.Generic/script-runner/net8.0
   ./build.ps1 --verbosity=diagnostic
   ```
   Takes about 15 seconds. This writes directly into `docs/input/documentation/report-formats/generic/templates` (path is resolved relatively from `BuildData.TemplateGalleryFolder` in `build.cake`).

3. **Verify what changed.** From the repo root:
   ```powershell
   git status docs/input/documentation/report-formats/generic/templates
   git diff docs/input/documentation/report-formats/generic/templates
   ```
   Confirm only the expected `.html` files changed (e.g. all `htmldxdatagrid-demo-theme-*.html` files if you changed the theme mechanism, or a single file if you changed one demo/theme). Unexpected diffs across unrelated files usually indicate a version/timestamp or environment difference — investigate before committing.

4. **(Optional) Also refresh via the Cake Frosting runner** if you want to confirm both runners produce identical output (CI runs both):
   ```powershell
   cd tests/Cake.Issues.Reporting.Generic/frosting/net8.0
   ./build.ps1 --verbosity=diagnostic
   ```
   Then re-run `git diff` to confirm no further changes.

5. **(Optional) Preview the docs site** to visually check the regenerated demo pages:
   ```powershell
   cd docs
   pip install -r requirements.txt
   mkdocs serve
   ```
   Open the relevant page (e.g. `/documentation/report-formats/generic/templates/htmldxdatagrid-demo-theme-carmine.html`) at `http://127.0.0.1:8000`.

6. **Do not hand-edit the generated `.html` files.** If the output looks wrong, fix the Razor template, the report format option, or the corresponding `create-reports-*.cake` task, then repeat from step 2.

## Adding a new theme or demo

If the change adds a new DxDataGrid theme or demo scenario rather than just altering an existing one:

1. Add a new `create-reports-htmldxdatagrid-theme-<name>.cake` (or feature-demo) file in each of `tests/Cake.Issues.Reporting.Generic/{script-runner,frosting}/{net8.0,net10.0}/build/create-reports`, following the pattern of an existing theme file (set `HtmlDxDataGridOption.Theme` and the output filename `htmldxdatagrid-demo-theme-<name>.html`).
2. Add the corresponding `#load` and `.IsDependentOn(...)` entries to `create-reports-htmldxdatagrid.cake` in each of those directories.
3. Add a link (and heading, if the `.md` groups themes under headings) to `htmldxdatagrid.md`.
4. Run the steps above to generate the new `.html` file.
