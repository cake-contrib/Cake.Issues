# Console report format example generation

The Console integration tests share the four report scenarios, issue fixture,
and checked-in source file under `tests/Cake.Issues.Reporting.Console/common`.
Both Cake Script Runner and Cake Frosting run and validate every scenario on
.NET 8 and .NET 10. Fixture locations are validated before each report is
rendered, and Errata rendering errors fail the test rather than being included
in a screenshot.

To regenerate all screenshots, create local NuGet packages from the repository
root, then install the renderer dependencies and run the generator:

```bash
./build.sh --target=Create-NuGet-Packages
cd tests/Cake.Issues.Reporting.Console/script-runner/net8.0
npm ci
npm run screenshots
```

The script runner's `global.json` selects .NET SDK 8.0.427 and rolls forward to
later feature bands in the .NET 8 line. Node.js 18 or newer and a local Chrome,
Chromium, or Microsoft Edge installation are also required. On Windows, use
`.\build.ps1 --target=Create-NuGet-Packages` for the first command. If the
browser is not in a standard location, set `CONSOLE_SCREENSHOT_BROWSER` to its
executable path.

The generator captures ANSI output directly from the real Console addin,
checks every report, and uses the browser to crop each PNG to the console
content. It writes all four PNGs to `docs/input/assets/images/console-examples/`
only after all reports and screenshots succeed.
