# Addin development

Follow [the shared instructions](../AGENTS.md).

## SDK and structure

- `global.json` in this directory pins the addin SDK (currently .NET 11).
  Run `dotnet --version` from `src` to check the selected SDK; running it from
  the repository root does not apply this file.
- `Directory.Build.props` defines supported target frameworks.
- `Cake.Issues.slnx` is the main solution.
- Main addins use the `Cake.Issues.*` naming convention and have corresponding
  `.Tests` projects. Keep changes in the relevant addin and its tests.

## Testing and documentation

- Use the shared utilities in `Cake.Issues.Testing`.
- Use `Cake.Issues.Testing/IssueChecker.cs` when comparing issues against expected
  results in tests.
- Update basic usage documentation in `docs`; do not hand-edit generated API
  reference content.
- Changes to `ReportFormats/Console/Cake.Issues.Reporting.Console` require
  regenerating the documentation screenshots as described in
  [docs/AGENTS.md](../docs/AGENTS.md).
- Changes to Generic report Razor templates, themes, or options require the
  `update-dxdatagrid-theme-docs` skill to refresh the checked-in demo galleries.
