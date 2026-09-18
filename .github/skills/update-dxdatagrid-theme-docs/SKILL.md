---
name: update-dxdatagrid-theme-docs
description: Regenerate the demo report templates (including the DxDataGrid theme galleries) under docs/input/documentation/report-formats/generic/templates. Use this after changing the DxDataGrid/DataTable/Diagnostic Razor templates, themes, or their generic report format options.
---

Use this skill to refresh the generated HTML template files that back the demo galleries in the Cake.Issues.Reporting.Generic documentation, most notably the DxDataGrid theme gallery (`htmldxdatagrid-demo-theme-*.html`).

See [copilot-instructions](../../copilot-instructions) for general repo build guidance.

## Background

The files under [docs/input/documentation/report-formats/generic/templates](../../../docs/input/documentation/report-formats/generic/templates) are **not** edited by hand. Every `htmldxdatagrid-demo-*.html`, `htmldatatable-demo-*.html`, and `htmldiagnostic-demo-*.html` file is produced by running the `Cake.Issues.Reporting.Generic` integration test build, which calls `CreateIssueReport` for each demo scenario/theme and writes the result directly into that folder.

Each demo is defined as its own Cake task, and every runner variant under `tests/Cake.Issues.Reporting.Generic` defines the same set of tasks. The two runner styles use different file layouts:

**Cake Scripting (`script-runner/net8.0`, `script-runner/net10.0`)** — `.cake` files in [build/create-reports](../../../tests/Cake.Issues.Reporting.Generic/script-runner/net8.0/build/create-reports):
- `create-reports-htmldxdatagrid-theme-*.cake` — one file per DxDataGrid theme (e.g. `create-reports-htmldxdatagrid-theme-carmine.cake`), each setting `HtmlDxDataGridOption.Theme` to the matching `DevExtremeTheme` value.
- `create-reports-htmldxdatagrid-*.cake` — one file per DxDataGrid feature demo (grouping, sorting, exporting, column chooser, persistence, etc.).
- `create-reports-htmldatatable*.cake` / `create-reports-htmldiagnostic*.cake` — same pattern for the other two report formats.
- `create-reports.cake` / `create-reports-htmldxdatagrid.cake` aggregate all of the above via `#load` plus `.IsDependentOn(...)` into the `Create-Reports` / `Create-Reports-HtmlDxDataGrid` tasks.

**Cake Frosting (`frosting/net8.0`, `frosting/net10.0`)** — C# task classes in [build/tasks/create-reports](../../../tests/Cake.Issues.Reporting.Generic/frosting/net8.0/build/tasks/create-reports):
- One `FrostingTask<BuildContext>` class per demo, e.g. `CreateReportsHtmlDxDataGridThemeCarmineTask.cs`, annotated with `[TaskName("Create-Reports-HtmlDxDataGrid-Theme-Carmine")]` and `[IsDependentOn(typeof(AnalyzeTask))]`.
- `CreateReportsHtmlDxDataGridTask.cs` / `CreateReportsTask.cs` aggregate them through `[IsDependentOn(typeof(...Task))]` attributes.

Task names and generated output filenames are identical across both runners.

The `.md` files (e.g. [htmldxdatagrid.md](../../../docs/input/documentation/report-formats/generic/templates/htmldxdatagrid.md)) already link to every generated `.html` file and normally do **not** need changes unless you are adding or removing a demo/theme entirely. The `.png` screenshots in that folder are manually captured and are out of scope for this skill.

## When to use this skill

- After modifying `DxDataGrid.cshtml`, `DataTable.cshtml`, or `Diagnostic.cshtml` in [src/ReportFormats/Generic/Cake.Issues.Reporting.Generic/Templates](../../../src/ReportFormats/Generic/Cake.Issues.Reporting.Generic/Templates).
- After adding/changing a `DevExtremeTheme` value or other `HtmlDxDataGridOption`.
- After changing any generic report format option's default behavior.
- When adding a brand-new theme or demo scenario (this also requires new task files for both runner styles, wiring them into the aggregating tasks, and adding a link + heading to the relevant `.md` file — see [Adding a new theme or demo](#adding-a-new-theme-or-demo), and do this before regenerating).

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

6. **Do not hand-edit the generated `.html` files.** If the output looks wrong, fix the Razor template, the report format option, or the corresponding demo task (the `create-reports-*.cake` file for the script runner, the `CreateReports*Task.cs` class for Frosting), then repeat from step 2.

## Adding a new theme or demo

If the change adds a new DxDataGrid theme or demo scenario rather than just altering an existing one:

1. Add the demo task to **both** runner styles, keeping the task name and output filename identical everywhere:
   - **Cake Scripting:** add `create-reports-htmldxdatagrid-theme-<name>.cake` (or a feature-demo equivalent) to `tests/Cake.Issues.Reporting.Generic/script-runner/net8.0/build/create-reports` and `.../script-runner/net10.0/build/create-reports`, following an existing theme file (set `HtmlDxDataGridOption.Theme` and write to `htmldxdatagrid-demo-theme-<name>.html`).
   - **Cake Frosting:** add `CreateReportsHtmlDxDataGridTheme<Name>Task.cs` to `tests/Cake.Issues.Reporting.Generic/frosting/net8.0/build/tasks/create-reports` and `.../frosting/net10.0/build/tasks/create-reports`, following e.g. `CreateReportsHtmlDxDataGridThemeCarmineTask.cs` (`[TaskName(...)]` matching the script-runner task name, `[IsDependentOn(typeof(AnalyzeTask))]`).
2. Wire the new task into the aggregating tasks:
   - Add `#load` and `.IsDependentOn("Create-Reports-HtmlDxDataGrid-Theme-<Name>")` to `create-reports-htmldxdatagrid.cake` in both script-runner directories.
   - Add `[IsDependentOn(typeof(CreateReportsHtmlDxDataGridTheme<Name>Task))]` to `CreateReportsHtmlDxDataGridTask.cs` in both frosting directories.
3. Add a link (and heading, if the `.md` groups themes under headings) to `htmldxdatagrid.md`.
4. Run the steps above to generate the new `.html` file.
