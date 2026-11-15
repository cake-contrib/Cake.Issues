# NuGet packaging

Follow [the shared instructions](../AGENTS.md).

- Specifications and package README files live under `nuget`.
- Each addin has separate specifications for Cake .NET Tool and Cake Frosting.
  Keep directly related package variants consistent.
- Each package includes a README. Update it for relevant usage changes.
- Cake Frosting packages contain targets files for namespace imports.
- Use `update-release-notes-link` when preparing a new release's package links.
- Use `update-frosting-dependencies` when updating inter-package dependency
  ranges for a major release.
