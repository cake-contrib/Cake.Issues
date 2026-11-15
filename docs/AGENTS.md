# Documentation website

Follow [the shared instructions](../AGENTS.md).

The website uses Zensical. `mkdocs.yml` is the configuration, `main.py` defines
macros, `requirements.txt` defines Python dependencies, and `input` contains
maintained Markdown pages and usage examples. API reference content is generated.

## Tooling

Python 3.12+ is required. Install missing documentation dependencies with:

```bash
cd docs
pip install -r requirements.txt
```

```powershell
Push-Location docs
pip install -r requirements.txt
Pop-Location
```

From `docs`, run `zensical serve` for the development server at
`http://127.0.0.1:8000` (typically builds in 30 seconds), or
`zensical build --strict` to validate website changes. The site is built to
`docs/site`.

Alternatively, from the repository root use `./build.sh --target=website` or
`.\build.ps1 --target=website` to build and serve the documentation.

## Content and generated assets

- Preserve the surrounding Markdown style, terminology, and links.
- Use Cake Script and Cake Frosting examples where the page supports both.
- Use `write-release-notes` for new release news posts and
  `update-version-in-examples` when changing the Cake Issues version in examples.
- Do not edit generated Generic HTML report galleries under
  `input/documentation/report-formats/generic/templates` by hand. Use the
  `update-dxdatagrid-theme-docs` skill after changing their source templates,
  themes, or report options.

## Console screenshots

After changing Console reporting code or its documentation examples, regenerate
the checked-in screenshots:

1. Create local NuGet packages from the repository root.
2. Install missing renderer dependencies in
   `tests/Cake.Issues.Reporting.Console/script-runner/net10.0`.
3. Run `npm run screenshots` from that runner directory.

Keep shared Console report-generation tasks and sample data under
`tests/Cake.Issues.Reporting.Console/common`, following the Generic
report-format demos.
